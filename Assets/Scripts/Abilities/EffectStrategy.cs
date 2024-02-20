using System;
using LNE.Inputs;
using UnityEngine;

namespace LNE.Abilities
{
  public abstract class EffectStrategy : ScriptableObject
  {
    public abstract void StartEffect(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      AbilityModel abilityModel
    );
  }
}
