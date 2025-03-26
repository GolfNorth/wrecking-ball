using System;
using System.Collections.Generic;
using Game.SDK.Infrastructure.Interfaces;
using UniRx;

namespace Game.SDK.Infrastructure
{
    /// <summary>
    /// Реализация <see cref="IEventBus"/> на основе библиотеки UniRx
    /// </summary>
    public class UniRxEventBus : IEventBus
    {
        private readonly Dictionary<Type, object> _eventSubjects = new();

        public void Publish<TEvent>(TEvent eventMessage) where TEvent : class
        {
            var subject = GetOrCreateSubject<TEvent>();

            subject.OnNext(eventMessage);
        }

        public IDisposable Subscribe<TEvent>(Action<TEvent> onNext) where TEvent : class
        {
            var subject = GetOrCreateSubject<TEvent>();
            var adapter = new ActionToObserverAdapter<TEvent>(onNext);

            return subject.Subscribe(adapter);
        }

        private ISubject<TEvent> GetOrCreateSubject<TEvent>() where TEvent : class
        {
            var eventType = typeof(TEvent);

            if (!_eventSubjects.TryGetValue(eventType, out var subject))
            {
                subject = new Subject<TEvent>();
                _eventSubjects[eventType] = subject;
            }

            return (ISubject<TEvent>)subject;
        }
    }
}