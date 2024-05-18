using System;
using System.Threading;

namespace MbsCore.LightWeightRx
{
    public class CallbackProperty<TValue> : ICallbackProperty<TValue>
    {
        private TValue _value;

        public TValue Value
        {
            get => _value;
            set => SetValue(value);
        }
        
        internal ObserverNode<TValue> CurrentNode { get; private set; }

        public CallbackProperty() : this(default!)
        {
        }

        public CallbackProperty(TValue defaultValue)
        {
            _value = defaultValue;
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
        
        private void SetValue(TValue value)
        {
            lock (this)
            {
                if (CanSetValue(value))
                {
                    _value = value;
                    Notify();
                }
            }
        }
        
        private bool CanSetValue(TValue value)
        {
            if ((value == null && Value == null) ||
                (value != null && value.Equals(Value)))
            {
                return false;
            }

            return true;
        }

        private void Notify()
        {
            ObserverNode<TValue> root = CurrentNode;
            if (root == null)
            {
                return;
            }

            TryNotify(root);
            ObserverNode<TValue> node = Volatile.Read(ref root);
            ObserverNode<TValue> last = node?.Previous;
            while (node != null)
            {
                TryNotify(node);
                if (node == last)
                {
                    return;
                }
                
                node = node.Next;
            }
        }

        private void TryNotify(ObserverNode<TValue> node)
        {
            try
            {
                node.OnNext(Value);
            }
            catch (Exception exception)
            {
                node.OnError(exception);
            }
            finally
            {
                node.OnCompleted();
            }
        }
    }
}