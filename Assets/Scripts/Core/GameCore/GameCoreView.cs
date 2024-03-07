using TMPro;
using UnityEngine;

namespace LNE.Core
{
  public class GameCoreView : MonoBehaviour
  {
    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private TextMeshProUGUI _goldAmountText;

    [SerializeField]
    private GameObject _tutorialPanel;

    public void ShowGameOverPanel()
    {
      _gameOverPanel.SetActive(true);
    }

    public void SetGoldAmount(int amount)
    {
      _goldAmountText.text = amount.ToString();
    }

    public void ShowTutorialPanel()
    {
      _tutorialPanel.SetActive(true);
    }

    public void HideTutorialPanel()
    {
      _tutorialPanel.SetActive(false);
    }

    public void ToggleTutorialPanel()
    {
      _tutorialPanel.SetActive(!_tutorialPanel.activeSelf);
    }
  }
}
