using System;

namespace Game.SDK.Infrastructure.Interfaces
{
    /// <summary>
    /// Шина событий
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Создать событие
        /// </summary>
        void Publish<TEvent>(TEvent eventMessage) where TEvent : class;

        /// <summary>
        /// Подписаться на событие
        /// </summary>
        IDisposable Subscribe<TEvent>(Action<TEvent> onNext) where TEvent : class;
    }
}