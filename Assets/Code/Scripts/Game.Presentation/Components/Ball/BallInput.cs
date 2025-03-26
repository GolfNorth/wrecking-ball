using Game.Events;
using Game.Presentation.Views;
using Game.SDK.Infrastructure.Interfaces;
using Game.ViewModels;
using UnityEngine;
using VContainer;

namespace Game.Presentation.Components
{
    /// <summary>
    /// Компонент управления
    /// </summary>
    public class BallInput : BaseViewComponent<BallView, BallViewModel>
    {
        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Update()
        {
            if (!Input.GetMouseButton(0) || !View.IsKinematic.Value)
                return;

            View.IsKinematic.Value = false;
            View.transform.SetParent(null);

            _eventBus.Publish(new BallDroppedEvent(DataContext));
        }
    }
}