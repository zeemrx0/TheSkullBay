using UnityEngine;

namespace LNE.Movements.Human
{
  public class PlayerHumanMovementView : MonoBehaviour
  {
    private CharacterController _characterController;

    private void Awake()
    {
      _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector3 velocity)
    {
      _characterController.Move(velocity * Time.fixedDeltaTime);
    }
  }
}
