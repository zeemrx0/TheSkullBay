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

    private void Start() { }

    private void HandleGameOver() { }

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
  }
}
