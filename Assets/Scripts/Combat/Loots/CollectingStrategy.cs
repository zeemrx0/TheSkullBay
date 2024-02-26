using UnityEngine;

namespace LNE.Combat.Loots
{
  public abstract class CollectingStrategy : ScriptableObject
  {
    public abstract void StartCollecting(
      PlayerLootsPresenter playerLootsPresenter
    );
  }
}
