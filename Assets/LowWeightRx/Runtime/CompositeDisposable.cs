using System;
using System.Collections.Generic;

namespace MbsCore.LowWeightRx
{
    public sealed class CompositeDisposable : IDisposable
    {
        private readonly HashSet<IDisposable> _disposables;

        public int Count => _disposables.Count;
        public bool IsDisposed { get; private set; }

        public CompositeDisposable()
        {
            _disposables = new HashSet<IDisposable>();
            IsDisposed = false;
        }

        public CompositeDisposable(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(capacity.ToString());
            }

            _disposables = new HashSet<IDisposable>(capacity);
            IsDisposed = false;
        }

        public CompositeDisposable(params IDisposable[] disposables) : this(disposables.Length)
        {
            AddRange(disposables);
        }

        public CompositeDisposable(IEnumerable<IDisposable> disposables) : this()
        {
            AddRange(disposables);
        }

        public void Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void AddRange(IEnumerable<IDisposable> disposables)
        {
            foreach (var disposable in disposables)
            {
                Add(disposable);
            }
        }

        public void Remove(IDisposable disposable)
        {
            _disposables.Remove(disposable);
        }

        public void Clear(bool quietly = false)
        {
            if (quietly)
            {
                _disposables.Clear();
            }
            else
            {
                Dispose();
            }
        }

        public void Reset()
        {
            Clear(true);
            IsDisposed = false;
        }
        
        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            
            foreach (var disposable in _disposables)
            {
                disposable?.Dispose();
            }
            
            _disposables.Clear();
            IsDisposed = true;
        }
    }
}