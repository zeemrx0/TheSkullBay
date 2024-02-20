using System;
using LNE.Inputs;
using UnityEngine;

namespace LNE.Abilities
{
  public abstract class TargetingStrategy : ScriptableObject
  {
    public abstract void StartTargeting(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      AbilityModel abilityModel,
      Action onTargetAcquired
    );
  }
}
