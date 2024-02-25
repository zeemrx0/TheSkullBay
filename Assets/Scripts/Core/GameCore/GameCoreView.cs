using TMPro;
using UnityEngine;

namespace LNE.Core
{
  public class GameCoreView : MonoBehaviour
  {
    [SerializeField]
    private GameObject _gameOverCanvas;

    [SerializeField]
    private TextMeshProUGUI _gameOverTitle;

    [SerializeField]
    private TextMeshProUGUI _goldAmountText;

    public void ShowGameOverPanel(string title)
    {
      _gameOverCanvas.SetActive(true);
      _gameOverTitle.text = title;
    }

    public void SetGoldAmount(int amount)
    {
      _goldAmountText.text = amount.ToString();
    }
  }
}
