using UnityEngine;
using UnityEngine.UI;

namespace LNE.Interactions
{
  public class PlayerInteractionView : MonoBehaviour
  {
    [SerializeField]
    private Button _interactButton;

    public void ShowInteractButton()
    {
      _interactButton.gameObject.SetActive(true);
    }

    public void HideInteractButton()
    {
      _interactButton.gameObject.SetActive(false);
    }
  }
}
