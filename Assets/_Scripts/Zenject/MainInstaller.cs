using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<World>().FromComponentInHierarchy().AsSingle().NonLazy();

        Container.Bind<BlockRegistry>().AsSingle().NonLazy();
    }
}