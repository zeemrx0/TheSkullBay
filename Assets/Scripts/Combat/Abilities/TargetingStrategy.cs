using System;
using LNE.Inputs;
using UnityEngine;

namespace LNE.Combat.Abilities
{
  public abstract class TargetingStrategy : ScriptableObject
  {
    protected PlayerInputActions _playerInputActions;

    public virtual void Init(AbilityModel abilityModel) { }

    public abstract void StartTargeting(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      AbilityModel abilityModel,
      Action onTargetAcquired
    );

    protected void HandleCancelTargeting(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      AbilityModel abilityModel
    )
    {
      UnsubscribeFromInputEvents(playerBoatAbilitiesPresenter, abilityModel);

      playerBoatAbilitiesPresenter.HideAbilityIndicators();

      abilityModel.IsCancelled = true;
    }

    protected void HandleConfirmTargetPosition(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      AbilityModel abilityModel
    )
    {
      UnsubscribeFromInputEvents(playerBoatAbilitiesPresenter, abilityModel);

      playerBoatAbilitiesPresenter.HideAbilityIndicators();

      abilityModel.IsPerformed = true;
    }

    protected void UnsubscribeFromInputEvents(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      AbilityModel abilityModel
    )
    {
      _playerInputActions.Boat.Choose.performed -= ctx =>
        HandleConfirmTargetPosition(playerBoatAbilitiesPresenter, abilityModel);
      _playerInputActions.Boat.Cancel.performed -= ctx =>
        HandleCancelTargeting(playerBoatAbilitiesPresenter, abilityModel);
    }

    protected virtual string GetAbilityName(string defaultFileName)
    {
      return name.Split(defaultFileName)[0];
    }
  }
}
