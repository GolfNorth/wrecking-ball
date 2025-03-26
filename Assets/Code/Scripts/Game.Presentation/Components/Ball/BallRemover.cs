using System;
using Game.Events;
using Game.Presentation.Events;
using Game.Presentation.Views;
using Game.SDK.Infrastructure.Interfaces;
using Game.ViewModels;
using UniRx;
using VContainer;

namespace Game.Presentation.Components
{
    public class BallRemover : BaseViewComponent<BallView, BallViewModel>
    {
        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;

            _eventBus.Subscribe<BallDestroyedEvent>(OnBallDestroyed).AddTo(Disposables);
            _eventBus.Subscribe<BallFallenEvent>(OnBallFallen).AddTo(Disposables);
        }

        private void OnEnable()
        {
            Disposables.Clear();

            if (_eventBus != null)
            {
                _eventBus.Subscribe<BallDestroyedEvent>(OnBallDestroyed).AddTo(Disposables);
                _eventBus.Subscribe<BallFallenEvent>(OnBallFallen).AddTo(Disposables);
            }
        }

        private void OnDisable()
        {
            Disposables.Clear();
        }

        private void OnBallFallen(BallFallenEvent e)
        {
            if (e.BallViewModel != DataContext)
                return;

            _eventBus.Publish(new BallReleasedEvent(View, false));
        }

        private void OnBallDestroyed(BallDestroyedEvent e)
        {
            if (e.BallViewModel != DataContext)
                return;

            _eventBus.Publish(new BallReleasedEvent(View, true));
        }
    }
}