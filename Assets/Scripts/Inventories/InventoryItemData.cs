using LNE.Utilities.Constants;
using UnityEditor;
using UnityEngine;

namespace LNE.Inventories
{
  [CreateAssetMenu(
    fileName = "NewInventoryItem",
    menuName = "Game Data/Inventory Item",
    order = 0
  )]
  public class InventoryItemData
    : ScriptableObject,
      ISerializationCallbackReceiver
  {
    private string _id;

    [SerializeField]
    private string _name;

    [SerializeField]
    [TextArea]
    private string _description;

    [SerializeField]
    private Sprite _icon;

    [SerializeField]
    private int _maxStack;

    [SerializeField]
    private ItemType _itemType;

    // [SerializeField]
    // private ConsumableData _consumableData;

#if UNITY_EDITOR
    private void OnValidate()
    {
      _maxStack = Mathf.Clamp(_maxStack, 1, 999);
    }
#endif

    public string GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetDescription()
    {
      return _description;
    }

    public Sprite GetIcon()
    {
      return _icon;
    }

    public int GetMaxStack()
    {
      return _maxStack;
    }

    public ItemType GetItemType()
    {
      return _itemType;
    }

    // public ConsumableData GetConsumableData()
    // {
    //   return _consumableData;
    // }

    public void OnBeforeSerialize()
    {
      if (string.IsNullOrEmpty(_id))
      {
        _id = GUID.Generate().ToString();
      }

      // string consumableDataPath = ConsumableResourcePath.GetPath(name);

      // if (!string.IsNullOrEmpty(consumableDataPath))
      // {
      //   _consumableData = Resources.Load<ConsumableData>(consumableDataPath);
      // }
    }

    public void OnAfterDeserialize() { }
  }
}
