using LNE.Inputs;
using UnityEngine;
using Zenject;

namespace LNE.Combat.Abilities
{
  public class PlayerBoatAbilitiesPresenter : BoatAbilitiesPresenter
  {
    [SerializeField]
    private AbilityButton[] _abilityButtons;

    // Injected
    private PlayerInputPresenter _playerInputPresenter;
    private PlayerInputActions _playerInputActions;

    [Inject]
    public void Init(PlayerInputPresenter playerInputPresenter)
    {
      _playerInputPresenter = playerInputPresenter;
      _playerInputPresenter.Init();

      _playerInputActions = _playerInputPresenter.GetPlayerInputActions();
    }

    protected override void Awake()
    {
      base.Awake();
      _view = GetComponent<PlayerBoatAbilitiesView>();
    }

    private void OnEnable()
    {
      _playerInputActions.Boat.Ability1.performed += HandleAbility1;
    }

    private void OnDisable()
    {
      _playerInputActions.Boat.Ability1.performed -= HandleAbility1;
    }

    protected override void Start()
    {
      base.Start();

      for (int i = 0; i < _abilityDataList.Count; ++i)
      {
        AbilityData abilityData = _abilityDataList[i];

        ((PlayerBoatAbilitiesView)_view).SetAbilityButtonIcon(
          _abilityDataList.IndexOf(abilityData),
          abilityData.Icon
        );
        ((PlayerBoatAbilitiesView)_view).SetAbilityButtonIconActive(
          _abilityDataList.IndexOf(abilityData),
          true
        );

        _abilityButtons[i].Init(
          abilityData,
          this,
          _projectilePools[i],
          _playerInputPresenter
        );
      }
    }

    protected override void Update()
    {
      base.Update();
      foreach (var abilityData in _abilityDataList)
      {
        ((PlayerBoatAbilitiesView)_view).SetAbilityCooldownTime(
          _abilityDataList.IndexOf(abilityData),
          _model.GetAbilityCooldownRemainingTime(abilityData),
          _model.GetAbilityCooldownInitialTime(abilityData)
        );
      }
    }

    public Vector3 GetJoystickDirection(Joystick joystick)
    {
      Transform cameraTransform = Camera.main.transform;
      float cameraAngle = cameraTransform.eulerAngles.y;

      Vector3 direction =
        Quaternion.Euler(0, cameraAngle, 0)
        * new Vector3(joystick.Direction.x, 0, joystick.Direction.y);

      return direction;
    }

    #region View Methods
    public void HideAbilityIndicators()
    {
      _view.HideRangeIndicator();
      _view.HideCircleIndicator();
      _view.HidePhysicalProjectileTrajectory();
    }

    public void ShowRangeIndicator()
    {
      _view.ShowRangeIndicator();
    }

    public void HideRangeIndicator()
    {
      _view.HideRangeIndicator();
    }

    public void SetRangeIndicatorSize(Vector2 size)
    {
      _view.SetRangeIndicatorSize(size);
    }

    public void ShowCircleIndicator()
    {
      _view.ShowCircleIndicator();
    }

    public void HideCircleIndicator()
    {
      _view.HideCircleIndicator();
    }

    public void SetCircleIndicatorSize(Vector2 size)
    {
      _view.SetCircleIndicatorSize(size);
    }

    public void SetCircleIndicatorPosition(Vector3 position)
    {
      _view.SetCircleIndicatorPosition(position);
    }

    public void ShowPhysicalProjectileTrajectory()
    {
      _view.ShowPhysicalProjectileTrajectory();
    }

    public void HidePhysicalProjectileTrajectory()
    {
      _view.HidePhysicalProjectileTrajectory();
    }

    public void SetPhysicalProjectileTrajectory(
      Vector3 initialPosition,
      Vector3 velocity
    )
    {
      _view.SetPhysicalProjectileTrajectory(initialPosition, velocity);
    }
    #endregion

    #region Model Methods
    public float GetAbilityCooldownRemainingTime(AbilityData abilityData)
    {
      return _model.GetAbilityCooldownRemainingTime(abilityData);
    }

    public void StartCooldown(AbilityData abilityData, float cooldownTime)
    {
      _model.StartCooldown(abilityData, cooldownTime);
    }
    #endregion

    #region Input Handlers
    private void HandleAbility1(
      UnityEngine.InputSystem.InputAction.CallbackContext context
    )
    {
      AbilityModel abilityModel = new AbilityModel();

      _abilityDataList[0].Perform(
        this,
        _playerInputPresenter,
        null,
        _projectilePools[0],
        abilityModel
      );
    }
    #endregion
  }
}
