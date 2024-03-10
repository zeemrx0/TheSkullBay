using BehaviorDesigner.Runtime.Tasks;
using LNE.Combat.Abilities;
using UnityEngine;

public class AttackTarget : Action
{
  private AIWatercraftAbilitiesPresenter _presenter;

  public override void OnAwake()
  {
    _presenter = GetComponent<AIWatercraftAbilitiesPresenter>();
  }
}
