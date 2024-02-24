using TMPro;
using UnityEngine;

namespace LNE.Core
{
  public class GameCoreView : MonoBehaviour
  {
    [SerializeField]
    private GameObject _gameOverCanvas;

    [SerializeField]
    private TextMeshProUGUI _goldAmountText;

    public void ShowGameOverPanel()
    {
      _gameOverCanvas.SetActive(true);
    }

    public void SetGoldAmount(int amount)
    {
      _goldAmountText.text = amount.ToString();
    }
  }
}
