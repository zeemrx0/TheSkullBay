using UnityEngine;

namespace LNE.Combat.Abilities
{
  public class AbilityModel
  {
    public bool IsPerformed { get; set; } = false;
    public bool IsCancelled { get; set; } = false;
    public Vector3 InitialPosition { get; set; }
    public Vector3 TargetPosition { get; set; }
    public float AimRadius { get; set; } = 0f;
    public float ProjectSpeed { get; set; }

    public Vector3 GetPhysicalProjectVelocity()
    {
      float distance = Vector3.Distance(InitialPosition, TargetPosition);

      float angle =
        Mathf.Asin(
          distance * Physics.gravity.magnitude / Mathf.Pow(ProjectSpeed, 2)
        )
        * Mathf.Rad2Deg
        / 2;

      float speedX = ProjectSpeed * Mathf.Cos(angle * Mathf.Deg2Rad);
      float speedY = ProjectSpeed * Mathf.Sin(angle * Mathf.Deg2Rad);

      Vector3 velocity =
        (TargetPosition - InitialPosition).normalized * speedX
        + Vector3.up * speedY;

      return velocity;
    }

    public Vector3 GetStraightProjectVelocity()
    {
      Vector3 velocity =
        (TargetPosition - InitialPosition).normalized * ProjectSpeed;

      return velocity;
    }
  }
}
