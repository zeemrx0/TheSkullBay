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
    private Image _itemImage;

    [SerializeField]
    private GameObject _quantityGameObject;

    [SerializeField]
    private TextMeshProUGUI _quantityTMP;

    public void Draw(InventorySlotModel slot)
    {
      if (slot == null)
      {
        _containerImage.sprite = _emptyFrameSprite;
        _itemImage.gameObject.SetActive(false);
        _quantityGameObject.SetActive(false);
        return;
      }

      _itemImage.sprite = slot.Icon;
      _quantityTMP.text = slot.Quantity.ToString();

      _itemImage.gameObject.SetActive(true);
      _quantityGameObject.SetActive(true);
    }
  }
}
