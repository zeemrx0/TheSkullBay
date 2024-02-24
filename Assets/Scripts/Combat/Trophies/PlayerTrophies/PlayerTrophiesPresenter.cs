using LNE.Core;
using UnityEngine;
using Zenject;

namespace LNE.Combat.Trophies
{
  public class PlayerTrophiesPresenter : MonoBehaviour
  {
    [SerializeField]
    private PlayerTrophiesView _view;

    private GameCorePresenter _gameCorePresenter;

    [Inject]
    public void Init(GameCorePresenter gameCorePresenter)
    {
      _gameCorePresenter = gameCorePresenter;
      _gameCorePresenter.OnGameOver += HandleGameOver;
    }

    private void HandleGameOver()
    {
      _gameCorePresenter.SetGoldAmount(_goldAmount);
    }

    private void OnDisable()
    {
      _gameCorePresenter.OnGameOver -= HandleGameOver;
    }

    private int _goldAmount = 0;

    public void AddGold(int amount)
    {
      _goldAmount += amount;
      _view.SetGoldAmount(_goldAmount);
    }
  }
}
