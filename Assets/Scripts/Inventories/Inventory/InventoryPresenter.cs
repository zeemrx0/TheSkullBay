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

    private InventorySlotModel[] _slots;

    private void Awake()
    {
      _slots = new InventorySlotModel[Size];
    }

    public InventorySlotModel GetInventorySlot(int index)
    {
      return _slots[index];
    }

    public void SetQuantity(int index, int quantity)
    {
      _slots[index].SetQuantity(quantity);
      if (_slots[index].GetQuantity() == 0)
      {
        _slots[index] = null;
      }

      _inventoryView.Draw(_slots);
    }

    public void AddItem(InventoryItemData itemData, int quantity)
    {
      if (itemData == null || quantity == 0)
      {
        return;
      }

      int remainingQuantity = quantity;

      for (int i = 0; i < _slots.Length; i++)
      {
        if (remainingQuantity == 0)
        {
          break;
        }

        if (_slots[i] == null)
        {
          int addQuantity = Math.Min(remainingQuantity, itemData.GetMaxStack());
          _slots[i] = new InventorySlotModel(itemData, addQuantity);
          remainingQuantity -= addQuantity;
        }
        else
        {
          if (
            _slots[i].GetItemData() == itemData
            && _slots[i].GetQuantity() < itemData.GetMaxStack()
          )
          {
            int addQuantity = Math.Min(
              remainingQuantity,
              itemData.GetMaxStack() - _slots[i].GetQuantity()
            );
            _slots[i] = new InventorySlotModel(
              itemData,
              _slots[i].GetQuantity() + addQuantity
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

      if (_slots[position] == null)
      {
        _slots[position] = new InventorySlotModel(itemData, quantity);
      }

      OnInventoryChanged?.Invoke();
    }

    public void Show()
    {
      _inventoryView.Show();
      _inventoryView.Draw(_slots);
    }

    public void Hide()
    {
      _inventoryView.Hide();
    }
  }
}
