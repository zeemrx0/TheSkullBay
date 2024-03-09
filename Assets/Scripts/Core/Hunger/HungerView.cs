using LNE.Core;
using LNE.Utilities.Constants;
using UnityEngine;
using UnityEngine.UI;

public class HungerView : MonoBehaviour
{
  private Slider _slider;

  private void Awake()
  {
    _slider = transform
      .GetComponentInChildren<Vehicle>()
      .transform.Find(GameObjectName.WatercraftCharacterInfoCanvas)
      .Find(GameObjectName.HungerBar)
      .GetComponent<Slider>();
  }

  public void SetHungerSliderValue(float value)
  {
    _slider.value = value;
  }
}
