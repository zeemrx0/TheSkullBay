using System;
using LNE.Inputs;
using UnityEngine;

namespace LNE.Combat.Abilities
{
  public abstract class TargetingStrategy : ScriptableObject
  {
    [field: SerializeField]
    public float AimRadius { get; protected set; } = 0f;

    public virtual void Init(AbilityModel abilityModel) { }

    public abstract void StartTargeting(
      WatercraftAbilitiesPresenter watercraftAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      Joystick joystick,
      AbilityModel abilityModel,
      Action onTargetAcquired
    );

    protected void HandleCancelTargeting(
      PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      AbilityModel abilityModel
    )
    {
      UnsubscribeFromInputEvents(
        playerWatercraftAbilitiesPresenter,
        playerInputPresenter,
        abilityModel
      );

      playerWatercraftAbilitiesPresenter.HideAbilityIndicators();

      abilityModel.IsCancelled = true;
    }

    protected void HandleConfirmTargetPosition(
      PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      AbilityModel abilityModel
    )
    {
      UnsubscribeFromInputEvents(
        playerWatercraftAbilitiesPresenter,
        playerInputPresenter,
        abilityModel
      );

      playerWatercraftAbilitiesPresenter.HideAbilityIndicators();

      abilityModel.IsPerformed = true;
    }

    protected void UnsubscribeFromInputEvents(
      PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      AbilityModel abilityModel
    )
    {
      PlayerInputActions playerInputActions =
        playerInputPresenter.GetPlayerInputActions();

      playerInputActions.Watercraft.Choose.performed -= ctx =>
        HandleConfirmTargetPosition(
          playerWatercraftAbilitiesPresenter,
          playerInputPresenter,
          abilityModel
        );
      playerInputActions.Watercraft.Cancel.performed -= ctx =>
        HandleCancelTargeting(
          playerWatercraftAbilitiesPresenter,
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
