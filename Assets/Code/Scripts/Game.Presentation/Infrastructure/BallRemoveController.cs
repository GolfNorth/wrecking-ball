using System;
using Game.Presentation.Events;
using Game.Presentation.Views;
using Game.SDK.Infrastructure.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

namespace Game.Presentation.Components
{
    public class BallRemoveController : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particles;

        [SerializeField]
        private Transform particlesParent;

        private IObjectPool<ParticleSystem> _particlePool;

        private IObjectPool<BallView> _ballPool;

        private readonly CompositeDisposable _disposables = new();

        private void Awake()
        {
            _particlePool = new ObjectPool<ParticleSystem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                OnDestroyPoolObject);
        }

        private void OnDestroyPoolObject(ParticleSystem ps)
        {
            Destroy(ps.gameObject);
        }

        private void OnReturnedToPool(ParticleSystem ps)
        {
        }

        private void OnTakeFromPool(ParticleSystem ps)
        {
            ps.transform.localScale = Vector3.one;
        }

        private ParticleSystem CreatePooledItem()
        {
            return Instantiate(particles);
        }

        [Inject]
        public void Construct(IEventBus eventBus, IObjectPool<BallView> ballPool)
        {
            _ballPool = ballPool;

            eventBus.Subscribe<BallReleasedEvent>(OnBallReleased).AddTo(_disposables);
        }

        private void OnDisable()
        {
            _disposables.Clear();
        }

        private void OnBallReleased(BallReleasedEvent e)
        {
            if (e.GotPoints)
            {
                var ps = _particlePool.Get();

                ps.transform.position = e.BallView.transform.position;
                ps.Play();

                Observable.Timer(TimeSpan.FromSeconds(1))
                    .Subscribe(_ => _particlePool.Release(ps))
                    .AddTo(_disposables);
            }

            _ballPool.Release(e.BallView);
        }
    }
}