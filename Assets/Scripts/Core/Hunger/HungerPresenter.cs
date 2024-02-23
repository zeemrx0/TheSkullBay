using UnityEngine;

public class HungerPresenter : MonoBehaviour
{
  [SerializeField]
  private float _maxHunger = 100;

  [SerializeField]
  private float _hungerDecreaseRate = 1f;

  [SerializeField]
  private HungerView _view;

  private float _currentHunger;

  private void Start()
  {
    _currentHunger = _maxHunger;
  }

  private void Update()
  {
    DecreaseHunger(_hungerDecreaseRate * Time.fixedDeltaTime);
    _view.SetHungerSliderValue(_currentHunger / _maxHunger);
  }

  public void IncreaseHunger(float value)
  {
    _currentHunger += value;
    if (_currentHunger > _maxHunger)
    {
      _currentHunger = _maxHunger;
    }
  }

  public void DecreaseHunger(float value)
  {
    _currentHunger -= value;
    if (_currentHunger < 0)
    {
      _currentHunger = 0;
    }
  }
}
