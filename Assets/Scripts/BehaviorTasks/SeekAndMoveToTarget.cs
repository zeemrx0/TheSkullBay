using BehaviorDesigner.Runtime.Tasks;
using LNE.Core;
using LNE.Movements;
using UnityEngine;

namespace LNE.BehaviorTasks
{
  public class SeekAndMoveToTarget : Action
  {
    private AIWatercraftMovementPresenter _presenter;

    public override void OnAwake()
    {
      _presenter = GetComponent<AIWatercraftMovementPresenter>();
    }

    public override TaskStatus OnUpdate()
    {
      if (SeekForTarget())
      {
        return TaskStatus.Success;
      }

      return TaskStatus.Failure;
    }

    public bool SeekForTarget()
    {
      RaycastHit[] hits = Physics.SphereCastAll(
        transform.position,
        _presenter.FieldOfViewRadius,
        Vector3.up,
        0
      );

      foreach (RaycastHit hit in hits)
      {
        hit.transform.TryGetComponent<Character>(out Character character);

        if (character != null && character.gameObject != gameObject)
        {
          _presenter.SetTargetPosition(character.transform.position);
          return true;
        }
      }

      return false;
    }
  }
}
