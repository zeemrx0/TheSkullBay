using LNE.Core;
using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Inventories.Loots
{
  public class SpawnLootOnDeath : MonoBehaviour
  {
    [SerializeField]
    private CollectableLoot _lootPrefab;

    private AIWatercraftInventoryPresenter _aiInventoryPresenter;

    private void Awake()
    {
      _aiInventoryPresenter = GetComponent<AIWatercraftInventoryPresenter>();
    }

    public void SpawnLoot()
    {
      CollectableLootModel collectableLootModel =
        _aiInventoryPresenter.GetCollectableLootModel();

      Vector3? origin = TryGetComponent<WatercraftCharacter>(out WatercraftCharacter character)
        ? character.Position
        : null;

      CollectableLoot loot = Instantiate(
        original: _lootPrefab,
        position: origin ?? transform.position,
        rotation: Quaternion.identity,
        parent: GameObject.Find(GameObjectName.LootsContainer).transform
      );

      loot.CollectableLootModel = collectableLootModel;
    }
  }
}
