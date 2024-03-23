using UnityEngine;

namespace LNE.Inventories
{
  public class PlayerWatercraftInventoryPresenter : InventoryPresenter
  {
    protected override void Awake()
    {
      base.Awake();
      _view = GetComponent<PlayerWatercraftInventoryView>();
    }
  }
}
