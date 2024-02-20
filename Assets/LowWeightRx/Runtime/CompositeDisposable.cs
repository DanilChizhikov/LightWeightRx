using System;
using System.Collections.Generic;

namespace MbsCore.LowWeightRx
{
    public sealed class CompositeDisposable : IDisposable
    {
        private readonly HashSet<IDisposable> _disposables;

        public CompositeDisposable()
        {
            _disposables = new HashSet<IDisposable>();
        }

        public CompositeDisposable(params IDisposable[] disposables) : this()
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
        
        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            
            _disposables.Clear();
        }
    }
}