using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Inventories.Loots
{
  public class CollectableLoot : MonoBehaviour
  {
    public CollectableLootModel CollectableLootModel { get; set; }

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag(TagName.Player))
      {
        PlayerWatercraftInventoryPresenter inventoryPresenter =
          other.GetComponent<PlayerWatercraftInventoryPresenter>();

        Debug.Log(CollectableLootModel);

        foreach (InventorySlotModel slotModel in CollectableLootModel.SlotModel)
        {
          if (slotModel.ItemData == null)
          {
            continue;
          }

          inventoryPresenter.AddItem(slotModel.ItemData, slotModel.Quantity);
        }

        inventoryPresenter.AddCurrencies(CollectableLootModel.CurrenciesModel);

        Destroy(gameObject);
      }
    }
  }
}
