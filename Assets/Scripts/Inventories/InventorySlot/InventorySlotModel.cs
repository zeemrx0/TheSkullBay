using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Inventories
{
  public class InventorySlotModel
  {
    public int Quantity { get; set; }
    public InventoryItemData ItemData { get; }

    public InventorySlotModel(InventoryItemData itemData, int quantity)
    {
      ItemData = itemData;
      Quantity = quantity;

      if (Quantity > ItemData.MaxStack)
      {
        throw new System.Exception(
          ExceptionMessage.ItemQuantityCannotBeGreaterThanMaxStack
        );
      }
    }

    public string Name => ItemData.Name;
    public string Description => ItemData.Description;
    public Sprite Icon => ItemData.Icon;
  }
}
