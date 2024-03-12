using System.Collections;
using LNE.Core;
using LNE.Utilities.Constants;
using UnityEngine;
using Zenject;

namespace LNE.Combat
{
  public class PlayerHealthPresenter : HealthPresenter
  {
    [SerializeField]
    private GameObject controlCanvas;

    // Injected
    private GameCorePresenter _gameCorePresenter;

    [Inject]
    public void Init(GameCorePresenter gameCorePresenter)
    {
      _gameCorePresenter = gameCorePresenter;
    }

    protected override IEnumerator DieCoroutine(float delayTime)
    {
      if (gameObject.CompareTag(TagName.Player))
      {
        controlCanvas.SetActive(false);
      }

      base.DieCoroutine(delayTime);

      _gameCorePresenter.GameOver();

      yield return null;
    }
  }
}
