using Cysharp.Threading.Tasks;
using Game.SDK.Services.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Game.SDK.Components
{
    /// <summary>
    /// Компонент загрузки сцен
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private bool loadOnStart;

        [SerializeField]
        private SceneSettings[] scenes;

        private ISceneService _sceneService;

        [Inject]
        public void Construct(ISceneService sceneService)
        {
            _sceneService = sceneService;
        }

        private async void Start()
        {
            if (!loadOnStart)
                return;

            await LoadAsync();
        }

        public async void Load()
        {
            await LoadAsync();
        }

        public async UniTask LoadAsync()
        {
            await Addressables.InitializeAsync();

            var loadSceneTasks = scenes.Select(x => _sceneService.LoadSceneAsync(x.Key, x.LoadMode, x.ActivateOnLoad));

            await UniTask.WhenAll(loadSceneTasks);
        }
    }
}