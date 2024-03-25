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
    private GameObject _controlCanvas;

    // Injected
    private GameCorePresenter _gameCorePresenter;

    [Inject]
    private void Construct(GameCorePresenter gameCorePresenter)
    {
      _gameCorePresenter = gameCorePresenter;
    }

    protected override IEnumerator DieCoroutine(float delayTime)
    {
      if (gameObject.CompareTag(TagName.Player))
      {
        _controlCanvas.SetActive(false);
      }

      TryGetComponent(out Collider c);
      if (c != null)
      {
        c.enabled = false;
      }

      _vehicle.gameObject.SetActive(false);

      yield return new WaitForSeconds(delayTime);

      Destroy(gameObject);

      _gameCorePresenter.GameOver();
    }
  }
}
