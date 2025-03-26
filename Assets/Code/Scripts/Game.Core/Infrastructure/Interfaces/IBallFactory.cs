using Game.ViewModels;
using UnityEngine;

namespace Game.Infrastructure
{
    /// <summary>
    /// Интерфейс фабрики шаров
    /// </summary>
    public interface IBallFactory
    {
        BallViewModel Get(Color color);
    }
}