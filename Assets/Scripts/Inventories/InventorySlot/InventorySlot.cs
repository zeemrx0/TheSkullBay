using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LNE.Inventories
{
  public class InventorySlot : MonoBehaviour
  {
    [SerializeField]
    private Image _containerImage;

    [SerializeField]
    private Sprite _emptyFrameSprite;

    [SerializeField]
    private GameObject _itemImageGameObject;

    [SerializeField]
    private GameObject _quantityGameObject;

    [SerializeField]
    private TextMeshProUGUI _quantityTMP;

    public void Draw(InventorySlotModel slot)
    {
      if (slot == null)
      {
        _containerImage.sprite = _emptyFrameSprite;
        return;
      }

      _itemImageGameObject.GetComponent<Image>().sprite = slot.GetItemIcon();
      _quantityTMP.text = slot.GetQuantity().ToString();

      _itemImageGameObject.SetActive(true);
      _quantityGameObject.SetActive(true);
    }
  }
}
