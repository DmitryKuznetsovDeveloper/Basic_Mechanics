using Model;
using Zenject;

namespace Installers
{
    public class GameManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<RestartGame>().AsCached().NonLazy();
        }
    }
}