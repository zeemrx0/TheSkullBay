using System.Collections.Generic;
using LNE.Combat;
using LNE.Inputs;
using LNE.Utilities.Constants;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace LNE.Abilities
{
  public class PlayerBoatAbilitiesPresenter : MonoBehaviour
  {
    public string Id { get; private set; }

    [field: SerializeField]
    private PlayerBoatAbilitiesView _view;

    [SerializeField]
    private List<AbilityData> _abilityDataList;

    private List<IObjectPool<Projectile>> _projectilePools =
      new List<IObjectPool<Projectile>>();

    // Injected
    private PlayerInputPresenter _playerInputPresenter;
    private PlayerInputActions _playerInputActions;

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

      foreach (var abilityData in _abilityDataList)
      {
        _projectilePools.Add(abilityData.InitProjectilePool());
        _view.SetAbilityButtonIcon(
          _abilityDataList.IndexOf(abilityData),
          abilityData.Icon
        );
        _view.SetAbilityButtonIconActive(
          _abilityDataList.IndexOf(abilityData),
          true
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
      return transform
        .Find($"{abilityName}{GameObjectName.SpawnPoint}")
        .position;
    }

    public Vector3 GetCurrentVelocity()
    {
      return gameObject.GetComponent<Rigidbody>().velocity;
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
      _abilityDataList[0].Perform(
        this,
        _playerInputPresenter.GetPlayerInputActions(),
        _projectilePools[0]
      );
    }
    #endregion
  }
}
