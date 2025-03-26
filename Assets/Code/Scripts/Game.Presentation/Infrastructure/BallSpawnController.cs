using System;
using Game.Events;
using Game.Presentation.Views;
using Game.SDK.Infrastructure.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

namespace Game.Presentation.Infrastructure
{
    /// <summary>
    /// Контроллер спауна шаров
    /// </summary>
    public class BallSpawnController : MonoBehaviour
    {
        [SerializeField, Tooltip("Трансформ маятника")]
        private Transform pendulumTransform;

        private IObjectPool<BallView> _ballPool;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        public void Construct(IEventBus eventBus, IObjectPool<BallView> ballPool)
        {
            _ballPool = ballPool;

            eventBus.Subscribe<BallCreatedEvent>(OnBallCreated).AddTo(_disposables);
        }

        private void OnDisable()
        {
            _disposables.Clear();
        }

        private void OnBallCreated(BallCreatedEvent e)
        {
            var view = _ballPool.Get();
            view.IsKinematic.Value = true;
            view.transform.SetParent(pendulumTransform);
            view.transform.localScale = Vector3.one;
            view.transform.localPosition = Vector3.zero;
            view.Init(e.BallViewModel);
        }
    }
}