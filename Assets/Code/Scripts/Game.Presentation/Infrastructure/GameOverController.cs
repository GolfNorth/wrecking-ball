using Game.Events;
using Game.SDK.Components;
using Game.SDK.Infrastructure.Interfaces;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Presentation.Infrastructure
{
    /// <summary>
    /// Контроллер окончания игры
    /// </summary>
    public class GameOverController : MonoBehaviour
    {
        [SerializeField]
        private SceneLoader nextSceneLoader;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            eventBus.Subscribe<GameOverEvent>(OnGameOver).AddTo(_disposables);
        }

        private void OnDisable()
        {
            _disposables.Clear();
        }

        private void OnGameOver(GameOverEvent e)
        {
            nextSceneLoader.Load();
        }
    }
}