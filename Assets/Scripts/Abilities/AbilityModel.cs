using UnityEngine;

namespace LNE.Abilities
{
  public class AbilityModel
  {
    public bool IsCancelled { get; private set; } = false;
    public Vector3 TargetPosition { get; set; }
  }
}
