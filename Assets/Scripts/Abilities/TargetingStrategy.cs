using System;
using LNE.Inputs;
using UnityEngine;

namespace LNE.Abilities
{
  public abstract class TargetingStrategy : ScriptableObject
  {
    public virtual void Init(AbilityModel abilityModel) { }

    public abstract void StartTargeting(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      AbilityModel abilityModel,
      Action onTargetAcquired
    );
  }
}
