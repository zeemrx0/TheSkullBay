using UnityEngine;

namespace LNE.Inventories.Loots
{
  public class CollectableLootModel
  {
    [field: SerializeField]
    public InventorySlotModel[] SlotModel { get; set; }

    [field: SerializeField]
    public CurrenciesModel CurrenciesModel { get; set; }
  }
}
