using LNE.Core;
using UnityEngine;

namespace LNE.Combat.Abilities
{
  public class AIWatercraftAbilitiesPresenter : WatercraftAbilitiesPresenter
  {
    private Character _target;

    public Vector3 GetTargetPosition()
    {
      return _target.transform.position;
    }

    public float GetDistanceToTarget()
    {
      return Vector3.Distance(transform.position, _target.transform.position);
    }
  }
}
