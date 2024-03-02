using System;
using LNE.Inputs;
using UnityEngine;

namespace LNE.Combat.Abilities
{
  public abstract class TargetingStrategy : ScriptableObject
  {
    public virtual void Init(AbilityModel abilityModel) { }

    public abstract void StartTargeting(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      Joystick joystick,
      AbilityModel abilityModel,
      Action onTargetAcquired
    );

    protected void HandleCancelTargeting(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      AbilityModel abilityModel
    )
    {
      UnsubscribeFromInputEvents(
        playerBoatAbilitiesPresenter,
        playerInputPresenter,
        abilityModel
      );

      playerBoatAbilitiesPresenter.HideAbilityIndicators();

      abilityModel.IsCancelled = true;
    }

    protected void HandleConfirmTargetPosition(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      AbilityModel abilityModel
    )
    {
      UnsubscribeFromInputEvents(
        playerBoatAbilitiesPresenter,
        playerInputPresenter,
        abilityModel
      );

      playerBoatAbilitiesPresenter.HideAbilityIndicators();

      abilityModel.IsPerformed = true;
    }

    protected void UnsubscribeFromInputEvents(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      AbilityModel abilityModel
    )
    {
      PlayerInputActions playerInputActions =
        playerInputPresenter.GetPlayerInputActions();

      playerInputActions.Boat.Choose.performed -= ctx =>
        HandleConfirmTargetPosition(
          playerBoatAbilitiesPresenter,
          playerInputPresenter,
          abilityModel
        );
      playerInputActions.Boat.Cancel.performed -= ctx =>
        HandleCancelTargeting(
          playerBoatAbilitiesPresenter,
          playerInputPresenter,
          abilityModel
        );
    }

    protected virtual string GetAbilityName(string defaultFileName)
    {
      return name.Split(defaultFileName)[0];
    }
  }
}
