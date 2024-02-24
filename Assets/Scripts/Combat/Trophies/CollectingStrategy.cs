using UnityEngine;

namespace LNE.Combat.Trophies
{
  public abstract class CollectingStrategy : ScriptableObject
  {
    public abstract void StartCollecting(
      PlayerTrophiesPresenter playerTrophiesPresenter
    );
  }
}
