using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Inventories
{
  public class PlayerInventoryPresenter : InventoryPresenter
  {
    [SerializeField]
    private PlayerWatercraftInventoryPresenter _playerWatercraftInventoryPresenter;

    protected override void Awake()
    {
      LoadFromFile();
      _playerWatercraftInventoryPresenter.SetInventoryModel(_model);
    }

    public void MovePlayerWatercraftInventoryToPlayerInventory(
      PlayerWatercraftInventoryPresenter playerWatercraftInventoryPresenter
    )
    {
      for (
        int i = 0;
        i < playerWatercraftInventoryPresenter.InventoryModel.Slots.Length;
        i++
      )
      {
        if (playerWatercraftInventoryPresenter.InventoryModel.Slots[i] != null)
        {
          AddItem(
            playerWatercraftInventoryPresenter.InventoryModel.Slots[i].ItemData,
            playerWatercraftInventoryPresenter.InventoryModel.Slots[i].Quantity
          );
        }
      }
    }

    public void SaveToFile()
    {
      ES3.Save<InventoryModel>(
        SavingKey.PlayerInventoryKey,
        value: _model,
        SavingKey.PlayerInventoryPath
      );
    }

    public void LoadFromFile()
    {
      if (
        ES3.KeyExists(
          SavingKey.PlayerInventoryKey,
          SavingKey.PlayerInventoryPath
        )
      )
      {
        _model = ES3.Load<InventoryModel>(
          SavingKey.PlayerInventoryKey,
          SavingKey.PlayerInventoryPath
        );
        Debug.Log(_model.Currencies.Gold);
      }
    }
  }
}
