using LNE.Core;
using LNE.Utilities.Constants;
using UnityEngine;
using Zenject;

public class Portal : MonoBehaviour
{
  private GameCorePresenter _gameCorePresenter;

  [Inject]
  public void Construct(GameCorePresenter gameCorePresenter)
  {
    _gameCorePresenter = gameCorePresenter;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag(TagName.Player))
    {
      _gameCorePresenter.GameOver();
    }
  }
}
