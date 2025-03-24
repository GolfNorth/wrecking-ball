using Cysharp.Threading.Tasks;
using Game.SDK.Components;
using Game.SDK.Services.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Game
{
    /// <summary>
    /// Последовательность действий перед запуском игры
    /// </summary>
    public class Preloader : MonoBehaviour
    {
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
            await Addressables.InitializeAsync();

            var loadSceneTasks = scenes.Select(x => _sceneService.LoadSceneAsync(x.Key, x.LoadMode, x.ActivateOnLoad));

            await UniTask.WhenAll(loadSceneTasks);

            Destroy(this);
        }
    }
}