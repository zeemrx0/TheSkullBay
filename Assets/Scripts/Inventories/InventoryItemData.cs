using UnityEditor;
using UnityEngine;

namespace LNE.Inventories
{
  [CreateAssetMenu(
    fileName = "_InventoryItemData",
    menuName = "Inventories/Item",
    order = 0
  )]
  public class InventoryItemData
    : ScriptableObject,
      ISerializationCallbackReceiver
  {
    public string Id { get; private set; }

    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public string Description { get; private set; }

    [field: SerializeField]
    public Sprite Icon { get; private set; }

    [field: SerializeField]
    public int MaxStack { get; private set; }

#if UNITY_EDITOR
    private void OnValidate()
    {
      MaxStack = Mathf.Clamp(MaxStack, 1, 99999);
    }
#endif

    public void OnBeforeSerialize()
    {
      Id ??= GUID.Generate().ToString();
    }

    public void OnAfterDeserialize() { }
  }
}
