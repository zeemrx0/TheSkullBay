using System;
using UnityEngine;

namespace LNE.Inventories
{
  public abstract class InventoryPresenter : MonoBehaviour
  {
    public event Action OnInventoryChanged;

    protected InventoryView _view;
    protected InventoryModel _model = new InventoryModel();

    protected virtual void Awake() { }

    public InventorySlotModel GetInventorySlot(int index)
    {
      return _model.Slots[index];
    }

    public void SetQuantity(int index, int quantity)
    {
      _model.Slots[index].Quantity = quantity;
      if (_model.Slots[index].Quantity == 0)
      {
        _model.Slots[index] = null;
      }

      _view.Draw(_model.Slots, _model.Currencies);
    }

    public void AddItem(InventoryItemData itemData, int quantity)
    {
      if (itemData == null || quantity == 0)
      {
        return;
      }

      int remainingQuantity = quantity;

      for (int i = 0; i < _model.Slots.Length; i++)
      {
        if (remainingQuantity == 0)
        {
          break;
        }

        if (_model.Slots[i] == null)
        {
          int addQuantity = Math.Min(remainingQuantity, itemData.MaxStack);
          _model.Slots[i] = new InventorySlotModel(itemData, addQuantity);
          remainingQuantity -= addQuantity;
        }
        else
        {
          if (
            _model.Slots[i].ItemData == itemData
            && _model.Slots[i].Quantity < itemData.MaxStack
          )
          {
            int addQuantity = Math.Min(
              remainingQuantity,
              itemData.MaxStack - _model.Slots[i].Quantity
            );
            _model.Slots[i] = new InventorySlotModel(
              itemData,
              _model.Slots[i].Quantity + addQuantity
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

      if (_model.Slots[position] == null)
      {
        _model.Slots[position] = new InventorySlotModel(itemData, quantity);
      }

      OnInventoryChanged?.Invoke();
    }

    public void AddCurrencies(CurrenciesModel currenciesModel)
    {
      _model.Currencies.Add(currenciesModel);
      OnInventoryChanged?.Invoke();
    }

    public void SubtractCurrencies(CurrenciesModel currenciesModel)
    {
      _model.Currencies.Subtract(currenciesModel);
      OnInventoryChanged?.Invoke();
    }

    public void Show()
    {
      _view.Show();
      _view.Draw(_model.Slots, _model.Currencies);
    }

    public void Hide()
    {
      _view.Hide();
    }
  }
}
