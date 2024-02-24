using UnityEngine;

namespace LNE.Combat.Trophies
{
  public class SpawnTrophyOnDeath : MonoBehaviour
  {
    [SerializeField]
    private GameObject trophyPrefab;

    public void SpawnTrophy()
    {
      Instantiate(trophyPrefab, transform.position, Quaternion.identity);
    }
  }
}
