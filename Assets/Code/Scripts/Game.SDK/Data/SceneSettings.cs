using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Game.SDK.Components
{
    /// <summary>
    /// Параметры для загрузки сцены
    /// </summary>
    [Serializable]
    public class SceneSettings
    {
        [SerializeField]
        private string sceneName;

        [SerializeField]
        private AssetReference sceneRef;

        public object Key => string.IsNullOrEmpty(sceneName) ? sceneRef : sceneName;

        [field: SerializeField]
        public LoadSceneMode LoadMode { get; private set; }

        [field: SerializeField]
        public bool ActivateOnLoad { get; private set; }
    }
}