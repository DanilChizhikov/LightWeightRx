using System;
using System.Collections;
using System.Collections.Generic;

namespace MbsCore.LightWeightRx
{
    public class CallbackList<TValue> : IReadOnlyCallbackList<TValue>, IList<TValue>, IList
    {
        private event Action<IReadOnlyList<TValue>> OnListChanged; 
        
        private readonly List<TValue> _list;

        public int Count => _list.Count;

        public int Capacity
        {
            get => _list.Capacity;
            set => _list.Capacity = value;
        }
        public TValue this[int index]
        {
            get => _list[index];
            set
            {
                _list[index] = value;
                SendCallbacks();
            }
        }
        
        object IList.this[int index]
        {
            get => this[index];
            set
            {
                if (value is not TValue item)
                {
                    throw new ArrayTypeMismatchException();
                }
                
                this[index] = item;
            }
        }
        bool ICollection.IsSynchronized { get; }
        object ICollection.SyncRoot { get; }
        bool IList.IsFixedSize { get; }
        bool IList.IsReadOnly { get; }
        bool ICollection<TValue>.IsReadOnly { get; }

        public CallbackList()
        {
            _list = new List<TValue>();
        }

        public CallbackList(int capacity)
        {
            _list = new List<TValue>(capacity);
        }

        public CallbackList(IEnumerable<TValue> collection)
        {
            _list = new List<TValue>(collection);
        }

        public int IndexOf(TValue item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, TValue item)
        {
            _list.Insert(index, item);
            SendCallbacks();
        }

        public void Remove(object value)
        {
            if (value is TValue item)
            {
                Remove(item);
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                return;
            }
            
            _list.RemoveAt(index);
            SendCallbacks();
        }
        
        public IEnumerator<TValue> GetEnumerator()
        {
            return _list.GetEnumerator();
        }
        
        public bool Contains(TValue item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(TValue[] array)
        {
            _list.CopyTo(array);
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }
        
        public void CopyTo(Array array, int index)
        {
            if (array is TValue[] items)
            {
                _list.CopyTo(items, index);
            }
        }

        public void Add(TValue value)
        {
            _list.Add(value);
            SendCallbacks();
        }

        public bool Remove(TValue value)
        {
            if (!_list.Remove(value))
            {
                return false;
            }
            
            SendCallbacks();
            return true;
        }

        public int Add(object value)
        {
            if (value is TValue item)
            {
                Add(item);
                return Count - 1;
            }

            return -1;
        }

        public void Clear()
        {
            if (Count <= 0)
            {
                return;
            }
            
            _list.Clear();
            SendCallbacks();
        }

        public bool Contains(object value)
        {
            return value is TValue item && Contains(item);
        }

        public int IndexOf(object value)
        {
            if (value is TValue item)
            {
                return IndexOf(item);
            }

            return -1;
        }

        public void Insert(int index, object value)
        {
            if (value is TValue item)
            {
                Insert(index, item);
            }
        }

        public TValue[] ToArray() => _list.ToArray();
        
        protected void SendCallbacks()
        {
            OnListChanged?.Invoke(_list);
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}