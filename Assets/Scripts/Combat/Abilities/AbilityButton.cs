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
    }

    public void OnPointerDown(PointerEventData eventData)
    {
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
      _joystick.OnPointerUp(eventData);

      _abilityModel.IsPerformed = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
      _joystick.OnDrag(eventData);
    }
  }
}
