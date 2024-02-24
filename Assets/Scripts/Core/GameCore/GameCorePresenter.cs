using System;
using LNE.Inputs;
using UnityEngine;
using Zenject;

namespace LNE.Core
{
  public class GameCorePresenter : MonoBehaviour
  {
    public event Action OnGameOver;
    public bool IsGameOver { get; private set; } = false;

    [SerializeField]
    private GameCoreView _view;

    // Injected
    private PlayerInputPresenter _playerInputPresenter;
    private PlayerInputActions _playerInputActions;

    [Inject]
    private void Init(PlayerInputPresenter playerInputPresenter)
    {
      _playerInputPresenter = playerInputPresenter;
      _playerInputPresenter.Init();
      _playerInputActions = _playerInputPresenter.GetPlayerInputActions();
    }

    public void GameOver()
    {
      IsGameOver = true;
      OnGameOver?.Invoke();
      _playerInputActions.Disable();
      _view.ShowGameOverPanel();
    }
  }
}
