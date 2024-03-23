using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Inventories
{
  public class CollectableLoot : MonoBehaviour
  {
    private InventorySlotModel[] _slotModel;
    private CurrenciesModel _currenciesModel;

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag(TagName.Player))
      {
        PlayerWatercraftInventoryPresenter inventoryPresenter =
          other.GetComponent<PlayerWatercraftInventoryPresenter>();

        foreach (InventorySlotModel slotModel in _slotModel)
        {
          inventoryPresenter.AddItem(slotModel.ItemData, slotModel.Quantity);
        }

        // TODO: Add currencies to the player's inventory

        Destroy(gameObject);
      }
    }
  }
}
