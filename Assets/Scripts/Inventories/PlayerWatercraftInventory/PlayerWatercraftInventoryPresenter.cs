using LNE.Core;
using Zenject;

namespace LNE.Inventories
{
  public class PlayerWatercraftInventoryPresenter : InventoryPresenter
  {
    private GameCorePresenter _gameCorePresenter;

    public void SetInventoryModel(InventoryModel inventoryModel)
    {
      _model = inventoryModel;
    }

    [Inject]
    public void Construct(GameCorePresenter gameCorePresenter)
    {
      _gameCorePresenter = gameCorePresenter;
      _gameCorePresenter.OnGameOver += HandleGameOver;
    }

    protected override void Awake()
    {
      base.Awake();
      _view = GetComponent<PlayerWatercraftInventoryView>();
    }

    public InventoryModel InventoryModel => _model;

    private void HandleGameOver()
    {
      _gameCorePresenter.SetGoldAmount(_model.Currencies.Gold);
    }
  }
}
