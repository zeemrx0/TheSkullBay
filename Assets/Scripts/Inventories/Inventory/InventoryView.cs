using UnityEngine;

namespace LNE.Inventories
{
  public class InventoryView : MonoBehaviour
  {
    [SerializeField]
    private GameObject _inventorySlotPrefab;

    [SerializeField]
    private Transform _inventoryContainerTransform;

    public void Show()
    {
      gameObject.SetActive(true);
    }

    public void Hide()
    {
      gameObject.SetActive(false);
    }

    public void Draw(InventorySlotModel[] _slots)
    {
      if (gameObject.activeSelf == false)
      {
        return;
      }

      foreach (Transform child in _inventoryContainerTransform)
      {
        Destroy(child.gameObject);
      }

      for (int i = 0; i < _slots.Length; i++)
      {
        InventorySlotModel slot = _slots[i];

        GameObject slotObject = Instantiate(
          _inventorySlotPrefab,
          _inventoryContainerTransform
        );

        // InventorySlotView slotView =
        //   slotObject.GetComponent<InventorySlotView>();

        // slotView.Draw(slot);
      }
    }
  }
}
