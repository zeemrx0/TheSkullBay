using LNE.Combat;
using UnityEngine;
using UnityEngine.Pool;

namespace LNE.Abilities
{
  public class AbilityModel
  {
    public bool IsPerformedOrCancelled { get; set; } = false;
    public Vector3 TargetPosition { get; set; }
    public float AimRadius { get; set; } = 0f;
  }
}
