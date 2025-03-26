using UnityEngine;

namespace Game.Presentation.Views
{
    /// <summary>
    /// Визуализатор позиции в башне
    /// </summary>
    public class ZonePosition : MonoBehaviour
    {
        [field: SerializeField]
        public Vector2Int Position { get; private set; }
    }
}