using UnityEngine;

namespace LNE.Inventories
{
  [CreateAssetMenu(
    fileName = "_InventoryData",
    menuName = "Inventories/AI Inventory",
    order = 0
  )]
  public class AIInventoryData : ScriptableObject
  {
    [field: SerializeField]
    public InventoryModel InventoryModel;
  }
}
