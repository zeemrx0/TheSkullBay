using LNE.Core;
using LNE.Movements;
using LNE.Utilities.Constants;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace LNE.Spawners
{
  public class AIBoatSpawner : MonoBehaviour
  {
    [field: SerializeField]
    public float Radius { get; private set; } = 50f;

    [SerializeField]
    private GameObject _boatPrefab;

    private GameObject _aiBoatsContainer;

    // Injected
    private GameCorePresenter _gameCorePresenter;

    [Inject]
    public void Init(GameCorePresenter gameCorePresenter)
    {
      _gameCorePresenter = gameCorePresenter;
    }

    private void Start()
    {
      _aiBoatsContainer = GameObject.Find(GameObjectName.AIBoatsContainer);

      SpawnBoat();
    }

    private GameObject SpawnBoat()
    {
      GameObject boat = Instantiate(
        original: _boatPrefab,
        parent: _aiBoatsContainer.transform
      );
      boat.GetComponent<BoatMovementPresenter>().Init(_gameCorePresenter);
      boat.GetComponent<AIBoatMovementPresenter>().Spawner = this;

      Vector2 randomPosition = RandomPositionOnCircle(
        new Vector2(transform.position.x, transform.position.z),
        Radius
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
        Radius
      );
    }
  }
}
