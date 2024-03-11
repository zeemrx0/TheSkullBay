using BehaviorDesigner.Runtime.Tasks;
using LNE.Combat.Abilities;

public class AttackTarget : Action
{
  private AIWatercraftAbilitiesPresenter _presenter;

  public override void OnAwake()
  {
    _presenter = GetComponent<AIWatercraftAbilitiesPresenter>();
  }

  public override TaskStatus OnUpdate()
  {
    _presenter.PerformAbilities();
    return TaskStatus.Success;
  }
}
