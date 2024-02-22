using System.Collections.Generic;
using LNE.Combat;
using LNE.Inputs;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace LNE.Abilities
{
  public class PlayerBoatAbilitiesPresenter : MonoBehaviour
  {
    public string Id { get; private set; }

    [field: SerializeField]
    public PlayerBoatAbilitiesView View { get; private set; }

    [SerializeField]
    private List<AbilityData> _abilityDataList;

    private List<IObjectPool<Projectile>> _projectilePools =
      new List<IObjectPool<Projectile>>();

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
      foreach (var abilityData in _abilityDataList)
      {
        _projectilePools.Add(abilityData.InitProjectilePool());
      }
    }

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
  }
}
