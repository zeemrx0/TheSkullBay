using System;
using UnityEngine;

namespace LNE.Inventories
{
  public class InventoryPresenter : MonoBehaviour
  {
    public event Action OnInventoryChanged;

    [field: SerializeField]
    public int Size { get; private set; } = 10;

    [SerializeField]
    private InventoryView _inventoryView;

    private InventorySlotModel[] _slotModels;

    private void Awake()
    {
      _slotModels = new InventorySlotModel[Size];
    }

    public InventorySlotModel GetInventorySlot(int index)
    {
      return _slotModels[index];
    }

    public void SetQuantity(int index, int quantity)
    {
      _slotModels[index].Quantity = quantity;
      if (_slotModels[index].Quantity == 0)
      {
        _slotModels[index] = null;
      }

      _inventoryView.Draw(_slotModels);
    }

    public void AddItem(InventoryItemData itemData, int quantity)
    {
      if (itemData == null || quantity == 0)
      {
        return;
      }

      int remainingQuantity = quantity;

      for (int i = 0; i < _slotModels.Length; i++)
      {
        if (remainingQuantity == 0)
        {
          break;
        }

        if (_slotModels[i] == null)
        {
          int addQuantity = Math.Min(remainingQuantity, itemData.MaxStack);
          _slotModels[i] = new InventorySlotModel(itemData, addQuantity);
          remainingQuantity -= addQuantity;
        }
        else
        {
          if (
            _slotModels[i].ItemData == itemData
            && _slotModels[i].Quantity < itemData.MaxStack
          )
          {
            int addQuantity = Math.Min(
              remainingQuantity,
              itemData.MaxStack - _slotModels[i].Quantity
            );
            _slotModels[i] = new InventorySlotModel(
              itemData,
              _slotModels[i].Quantity + addQuantity
            );
            remainingQuantity -= addQuantity;
          }
        }
      }

      OnInventoryChanged?.Invoke();
    }

    public void InsertItem(
      InventoryItemData itemData,
      int quantity,
      int position
    )
    {
      if (itemData == null || quantity == 0)
      {
        return;
      }

      if (_slotModels[position] == null)
      {
        _slotModels[position] = new InventorySlotModel(itemData, quantity);
      }

      OnInventoryChanged?.Invoke();
    }

    public void Show()
    {
      _inventoryView.Show();
      _inventoryView.Draw(_slotModels);
    }

    public void Hide()
    {
      _inventoryView.Hide();
    }
  }
}
