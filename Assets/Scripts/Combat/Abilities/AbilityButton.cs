using LNE.Inputs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

namespace LNE.Combat.Abilities
{
  public class AbilityButton
    : MonoBehaviour,
      IPointerDownHandler,
      IPointerUpHandler,
      IDragHandler
  {
    private AbilityData _abilityData;
    private PlayerBoatAbilitiesPresenter _playerBoatAbilitiesPresenter;
    private PlayerInputPresenter _playerInputPresenter;
    private IObjectPool<Projectile> _projectilePool;

    private FixedJoystick _joystick;
    private AbilityModel _abilityModel;

    private bool _isPerforming = false;

    public void Init(
      AbilityData abilityData,
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      IObjectPool<Projectile> projectilePool,
      PlayerInputPresenter playerInputPresenter
    )
    {
      _abilityData = abilityData;
      _playerBoatAbilitiesPresenter = playerBoatAbilitiesPresenter;
      _projectilePool = projectilePool;
      _playerInputPresenter = playerInputPresenter;
    }

    private void Start()
    {
      _joystick = FindObjectOfType<FixedJoystick>();
      _joystick.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      if (
        _playerBoatAbilitiesPresenter.GetAbilityCooldownRemainingTime(
          _abilityData
        ) > 0
      )
      {
        return;
      }

      _isPerforming = true;
      
      _joystick.gameObject.SetActive(true);
      _joystick.OnPointerDown(eventData);

      _abilityModel = new AbilityModel();
      _abilityData.Perform(
        _playerBoatAbilitiesPresenter,
        _playerInputPresenter,
        _joystick,
        _projectilePool,
        _abilityModel
      );
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      if (!_isPerforming)
      {
        return;
      }

      _joystick.OnPointerUp(eventData);

      _abilityModel.IsPerformed = true;

      _joystick.gameObject.SetActive(false);
      _isPerforming = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (!_isPerforming)
      {
        return;
      }

      _joystick.OnDrag(eventData);
    }
  }
}
