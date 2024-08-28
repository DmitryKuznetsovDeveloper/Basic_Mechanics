using DataConfigs;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameCoreInstaller", menuName = "Installers/GameCoreInstaller", order = 0)]
    public class GameCoreInstaller : ScriptableObjectInstaller<GameCoreInstaller>
    { 
        public GameLoop gameLoop;
        public override void InstallBindings()
        {
            Container.Bind<GameLoop>().FromInstance(gameLoop).AsCached().NonLazy();
        }
    }
}