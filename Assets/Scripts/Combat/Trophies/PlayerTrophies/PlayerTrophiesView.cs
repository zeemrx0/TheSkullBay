using TMPro;
using UnityEngine;

namespace LNE.Combat.Trophies
{
  public class PlayerTrophiesView : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _goldAmountText;

    public void SetGoldAmount(int amount)
    {
      _goldAmountText.text = amount.ToString();
    }
  }
}
