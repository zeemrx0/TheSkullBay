using System;
using UnityEngine;

namespace LNE.Inventories
{
  public abstract class InventoryPresenter : MonoBehaviour
  {
    public event Action OnInventoryChanged;

    [field: SerializeField]
    public int Size { get; protected set; } = 10;

    [field: SerializeField]
    public int MaxWeight { get; protected set; } = 100;

    protected InventoryView _view;
    protected InventorySlotModel[] _slotModels;
    protected CurrenciesModel _gameResourcesModel;

    protected virtual void Awake()
    {
      _slotModels = new InventorySlotModel[Size];
      _gameResourcesModel = new CurrenciesModel();
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

      _view.Draw(_slotModels, _gameResourcesModel);
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

    public void AddCurrencies(CurrenciesModel currenciesModel)
    {
      _gameResourcesModel.Add(currenciesModel);
      OnInventoryChanged?.Invoke();
    }

    public void SubtractCurrencies(CurrenciesModel currenciesModel)
    {
      _gameResourcesModel.Subtract(currenciesModel);
      OnInventoryChanged?.Invoke();
    }

    public void Show()
    {
      _view.Show();
      _view.Draw(_slotModels, _gameResourcesModel);
    }

    public void Hide()
    {
      _view.Hide();
    }
  }
}
