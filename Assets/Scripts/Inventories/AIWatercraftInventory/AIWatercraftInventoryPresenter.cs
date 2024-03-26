using LNE.Inventories.Loots;
using UnityEngine;

namespace LNE.Inventories
{
  public class AIWatercraftInventoryPresenter : InventoryPresenter
  {
    [SerializeField]
    private AIInventoryData _aiInventoryData;

    protected override void Awake()
    {
      base.Awake();

      _model = _aiInventoryData.InventoryModel.Clone() as InventoryModel;
    }

    public CollectableLootModel GetCollectableLootModel()
    {
      CollectableLootModel collectableLootModel = new CollectableLootModel
      {
        SlotModel = _model.Slots,
        CurrenciesModel = _model.Currencies
      };

      return collectableLootModel;
    }
  }
}
