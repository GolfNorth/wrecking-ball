using System.Collections;
using Game.Events;
using Game.Presentation.Views;
using Game.SDK.Infrastructure.Interfaces;
using Game.ViewModels;
using UnityEngine;
using VContainer;

namespace Game.Presentation.Components
{
    /// <summary>
    /// Компонент фиксации колизии
    /// </summary>
    public class BallCollision : BaseViewComponent<BallView, BallViewModel>
    {
        [SerializeField]
        private string zoneTag = "Zone";

        [SerializeField]
        private string fallTag = "Fall";

        [SerializeField]
        private string gameOverTag = "GameOver";

        [SerializeField, Tooltip("Задержка перед фиксацией коллизии")]
        private float detectionDelay = 0.2f;

        [SerializeField, Tooltip("Задержка перед фиксацией коллизии")]
        private float gameOverDelay = 2f;

        private Coroutine _detectionRoutine;

        private IEventBus _eventBus;

        private Collider2D _lastCollider;

        private float _cooldown;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(zoneTag) && !other.CompareTag(fallTag) && !other.CompareTag(gameOverTag))
                return;

            _lastCollider = other;

            if (_detectionRoutine == null)
            {
                _detectionRoutine = StartCoroutine(DetectCollision());
            }
            else
            {
                _cooldown += detectionDelay;
            }
        }

        private IEnumerator DetectCollision()
        {
            _cooldown = detectionDelay;

            while (_cooldown > 0)
            {
                var delay = _lastCollider.CompareTag(gameOverTag) ? gameOverDelay : detectionDelay;

                yield return new WaitForSeconds(delay);

                _cooldown -= detectionDelay;
            }

            if (_lastCollider.CompareTag(gameOverTag))
            {
                _eventBus.Publish(new BallOverFilledEvent());
            }
            else if (_lastCollider.CompareTag(zoneTag))
            {
                var zone = _lastCollider.gameObject.GetComponent<ZonePosition>();

                _eventBus.Publish(new BallAddedToTowerEvent(DataContext, zone.Position));
            }
            else if (_lastCollider.CompareTag(fallTag))
            {
                _eventBus.Publish(new BallFallenEvent(DataContext));
            }

            _detectionRoutine = null;
        }
    }
}