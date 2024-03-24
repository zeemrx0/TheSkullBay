using LNE.Utilities.Constants;

namespace LNE.Inventories
{
  public class PlayerInventoryPresenter : InventoryPresenter
  {
    private void SaveToFile()
    {
      ES3.Save<InventoryModel>(
        SavingKey.PlayerInventoryKey,
        value: _model,
        SavingKey.PlayerInventoryPath
      );
    }

    private void LoadFromFile()
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
      }
    }
  }
}
