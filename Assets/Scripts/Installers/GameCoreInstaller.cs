using Input;
using Zenject;

namespace Installers
{
    public sealed class GameCoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameLoader>().AsSingle().NonLazy();
        }
    }
}