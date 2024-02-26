using LNE.Utilities.Constants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LNE.Abilities
{
  public class PlayerBoatAbilitiesView : MonoBehaviour
  {
    [field: SerializeField]
    public RectTransform Origin { get; private set; }
    public Vector3 Direction { get; set; }

    [SerializeField]
    private RectTransform _rangeIndicator;

    [SerializeField]
    private RectTransform _circleIndicator;

    [SerializeField]
    private GameObject[] _abilityButtons;

    public void SetRangeIndicatorSize(Vector2 size)
    {
      _rangeIndicator.sizeDelta = size;
    }

    public void SetCircleIndicatorPosition(Vector3 position)
    {
      _circleIndicator.position = position;
    }

    public void SetCircleIndicatorSize(Vector2 size)
    {
      _circleIndicator.sizeDelta = size;
    }

    public void ShowCircleIndicator()
    {
      _circleIndicator.gameObject.SetActive(true);
    }

    public void HideCircleIndicator()
    {
      _circleIndicator.gameObject.SetActive(false);
    }

    public void ShowRangeIndicator()
    {
      _rangeIndicator.gameObject.SetActive(true);
    }

    public void HideRangeIndicator()
    {
      _rangeIndicator.gameObject.SetActive(false);
    }

    public void SetAbilityButtonIconActive(int index, bool active)
    {
      _abilityButtons[index].transform
        .Find(GameObjectName.Icon)
        .gameObject.SetActive(active);
    }

    public void SetAbilityButtonIcon(int index, Sprite icon)
    {
      _abilityButtons[index].transform
        .Find(GameObjectName.Icon)
        .GetComponent<Image>()
        .sprite = icon;
    }

    public void SetAbilityCooldownTime(
      int index,
      float remainingTime,
      float initialTime
    )
    {
      _abilityButtons[index].transform
        .Find(GameObjectName.Overlay)
        .GetComponent<Image>()
        .fillAmount = initialTime == 0 ? 0 : (remainingTime / initialTime);

      _abilityButtons[index].transform
        .Find(GameObjectName.CooldownTimeText)
        .GetComponent<TextMeshProUGUI>()
        .text = initialTime == 0 ? "" : Mathf.Ceil(remainingTime).ToString();
    }

    private void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawRay(Origin.position, Direction);
    }
  }
}
