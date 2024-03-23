using TMPro;
using UnityEngine;

namespace LNE.Inventories
{
  public abstract class InventoryView : MonoBehaviour
  {
    [SerializeField]
    private InventorySlot _inventorySlotPrefab;

    [SerializeField]
    private GameObject _inventoryCanvas;

    [SerializeField]
    private Transform _inventoryGrid;

    [SerializeField]
    private TextMeshProUGUI _goldText;

    public void Show()
    {
      _inventoryCanvas.SetActive(true);
    }

    public void Hide()
    {
      _inventoryCanvas.SetActive(false);
    }

    public void Draw(
      InventorySlotModel[] _slots,
      CurrenciesModel gameResourcesModel
    )
    {
      foreach (Transform child in _inventoryGrid)
      {
        Destroy(child.gameObject);
      }

      for (int i = 0; i < _slots.Length; i++)
      {
        InventorySlotModel slotModel = _slots[i];

        InventorySlot slot = Instantiate(_inventorySlotPrefab, _inventoryGrid);

        slot.Draw(slotModel);
      }

      _goldText.text = gameResourcesModel.Gold.ToString();
    }
  }
}
