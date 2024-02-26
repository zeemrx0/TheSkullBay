using UnityEngine;

namespace LNE.Combat.Loots
{
  [CreateAssetMenu(
    fileName = "_GoldCollectingData",
    menuName = "Loots/Collecting/Gold Collecting"
  )]
  public class GoldCollectingData : CollectingStrategy
  {
    [SerializeField]
    private int _goldAmount;

    public override void StartCollecting(
      PlayerLootsPresenter playerLootsPresenter
    )
    {
      playerLootsPresenter.AddGold(_goldAmount);
    }
  }
}
