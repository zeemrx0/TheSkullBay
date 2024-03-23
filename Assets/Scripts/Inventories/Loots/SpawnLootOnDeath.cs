using LNE.Core;
using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Inventories.Loots
{
  public class SpawnLootOnDeath : MonoBehaviour
  {
    [SerializeField]
    private GameObject lootPrefab;

    public void SpawnLoot()
    {
      Vector3? origin = TryGetComponent<Character>(out Character character)
        ? character.Position
        : null;

      Instantiate(
        original: lootPrefab,
        position: origin ?? transform.position,
        rotation: Quaternion.identity,
        parent: GameObject.Find(GameObjectName.LootsContainer).transform
      );
    }
  }
}
