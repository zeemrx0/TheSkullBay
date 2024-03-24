using System;
using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Inventories
{
  [System.Serializable]
  public class InventorySlotModel : ICloneable
  {
    [field: SerializeField]
    public int Quantity { get; set; }

    [field: SerializeField]
    public InventoryItemData ItemData { get; }

    public InventorySlotModel(InventoryItemData itemData, int quantity)
    {
      if (itemData == null)
      {
        Quantity = 0;
        return;
      }

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

    public object Clone()
    {
      return new InventorySlotModel(ItemData, Quantity);
    }
  }
}
