using UnityEngine;
using UnityEngine.UI;

public class HungerView : MonoBehaviour
{
  [SerializeField]
  private Slider _slider;

  public void SetHungerSliderValue(float value)
  {
    _slider.value = value;
  }
}
