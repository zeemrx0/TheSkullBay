using LNE.Utilities.Constants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LNE.Combat.Abilities
{
  public class PlayerBoatAbilitiesView : BoatAbilitiesView
  {
    public void SetAbilityButtonIconActive(int index, bool active)
    {
      _abilityButtons[index].transform
        .Find(GameObjectName.Border)
        .Find(GameObjectName.Icon)
        .gameObject.SetActive(active);
    }

    public void SetAbilityButtonIcon(int index, Sprite icon)
    {
      _abilityButtons[index].transform
        .Find(GameObjectName.Border)
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
        .Find(GameObjectName.Border)
        .Find(GameObjectName.Overlay)
        .GetComponent<Image>()
        .fillAmount = initialTime == 0 ? 0 : (remainingTime / initialTime);

      _abilityButtons[index].transform
        .Find(GameObjectName.Border)
        .Find(GameObjectName.CooldownTimeText)
        .GetComponent<TextMeshProUGUI>()
        .text = initialTime == 0 ? "" : Mathf.Ceil(remainingTime).ToString();
    }
  }
}
