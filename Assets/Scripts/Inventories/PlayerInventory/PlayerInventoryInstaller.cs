using Zenject;

namespace LNE.Inventories
{
  public class PlayerInventoryInstaller
    : MonoInstaller<PlayerInventoryInstaller>
  {
    public override void InstallBindings()
    {
      Container
        .Bind<PlayerInventoryPresenter>()
        .FromComponentInHierarchy()
        .AsSingle();
    }
  }
}
