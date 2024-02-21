using System;
using System.Collections;
using System.Collections.Generic;

namespace MbsCore.LightWeightRx
{
    public sealed class CallbackQueue<TValue> : IReadOnlyCallbackCollection<TValue>, ICollection
    {
        private event Action<IReadOnlyCollection<TValue>> OnQueueChanged; 
        
        private readonly Queue<TValue> _queue;

        public int Count => _queue.Count;
        
        bool ICollection.IsSynchronized => ((ICollection)_queue).IsSynchronized;
        object ICollection.SyncRoot => ((ICollection)_queue).SyncRoot;

        public CallbackQueue()
        {
            _queue = new Queue<TValue>();
        }

        public CallbackQueue(IEnumerable<TValue> collection)
        {
            _queue = new Queue<TValue>(collection);
        }

        public CallbackQueue(int capacity)
        {
            _queue = new Queue<TValue>(capacity);
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        public void Enqueue(TValue item)
        {
            _queue.Enqueue(item);
            SendCallbacks();
        }
        
        public bool TryDequeue(out TValue item)
        {
            if (!_queue.TryDequeue(out item))
            {
                return false;
            }
            
            SendCallbacks();
            return true;
        }

        public TValue Dequeue()
        {
            return TryDequeue(out TValue item) ? item : default;
        }

        public bool TryPeek(out TValue result) => _queue.TryPeek(out result);

        public TValue Peek() => _queue.Peek();

        public bool Contains(TValue item)
        {
            return _queue.Contains(item);
        }
        
        public void CopyTo(TValue[] array, int index)
        {
            _queue.CopyTo(array, index);
        }
        
        public void CopyTo(Array array, int index)
        {
            ((ICollection)_queue).CopyTo(array, index);
        }
        
        public void Clear()
        {
            if (Count <= 0)
            {
                return;
            }
            
            _queue.Clear();
            SendCallbacks();
        }

        public IDisposable Subscribe(Action<IReadOnlyCollection<TValue>> callback)
        {
            IDisposable disposable = SkipLastValueSubscribe(callback);
            callback?.Invoke(_queue);
            return disposable;
        }

        public IDisposable SkipLastValueSubscribe(Action<IReadOnlyCollection<TValue>> callback)
        {
            OnQueueChanged += callback;
            return new CallbackDisposableHandler(() => Unsubscribe(callback));
        }

        public void Unsubscribe(Action<IReadOnlyCollection<TValue>> callback)
        {
            OnQueueChanged -= callback;
        }

        private void SendCallbacks()
        {
            OnQueueChanged?.Invoke(_queue);
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}