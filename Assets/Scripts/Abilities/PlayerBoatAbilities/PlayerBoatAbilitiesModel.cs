using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LNE.Abilities
{
  public class PlayerBoatAbilitiesModel
  {
    public Dictionary<AbilityData, float> CooldownTimers =
      new Dictionary<AbilityData, float>();
    public Dictionary<AbilityData, float> InitialCooldownTimers =
      new Dictionary<AbilityData, float>();

    public void CoolDownAbilities()
    {
      foreach (var abilityData in CooldownTimers.Keys.ToList())
      {
        if (CooldownTimers[abilityData] > 0)
        {
          CooldownTimers[abilityData] -= Time.deltaTime;

          if (CooldownTimers[abilityData] < 0)
          {
            CooldownTimers.Remove(abilityData);
            InitialCooldownTimers.Remove(abilityData);
          }
        }
      }
    }

    public void StartCooldown(AbilityData abilityData, float cooldownTime)
    {
      if (CooldownTimers.ContainsKey(abilityData))
      {
        return;
      }

      CooldownTimers.Add(abilityData, cooldownTime);
      InitialCooldownTimers.Add(abilityData, cooldownTime);
    }

    public float GetAbilityCooldownRemainingTime(AbilityData abilityData)
    {
      if (CooldownTimers.ContainsKey(abilityData))
      {
        return CooldownTimers[abilityData];
      }

      return 0;
    }

    public float GetAbilityCooldownInitialTime(AbilityData abilityData)
    {
      if (InitialCooldownTimers.ContainsKey(abilityData))
      {
        return InitialCooldownTimers[abilityData];
      }

      return 0;
    }
  }
}
