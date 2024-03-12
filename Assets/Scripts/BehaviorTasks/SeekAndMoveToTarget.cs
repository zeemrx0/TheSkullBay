using BehaviorDesigner.Runtime.Tasks;
using LNE.Combat.Abilities;
using LNE.Core;
using LNE.Movements;
using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.BehaviorTasks
{
  public class SeekAndMoveToTarget : Action
  {
    private AIWatercraftMovementPresenter _movementPresenter;
    private AIWatercraftAbilitiesPresenter _abilitiesPresenter;

    public override void OnAwake()
    {
      _movementPresenter = GetComponent<AIWatercraftMovementPresenter>();
      _abilitiesPresenter = GetComponent<AIWatercraftAbilitiesPresenter>();
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
        _movementPresenter.FieldOfViewRadius,
        Vector3.up,
        0
      );

      foreach (RaycastHit hit in hits)
      {
        hit.transform.TryGetComponent<Character>(out Character character);

        if (
          character != null
          && character.gameObject != gameObject
          && character.CompareTag(TagName.Player)
        )
        {
          _abilitiesPresenter.Target = character;
          _movementPresenter.SetTargetPosition(character.transform.position);
          return true;
        }
      }

      return false;
    }
  }
}
