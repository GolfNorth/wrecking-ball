using Game.Infrastructure;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using VContainer;

namespace Game.Presentation.UI
{
    /// <summary>
    /// Компонент визуализация наивысшего результата
    /// </summary>
    public class HighScoreView : MonoBehaviour
    {
        [SerializeField]
        private LocalizeStringEvent highScoreStringEvent;

        [SerializeField]
        private LocalizeStringEvent lastScoreStringEvent;

        private IHighScoreSystem _highScoreSystem;

        [Inject]
        public void Construct(IHighScoreSystem highScoreSystem)
        {
            _highScoreSystem = highScoreSystem;
        }

        private void Start()
        {
            highScoreStringEvent.StringReference.Add("0", new IntVariable()
            {
                Value = _highScoreSystem.HighScore
            });
            lastScoreStringEvent.StringReference.Add("0", new IntVariable()
            {
                Value = _highScoreSystem.LastScore
            });

            highScoreStringEvent.StringReference.RefreshString();
            lastScoreStringEvent.StringReference.RefreshString();
        }
    }
}