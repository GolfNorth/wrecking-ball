using Game.SDK.Components;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Последовательность действий перед запуском игры
    /// </summary>
    public class Preloader : MonoBehaviour
    {
        [SerializeField]
        private SceneLoader sceneLoader;

        private async void Start()
        {
            await sceneLoader.LoadAsync();

            Destroy(sceneLoader);
            Destroy(this);
        }
    }
}