using System;
using System.Collections;
using System.Collections.Generic;

namespace MbsCore.LightWeightRx
{
    public class CallbackHasSet<TValue> : IReadOnlyCallbackCollection<TValue>, ICollection<TValue>
    {
        private event Action<IReadOnlyCollection<TValue>> OnCollectionChanged; 
        
        private readonly HashSet<TValue> _hashSet;

        public int Count => _hashSet.Count;
        public IEqualityComparer<TValue> Comparer => _hashSet.Comparer;
        
        bool ICollection<TValue>.IsReadOnly { get; }

        public CallbackHasSet()
        {
            _hashSet = new HashSet<TValue>();
        }
        
        public CallbackHasSet(int capacity)
        {
            _hashSet = new HashSet<TValue>(capacity);
        }

        public CallbackHasSet(int capacity, IEqualityComparer<TValue> comparer)
        {
            _hashSet = new HashSet<TValue>(capacity, comparer);
        }

        public CallbackHasSet(IEnumerable<TValue> collection)
        {
            _hashSet = new HashSet<TValue>(collection);
        }

        public CallbackHasSet(IEnumerable<TValue> collection, IEqualityComparer<TValue> comparer)
        {
            _hashSet = new HashSet<TValue>(collection, comparer);
        }

        public bool Contains(TValue item)
        {
            return _hashSet.Contains(item);
        }

        public void CopyTo(TValue[] array)
        {
            _hashSet.CopyTo(array);
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            _hashSet.CopyTo(array, arrayIndex);
        }

        public void CopyTo(TValue[] array, int arrayIndex, int count)
        {
            _hashSet.CopyTo(array, arrayIndex, count);
        }

        public void Add(TValue value)
        {
            if (!_hashSet.Add(value))
            {
                return;
            }
            
            SendCallbacks();
        }

        public bool Remove(TValue value)
        {
            if (!_hashSet.Remove(value))
            {
                return false;
            }
            
            SendCallbacks();
            return true;
        }
        
        public void Clear()
        {
            if (Count <= 0)
            {
                return;
            }
            
            _hashSet.Clear();
            SendCallbacks();
        }
        
        public IEnumerator<TValue> GetEnumerator()
        {
            return _hashSet.GetEnumerator();
        }
        
        public IDisposable Subscribe(Action<IReadOnlyCollection<TValue>> callback)
        {
            IDisposable disposable = SkipLastValueSubscribe(callback);
            callback?.Invoke(_hashSet);
            return disposable;
        }

        public IDisposable SkipLastValueSubscribe(Action<IReadOnlyCollection<TValue>> callback)
        {
            OnCollectionChanged += callback;
            return new CallbackDisposableHandler(() => Unsubscribe(callback));
        }

        public void Unsubscribe(Action<IReadOnlyCollection<TValue>> callback)
        {
            OnCollectionChanged -= callback;
        }

        protected void SendCallbacks()
        {
            OnCollectionChanged?.Invoke(_hashSet);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _hashSet.GetEnumerator();
        }
    }
}