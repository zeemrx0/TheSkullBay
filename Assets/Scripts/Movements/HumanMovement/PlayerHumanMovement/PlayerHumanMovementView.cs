using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Movements.Human
{
  public class PlayerHumanMovementView : MonoBehaviour
  {
    [SerializeField]
    private Animator _animator;

    public void Move(HumanMovementModel model)
    {
      _animator.SetFloat(AnimationName.MoveSpeed, model.MoveVelocity.magnitude);
    }
  }
}
