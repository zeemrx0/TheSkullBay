using UnityEngine;

namespace LNE.Inventories
{
  [CreateAssetMenu(
    fileName = "_InventoryData",
    menuName = "Inventories/AI Inventory",
    order = 0
  )]
  public class AIInventoryData : ScriptableObject
  {
    [field: SerializeField]
    public int Size { get; private set; } = 10;

    [field: SerializeField]
    public int MaxWeight { get; private set; } = 100;

    [field: SerializeField]
    public InventorySlotModel[] SlotModels { get; private set; } =
      new InventorySlotModel[10];

    [field: SerializeField]
    public CurrenciesModel CurrenciesModel { get; private set; } =
      new CurrenciesModel();
  }
}
