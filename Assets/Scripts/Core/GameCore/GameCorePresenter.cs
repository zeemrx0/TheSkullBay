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
    private AdMobPresenter _adMobPresenter;

    private PlayerInputActions _playerInputActions;
    private bool _hasShownAd = false;

    [Inject]
    private void Init(
      PlayerInputPresenter playerInputPresenter,
      AdMobPresenter adMobPresenter
    )
    {
      _playerInputPresenter = playerInputPresenter;
      _playerInputPresenter.Init();
      _playerInputActions = _playerInputPresenter.GetPlayerInputActions();
      _adMobPresenter = adMobPresenter;
    }

    private void Start()
    {
      _aiBoatsContainer = GameObject.Find(GameObjectName.AIWatercraftCharactersContainer);
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
      _view.ShowGameOverPanel();
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

    public void ShowTutorialPanel()
    {
      _view.ShowTutorialPanel();
    }

    public void HideTutorialPanel()
    {
      _view.HideTutorialPanel();
    }

    public void ToggleTutorialPanel()
    {
      _view.ToggleTutorialPanel();
    }

    public void HandleRetryButton()
    {
      if (_hasShownAd)
      {
        RestartLevel();
      }
      else
      {
        _adMobPresenter.ShowInterstitialAd();
        _hasShownAd = true;
      }
    }
  }
}
