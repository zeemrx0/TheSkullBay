using LNE.Core;
using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Combat.Loots
{
  public class SpawnLootOnDeath : MonoBehaviour
  {
    [SerializeField]
    private GameObject lootPrefab;

    public void SpawnTrophy()
    {
      Vector3? origin = TryGetComponent<Character>(out Character boat)
        ? boat.Position
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
