using System;
using System.Collections;
using System.Collections.Generic;

namespace MbsCore.LightWeightRx
{
    public class CallbackStack<TValue> : IReadOnlyCallbackCollection<TValue>, ICollection
    {
        private event Action<IReadOnlyCollection<TValue>> OnCollectionChanged; 
        
        private readonly Stack<TValue> _stack;

        public int Count => _stack.Count;
        
        bool ICollection.IsSynchronized { get; }
        object ICollection.SyncRoot { get; }

        public CallbackStack()
        {
            _stack = new Stack<TValue>();
        }
        
        public CallbackStack(int capacity)
        {
            _stack = new Stack<TValue>(capacity);
        }

        public CallbackStack(IEnumerable<TValue> collection)
        {
            _stack = new Stack<TValue>(collection);
        }

        public bool Contains(TValue item)
        {
            return _stack.Contains(item);
        }
        
        public void CopyTo(Array array, int index)
        {
            if (array is TValue[] items)
            {
                CopyTo(array, index);
            }
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            _stack.CopyTo(array, arrayIndex);
        }

        public bool TryPeek(out TValue value)
        {
            value = default;
            if (Count <= 0)
            {
                return false;
            }
            
            value = _stack.Peek();
            return true;
        }

        public TValue Peek()
        {
            return _stack.Peek();
        }
        
        public bool TryPop(out TValue value)
        {
            value = default;
            if (Count <= 0)
            {
                return false;
            }
            
            value = _stack.Pop();
            return true;
        }
        
        public TValue Pop()
        {
            if (TryPop(out TValue value))
            {
                SendCallbacks();
            }

            return value;
        }
        
        public void Push(TValue item)
        {
            _stack.Push(item);
            SendCallbacks();
        }
        
        public void Clear()
        {
            if (Count <= 0)
            {
                return;
            }
            
            _stack.Clear();
            SendCallbacks();
        }

        public TValue[] ToArray() => _stack.ToArray();
        
        public IEnumerator<TValue> GetEnumerator()
        {
            return _stack.GetEnumerator();
        }

        protected void SendCallbacks()
        {
            OnCollectionChanged?.Invoke(_stack);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _stack.GetEnumerator();
        }
    }
}