using LNE.Inputs;
using LNE.Utilities.Constants;
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
    private PlayerWatercraftAbilitiesPresenter _playerWatercraftAbilitiesPresenter;
    private PlayerInputPresenter _playerInputPresenter;
    private IObjectPool<Projectile> _projectilePool;

    private FixedJoystick _joystick;
    private AbilityModel _abilityModel;

    private bool _isPerforming = false;

    public void Init(
      AbilityData abilityData,
      PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
      IObjectPool<Projectile> projectilePool,
      PlayerInputPresenter playerInputPresenter
    )
    {
      _abilityData = abilityData;
      _playerWatercraftAbilitiesPresenter = playerWatercraftAbilitiesPresenter;
      _projectilePool = projectilePool;
      _playerInputPresenter = playerInputPresenter;
    }

    private void Awake()
    {
      _joystick = transform
        .Find(GameObjectName.TargetingJoystick)
        .GetComponent<FixedJoystick>();
    }

    private void Start()
    {
      _joystick.gameObject.SetActive(false);
      _abilityModel = new AbilityModel();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      if (
        _playerWatercraftAbilitiesPresenter.GetAbilityCooldownRemainingTime(
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
        _playerWatercraftAbilitiesPresenter,
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
