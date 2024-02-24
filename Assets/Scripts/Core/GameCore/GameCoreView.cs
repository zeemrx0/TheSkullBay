using UnityEngine;

namespace LNE.Core
{
  public class GameCoreView : MonoBehaviour
  {
    [SerializeField]
    GameObject _gameOverCanvas;

    public void ShowGameOverPanel()
    {
      _gameOverCanvas.SetActive(true);
    }
  }
}
