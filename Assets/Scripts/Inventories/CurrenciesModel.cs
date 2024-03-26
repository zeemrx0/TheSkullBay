using System;
using UnityEngine;

namespace LNE.Inventories
{
  [System.Serializable]
  public class CurrenciesModel : ICloneable
  {
    [field: SerializeField]
    public int Gold { get; set; } = 0;

    public void Add(CurrenciesModel currenciesModel)
    {
      Gold += currenciesModel.Gold;
    }

    public void Subtract(CurrenciesModel currenciesModel)
    {
      Gold -= currenciesModel.Gold;
    }

    public object Clone()
    {
      return new CurrenciesModel { Gold = Gold };
    }
  }
}
