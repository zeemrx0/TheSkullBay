using UnityEngine;

namespace LNE.Movements
{
  public class PlayerBoatMovementView : MonoBehaviour
  {
    public void Move(
      Rigidbody rigidbody,
      Vector2 moveDirection,
      float moveSpeed,
      float steerSpeed,
      Transform steerPosition
    )
    {
      rigidbody.AddRelativeForce(
        moveDirection.y * Vector3.forward * moveSpeed * Time.deltaTime,
        ForceMode.Force
      );

      rigidbody.AddForceAtPosition(
        moveDirection.x * Vector3.left * steerSpeed * Time.deltaTime,
        steerPosition.position
      );
    }
  }
}
