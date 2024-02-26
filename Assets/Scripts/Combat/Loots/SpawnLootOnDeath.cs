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
      Instantiate(
        original: lootPrefab,
        position: transform.position,
        rotation: Quaternion.identity,
        parent: GameObject.Find(GameObjectName.LootsContainer).transform
      );
    }
  }
}
