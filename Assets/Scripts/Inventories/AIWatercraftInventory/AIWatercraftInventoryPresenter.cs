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

      Size = _aiInventoryData.Size;
      MaxWeight = _aiInventoryData.MaxWeight;
      _slotModels = _aiInventoryData.SlotModels;
      _gameResourcesModel = _aiInventoryData.CurrenciesModel;
    }

    public CollectableLootModel GetCollectableLootModel()
    {
      CollectableLootModel collectableLootModel = new CollectableLootModel
      {
        SlotModel = _slotModels,
        CurrenciesModel = _gameResourcesModel
      };

      return collectableLootModel;
    }
  }
}
