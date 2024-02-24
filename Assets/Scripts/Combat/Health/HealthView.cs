using UnityEngine;
using UnityEngine.UI;

namespace LNE.Combat
{
  public class HealthView : MonoBehaviour
  {
    [SerializeField]
    private Slider _slider;

    public void SetHealthSliderValue(float value)
    {
      _slider.value = value;
    }
  }
}
