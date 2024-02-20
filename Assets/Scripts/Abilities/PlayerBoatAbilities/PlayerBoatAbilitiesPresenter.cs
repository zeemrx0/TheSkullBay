using System.Collections.Generic;
using LNE.Inputs;
using UnityEngine;
using Zenject;

namespace LNE.Abilities
{
  public class PlayerBoatAbilitiesPresenter : MonoBehaviour
  {
    [field: SerializeField]
    public PlayerBoatAbilitiesView View { get; private set; }

    [SerializeField]
    private List<AbilityData> _abilityDataList;

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

    private void OnEnable()
    {
      _playerInputActions.Boat.Ability1.performed += HandleAbility1;
    }

    private void OnDisable()
    {
      _playerInputActions.Boat.Ability1.performed -= HandleAbility1;
    }

    private void Update() { }

    private void HandleAbility1(
      UnityEngine.InputSystem.InputAction.CallbackContext context
    )
    {
      _abilityDataList[0].Perform(
        this,
        _playerInputPresenter.GetPlayerInputActions()
      );
    }
  }
}
