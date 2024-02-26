using System;
using LNE.Inputs;
using LNE.Utilities.Constants;
using UnityEngine;
using Zenject;

namespace LNE.Core
{
  public class GameCorePresenter : MonoBehaviour
  {
    public event Action OnGameOver;
    public bool IsGameOver { get; private set; } = false;

    [SerializeField]
    private GameObject _aiBoatsContainer;

    [SerializeField]
    private GameObject _lootsContainer;

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

    private void Start()
    {
      _aiBoatsContainer = GameObject.Find(GameObjectName.AIBoatsContainer);
      _lootsContainer = GameObject.Find(GameObjectName.LootsContainer);
    }

    private void Update()
    {
      CheckIfVictory();
    }

    private void CheckIfVictory()
    {
      if (_aiBoatsContainer)
      {
        if (
          _aiBoatsContainer.transform.childCount == 0
          && _lootsContainer.transform.childCount == 0
        )
        {
          GameOver();
          ShowGameOverPanel(GameString.Victory);
        }
      }
    }

    public void GameOver()
    {
      IsGameOver = true;
      OnGameOver?.Invoke();
      _playerInputActions.Disable();
    }

    public void ShowGameOverPanel(string title)
    {
      _view.ShowGameOverPanel(title);
    }

    public void SetGoldAmount(int amount)
    {
      _view.SetGoldAmount(amount);
    }

    public void RestartLevel()
    {
      int currentSceneIndex = UnityEngine.SceneManagement.SceneManager
        .GetActiveScene()
        .buildIndex;
      UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
    }
  }
}
