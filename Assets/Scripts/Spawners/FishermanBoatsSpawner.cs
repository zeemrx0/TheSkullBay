using LNE.Core;
using LNE.Movements;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace LNE.Spawners
{
  public class FishermanBoatsSpawnController : MonoBehaviour
  {
    [SerializeField]
    private GameObject _boatPrefab;

    [SerializeField]
    private float _radius = 40f;

    // Injected
    private GameCorePresenter _gameCorePresenter;

    [Inject]
    public void Init(GameCorePresenter gameCorePresenter)
    {
      _gameCorePresenter = gameCorePresenter;
    }

    private void Start()
    {
      SpawnBoat();
    }

    private GameObject SpawnBoat()
    {
      GameObject boat = Instantiate(_boatPrefab);
      boat.GetComponent<BoatMovementPresenter>().Init(_gameCorePresenter);
      
      Vector2 randomPosition = RandomPositionOnCircle(
        new Vector2(transform.position.x, transform.position.z),
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
      float randomAngle = Random.value * Mathf.PI * 2;
      float randomRadius = Random.value * radius;
      float x = center.x + Mathf.Cos(randomAngle) * randomRadius;
      float y = center.y + Mathf.Sin(randomAngle) * randomRadius;
      return new Vector2(x, y);
    }

    private void OnDrawGizmosSelected()
    {
      Handles.color = Color.blue;
      Handles.DrawWireArc(
        transform.position,
        Vector3.up,
        Vector3.forward,
        360,
        _radius
      );
    }
  }
}
