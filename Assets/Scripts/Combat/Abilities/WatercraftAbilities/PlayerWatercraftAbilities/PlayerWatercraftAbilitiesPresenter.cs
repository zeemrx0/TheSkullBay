using LNE.Inputs;
using UnityEngine;
using Zenject;

namespace LNE.Combat.Abilities
{
  public class PlayerWatercraftAbilitiesPresenter : WatercraftAbilitiesPresenter
  {
    [SerializeField]
    private AbilityButton[] _abilityButtons;

    // Injected
    private PlayerInputPresenter _playerInputPresenter;
    private PlayerInputActions _playerInputActions;

    private Camera _mainCamera;

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
      _view = GetComponent<PlayerWatercraftAbilitiesView>();
      _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
      _playerInputActions.Watercraft.Ability1.performed += HandleAbility1;
    }

    private void OnDisable()
    {
      _playerInputActions.Watercraft.Ability1.performed -= HandleAbility1;
    }

    protected override void Start()
    {
      base.Start();

      for (int i = 0; i < _abilityDataList.Count; ++i)
      {
        AbilityData abilityData = _abilityDataList[i];

        ((PlayerWatercraftAbilitiesView)_view).SetAbilityButtonIcon(
          _abilityDataList.IndexOf(abilityData),
          abilityData.Icon
        );
        ((PlayerWatercraftAbilitiesView)_view).SetAbilityButtonIconActive(
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
        ((PlayerWatercraftAbilitiesView)_view).SetAbilityCooldownTime(
          _abilityDataList.IndexOf(abilityData),
          _model.GetAbilityCooldownRemainingTime(abilityData),
          _model.GetAbilityCooldownInitialTime(abilityData)
        );
      }
    }

    public Vector3 GetJoystickDirection(Joystick joystick)
    {
      Transform cameraTransform = _mainCamera.transform;
      float cameraAngle = cameraTransform.eulerAngles.y;

      Vector3 direction =
        Quaternion.Euler(0, cameraAngle, 0)
        * new Vector3(joystick.Direction.x, 0, joystick.Direction.y);

      return direction;
    }

    #region View Methods

    public void HideAbilityIndicators()
    {
      ((PlayerWatercraftAbilitiesView)_view).HideRangeIndicator();
      ((PlayerWatercraftAbilitiesView)_view).HideCircleIndicator();
      ((PlayerWatercraftAbilitiesView)_view).HidePhysicalProjectileTrajectory();
      ((PlayerWatercraftAbilitiesView)_view).HideAimRay();
    }

    #region Range Indicator
    public void ShowRangeIndicator()
    {
      ((PlayerWatercraftAbilitiesView)_view).ShowRangeIndicator();
    }

    public void HideRangeIndicator()
    {
      ((PlayerWatercraftAbilitiesView)_view).HideRangeIndicator();
    }

    public void SetRangeIndicatorSize(Vector2 size)
    {
      ((PlayerWatercraftAbilitiesView)_view).SetRangeIndicatorSize(size);
    }
    #endregion

    #region Aim Indicator
    public void ShowCircleIndicator()
    {
      ((PlayerWatercraftAbilitiesView)_view).ShowCircleIndicator();
    }

    public void HideCircleIndicator()
    {
      ((PlayerWatercraftAbilitiesView)_view).HideCircleIndicator();
    }

    public void SetCircleIndicatorSize(Vector2 size)
    {
      ((PlayerWatercraftAbilitiesView)_view).SetCircleIndicatorSize(size);
    }

    public void SetCircleIndicatorPosition(Vector3 position)
    {
      ((PlayerWatercraftAbilitiesView)_view).SetCircleIndicatorPosition(
        position
      );
    }
    #endregion

    #region Physical Projectile Trajectory
    public void ShowPhysicalProjectileTrajectory()
    {
      ((PlayerWatercraftAbilitiesView)_view).ShowPhysicalProjectileTrajectory();
    }

    public void HidePhysicalProjectileTrajectory()
    {
      ((PlayerWatercraftAbilitiesView)_view).HidePhysicalProjectileTrajectory();
    }

    public void SetPhysicalProjectileTrajectory(
      Vector3 initialPosition,
      Vector3 velocity
    )
    {
      ((PlayerWatercraftAbilitiesView)_view).SetPhysicalProjectileTrajectory(
        initialPosition,
        velocity
      );
    }
    #endregion

    #region Aim Ray
    public void ShowAimRay()
    {
      ((PlayerWatercraftAbilitiesView)_view).ShowAimRay();
    }

    public void HideAimRay()
    {
      ((PlayerWatercraftAbilitiesView)_view).HideAimRay();
    }

    public void SetAimRay(Vector3 start, Vector3 end)
    {
      ((PlayerWatercraftAbilitiesView)_view).SetAimRay(start, end);
    }
    #endregion

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
