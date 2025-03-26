using Game.ViewModels;

namespace Game.Infrastructure
{
    /// <summary>
    /// Фабрика башни
    /// </summary>
    public class TowerFactory : ITowerFactory
    {
        private TowerVIewModel _instance;
        
        public TowerVIewModel Get()
        {
            return _instance ??= new TowerVIewModel();
        }
    }
}