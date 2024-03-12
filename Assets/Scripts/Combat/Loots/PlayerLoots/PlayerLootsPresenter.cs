using LNE.Core;
using UnityEngine;
using Zenject;

namespace LNE.Combat.Loots
{
  public class PlayerLootsPresenter : MonoBehaviour
  {
    [SerializeField]
    private PlayerLootsView _view;

    // Injected
    private GameCorePresenter _gameCorePresenter;

    [Inject]
    public void Init(GameCorePresenter gameCorePresenter)
    {
      _gameCorePresenter = gameCorePresenter;
      _gameCorePresenter.OnGameOver += HandleGameOver;
    }

    private void Start()
    {
      ResetGold();
    }

    private void HandleGameOver()
    {
      _gameCorePresenter.SetGoldAmount(_goldAmount);
    }

    private void OnEnable()
    {
      if (_gameCorePresenter != null)
      {
        _gameCorePresenter.OnGameOver += HandleGameOver;
      }
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

    public void ResetGold()
    {
      _goldAmount = 0;
      _view.SetGoldAmount(_goldAmount);
    }
  }
}
