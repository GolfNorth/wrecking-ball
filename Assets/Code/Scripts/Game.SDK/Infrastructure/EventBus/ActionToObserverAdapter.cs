using System;
using UnityEngine;

namespace Game.SDK.Infrastructure
{
    public class ActionToObserverAdapter<T> : IObserver<T>
    {
        private readonly Action<T> _onNext;

        public ActionToObserverAdapter(Action<T> onNext)
        {
            _onNext = onNext ?? throw new ArgumentNullException(nameof(onNext));
        }
        
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            Debug.LogException(error);
        }

        public void OnNext(T value)
        {
            _onNext(value);
        }
    }
}