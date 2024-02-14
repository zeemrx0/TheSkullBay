using UnityEngine;

namespace LNE
{
  public class FishermanBoatsSpawnController : MonoBehaviour
  {
    [SerializeField]
    private Transform _playerPosition;

    [SerializeField]
    private GameObject _boatPrefab;

    [SerializeField]
    private float _radius = 50f;

    private void Start()
    {
      SpawnBoat();
    }

    private GameObject SpawnBoat()
    {
      GameObject boat = Instantiate(_boatPrefab);
      Vector2 randomPosition = RandomPositionOnCircle(
        _playerPosition.position,
        _radius
      );
      boat.transform.position = new Vector3(
        randomPosition.x,
        0,
        randomPosition.y
      );
      return boat;
    }

    private Vector2 RandomPositionOnCircle(Vector2 center, float radius)
    {
      float angle = Random.value * Mathf.PI * 2;
      float x = center.x + Mathf.Cos(angle) * radius;
      float y = center.y + Mathf.Sin(angle) * radius;
      return new Vector2(x, y);
    }
  }
}
