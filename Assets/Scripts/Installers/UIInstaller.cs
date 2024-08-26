using Zenject;

public class UIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<HUDScreenPresenter>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<BasePopupView>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}