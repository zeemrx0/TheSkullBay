using UnityEngine;

namespace LNE.Movements.Human
{
  public class HumanMovementModel
  {
    public Vector3 Velocity = Vector3.zero;
    public Vector2 MoveVelocity
    {
      get => new Vector2(Velocity.x, Velocity.z);
      set => Velocity = new Vector3(value.x, Velocity.y, value.y);
    }
  }
}
