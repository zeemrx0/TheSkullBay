using Zenject;

namespace LNE.Core
{
  public class AdMobInstaller : MonoInstaller<AdMobInstaller>
  {
    public override void InstallBindings()
    {
      Container
        .Bind<AdMobPresenter>()
        .FromComponentInHierarchy()
        .AsSingle();
    }
  }
}
