using BehaviorDesigner.Runtime.Tasks;
using LNE.Combat.Abilities;

public class AttackTarget : Action
{
  private AIWatercraftAbilitiesPresenter _aiWatercraftAbilitiesPresenter;

  public override void OnAwake()
  {
    _aiWatercraftAbilitiesPresenter =
      GetComponent<AIWatercraftAbilitiesPresenter>();
  }

  public override TaskStatus OnUpdate()
  {
    if (_aiWatercraftAbilitiesPresenter.PerformAbilities())
    {
      return TaskStatus.Success;
    }

    return TaskStatus.Failure;
  }
}
