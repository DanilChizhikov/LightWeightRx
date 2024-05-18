using System;
using System.Threading;

namespace MbsCore.LightWeightRx
{
    internal sealed class ObserverNode<T> : IObserver<T>, IDisposable
    {
        private readonly IObserver<T> _observer;

        private CallbackProperty<T> _property;
        
        private ObserverNode<T> Previous { get; set; }
        public ObserverNode<T> Next { get; set; }
        
        public ObserverNode(CallbackProperty<T> property, IObserver<T> observer)
        {
            _observer = observer;
            _property = property;
            if (property.CurrentNode == null)
            {
                property.SetCurrentNodeWithLock(this);
            }
            else
            {
                ObserverNode<T> lastNode = property.CurrentNode.Previous ?? property.CurrentNode;
                lastNode.Next = this;
                Previous = lastNode;
                property.CurrentNode.Previous = this;
            }
        }
        
        public void OnCompleted()
        {
            _observer.OnCompleted();
        }

        public void OnError(Exception error)
        {
            _observer.OnError(error);
        }

        public void OnNext(T value)
        {
            _observer.OnNext(value);
        }

        public void Dispose()
        {
            CallbackProperty<T> property = Interlocked.Exchange(ref _property, null);
            if (property == null)
            {
                return;
            }

            lock (property)
            {
                if (property.CurrentNode == this)
                {
                    if (Previous == null || Next == null)
                    {
                        property.SetCurrentNode(null);
                    }
                    else
                    {
                        ObserverNode<T> root = Next;
                        if (root.Next == null)
                        {
                            root.Previous = null;
                        }
                        else
                        {
                            root.Previous = Previous;
                        }

                        property.SetCurrentNode(root);
                    }
                }
                else
                {
                    Previous!.Next = Next;
                    if (Next != null)
                    {
                        Next.Previous = Previous;
                    }
                    else
                    {
                        property.CurrentNode!.Previous = Previous;
                    }
                }
            }
        }
    }
}