using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Inventories
{
  public class InventorySlotModel
  {
    private InventoryItemData _itemData;
    private int _quantity;

    public InventorySlotModel(InventoryItemData itemData, int quantity)
    {
      _itemData = itemData;
      _quantity = quantity;

      if (_quantity > _itemData.GetMaxStack())
      {
        throw new System.Exception(
          ExceptionMessage.ItemQuantityCannotBeGreaterThanMaxStack
        );
      }
    }

    public string GetItemName()
    {
      return _itemData.GetName();
    }

    public string GetItemDescription()
    {
      return _itemData.GetDescription();
    }

    public InventoryItemData GetItemData()
    {
      return _itemData;
    }

    public Sprite GetItemIcon()
    {
      return _itemData.GetIcon();
    }

    public int GetQuantity()
    {
      return _quantity;
    }

    public void SetQuantity(int quantity)
    {
      _quantity = quantity;
    }

    public ItemType GetItemType()
    {
      return _itemData.GetItemType();
    }

    // public ConsumableData GetConsumableData()
    // {
    //   return _itemData.GetConsumableData();
    // }
  }
}
