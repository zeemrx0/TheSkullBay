using UnityEngine;

namespace LNE.Combat.Trophies
{
  public class PlayerTrophiesPresenter : MonoBehaviour
  {
    [SerializeField]
    private PlayerTrophiesView _view;

    private int _goldAmount = 0;

    public void AddGold(int amount)
    {
      _goldAmount += amount;
      _view.SetGoldAmount(_goldAmount);
    }
  }
}
