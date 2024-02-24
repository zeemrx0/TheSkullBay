using UnityEngine;

namespace LNE.Combat.Trophies
{
  [CreateAssetMenu(
    fileName = "_GoldCollectingData",
    menuName = "Trophies/Collecting/Gold Collecting"
  )]
  public class GoldCollectingData : CollectingStrategy
  {
    [SerializeField]
    private int _goldAmount;

    public override void StartCollecting(
      PlayerTrophiesPresenter playerTrophiesPresenter
    )
    {
      playerTrophiesPresenter.AddGold(_goldAmount);
    }
  }
}
