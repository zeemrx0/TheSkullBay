using TMPro;
using UnityEngine;

namespace LNE.Combat.Loots
{
  public class PlayerLootsView : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _goldAmountText;

    public void SetGoldAmount(int amount)
    {
      _goldAmountText.text = amount.ToString();
    }
  }
}
