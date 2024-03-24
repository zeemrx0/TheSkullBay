using System;
using UnityEngine;

namespace LNE.Inventories
{
  [System.Serializable]
  public class InventoryModel : ICloneable
  {
    [field: SerializeField]
    public int Size { get; set; } = 10;

    [field: SerializeField]
    public int MaxWeight { get; set; } = 100;

    [field: SerializeField]
    public InventorySlotModel[] Slots { get; set; }

    [field: SerializeField]
    public CurrenciesModel Currencies { get; set; }

    public InventoryModel()
    {
      Slots = new InventorySlotModel[Size];
      Currencies = new CurrenciesModel();
    }

    public object Clone()
    {
      InventoryModel inventoryModel = new InventoryModel
      {
        Size = Size,
        MaxWeight = MaxWeight,
        Slots = new InventorySlotModel[Size],
        Currencies = (CurrenciesModel)Currencies.Clone()
      };

      for (int i = 0; i < Size; i++)
      {
        inventoryModel.Slots[i] = (InventorySlotModel)Slots[i].Clone();
      }

      return inventoryModel;
    }
  }
}
