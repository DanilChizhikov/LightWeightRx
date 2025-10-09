using System;
using System.Collections.Generic;
using System.Threading;

namespace MbsCore.LightWeightRx
{
    public class CallbackProperty<TValue> : ICallbackProperty<TValue>
    {
        private readonly IEqualityComparer<TValue> _equalityComparer;
        
        private TValue _currentValue;

        public TValue Value
        {
            get => _currentValue;
            set => SetValue(value);
        }
        
        internal ObserverNode<TValue> CurrentNode { get; private set; }

        public CallbackProperty() : this(default)
        {
        }
        
        public CallbackProperty(TValue defaultValue) : this(defaultValue, EqualityComparer<TValue>.Default)
        {
        }

        public CallbackProperty(TValue defaultValue, IEqualityComparer<TValue> equalityComparer)
        {
            _currentValue = defaultValue;
            _equalityComparer = equalityComparer ?? new DefaultEqualityComparer<TValue>();
        }
        
        public IDisposable Subscribe(IObserver<TValue> observer)
        {
            lock (this)
            {
                observer.OnNext(Value);
                var node = new ObserverNode<TValue>(this, observer);
                return node;
            }
        }

        internal void SetCurrentNodeWithLock(ObserverNode<TValue> value)
        {
            lock (this)
            {
                SetCurrentNode(value);
            }
        }
        
        internal void SetCurrentNode(ObserverNode<TValue> value)
        {
            CurrentNode = value;
        }
        
        protected ref TValue GetValueRef() => ref _currentValue;

        protected virtual void ValueChanged(TValue value)
        {
        }
        
        private void SetValue(TValue value)
        {
            lock (this)
            {
                if (CanSetValue(value))
                {
                    _currentValue = value;
                    ValueChanged(_currentValue);
                    Notify();
                }
            }
        }

        private bool CanSetValue(TValue value) => !_equalityComparer.Equals(value);

        private void Notify()
        {
            ObserverNode<TValue> root = CurrentNode;
            if (root == null)
            {
                return;
            }
            
            ObserverNode<TValue> node = Volatile.Read(ref root);
            ObserverNode<TValue> last = node?.Previous;
            for (; node != null; node = node.Next)
            {
                TryNotify(node);
                if (node == last)
                {
                    break;
                }
            }
        }

        private void TryNotify(ObserverNode<TValue> node)
        {
            bool wasException = false;
            try
            {
                node.OnNext(Value);
            }
            catch (Exception exception)
            {
                node.OnError(exception);
                wasException = true;
            }
            finally
            {
                if (!wasException)
                {
                    node.OnCompleted();
                }
            }
        }
    }
}