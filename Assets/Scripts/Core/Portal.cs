using LNE.Core;
using LNE.Inventories;
using LNE.Utilities.Constants;
using UnityEngine;
using Zenject;

public class Portal : MonoBehaviour
{
  private GameCorePresenter _gameCorePresenter;
  private PlayerInventoryPresenter _playerInventoryPresenter;

  [Inject]
  public void Construct(
    GameCorePresenter gameCorePresenter,
    PlayerInventoryPresenter playerInventoryPresenter
  )
  {
    _gameCorePresenter = gameCorePresenter;
    _playerInventoryPresenter = playerInventoryPresenter;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag(TagName.Player))
    {
      PlayerWatercraftInventoryPresenter playerWatercraftInventoryPresenter =
        other.GetComponent<PlayerWatercraftInventoryPresenter>();

      _playerInventoryPresenter.MovePlayerWatercraftInventoryToPlayerInventory(
        playerWatercraftInventoryPresenter
      );

      _playerInventoryPresenter.SaveToFile();
      _gameCorePresenter.GameOver();
    }
  }
}
