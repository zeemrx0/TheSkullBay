using System.Collections.Generic;
using LNE.Core;
using LNE.Inputs;
using LNE.Utilities.Constants;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace LNE.Combat.Abilities
{
  public class PlayerBoatAbilitiesPresenter : MonoBehaviour
  {
    public string Id { get; private set; }

    [SerializeField]
    private PlayerBoatAbilitiesView _view;

    [SerializeField]
    private Transform _abilitySpawnPointsContainer;

    [SerializeField]
    private List<AbilityData> _abilityDataList;

    [SerializeField]
    private AbilityButton[] _abilityButtons;

    // Injected
    private PlayerInputPresenter _playerInputPresenter;
    private PlayerInputActions _playerInputActions;

    private Character _character;

    private List<IObjectPool<Projectile>> _projectilePools =
      new List<IObjectPool<Projectile>>();

    private PlayerBoatAbilitiesModel _model;

    public RectTransform Origin => _view.Origin;
    public Vector3 Direction
    {
      get => _view.Direction;
      set { _view.Direction = value; }
    }

    [Inject]
    public void Init(PlayerInputPresenter playerInputPresenter)
    {
      _playerInputPresenter = playerInputPresenter;
      _playerInputPresenter.Init();

      _playerInputActions = _playerInputPresenter.GetPlayerInputActions();
    }

    private void Awake()
    {
      Id = gameObject.GetInstanceID().ToString();

      _character = GetComponent<Character>();
    }

    private void OnEnable()
    {
      _playerInputActions.Boat.Ability1.performed += HandleAbility1;
    }

    private void OnDisable()
    {
      _playerInputActions.Boat.Ability1.performed -= HandleAbility1;
    }

    private void Start()
    {
      _model = new PlayerBoatAbilitiesModel();

      for (int i = 0; i < _abilityDataList.Count; ++i)
      {
        AbilityData abilityData = _abilityDataList[i];

        _projectilePools.Add(abilityData.InitProjectilePool());
        _view.SetAbilityButtonIcon(
          _abilityDataList.IndexOf(abilityData),
          abilityData.Icon
        );
        _view.SetAbilityButtonIconActive(
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

    private void Update()
    {
      _model.CoolDownAbilities();

      foreach (var abilityData in _abilityDataList)
      {
        _view.SetAbilityCooldownTime(
          _abilityDataList.IndexOf(abilityData),
          _model.GetAbilityCooldownRemainingTime(abilityData),
          _model.GetAbilityCooldownInitialTime(abilityData)
        );
      }
    }

    public Vector3 FindAbilitySpawnPosition(string abilityName)
    {
      return _character.AbilitySpawnPosition;
    }

    public Vector3 GetCurrentVelocity()
    {
      return gameObject.GetComponent<Rigidbody>().velocity;
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

    public float PlayAudioClip(AudioClip audioClip)
    {
      return _view.PlayAudioClip(audioClip);
    }

    #region View Methods
    public void ShowRangeIndicator()
    {
      _view.ShowRangeIndicator();
    }

    public void ShowCircleIndicator()
    {
      _view.ShowCircleIndicator();
    }

    public void HideRangeIndicator()
    {
      _view.HideRangeIndicator();
    }

    public void HideCircleIndicator()
    {
      _view.HideCircleIndicator();
    }

    public void SetRangeIndicatorSize(Vector2 size)
    {
      _view.SetRangeIndicatorSize(size);
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

    public void HideAbilityIndicators()
    {
      _view.HideRangeIndicator();
      _view.HideCircleIndicator();
      _view.HidePhysicalProjectileTrajectory();
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
