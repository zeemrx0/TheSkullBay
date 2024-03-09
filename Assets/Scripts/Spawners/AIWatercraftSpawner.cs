using LNE.Core;
using LNE.Movements;
using LNE.Utilities.Constants;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace LNE.Spawners
{
  public class AIWatercraftCharacterSpawner : MonoBehaviour
  {
    [field: SerializeField]
    public float Radius { get; private set; } = 50f;

    [SerializeField]
    private Character _characterPrefab;

    private GameObject _aiWatercraftCharactersContainer;

    // Injected
    private GameCorePresenter _gameCorePresenter;

    [Inject]
    public void Init(GameCorePresenter gameCorePresenter)
    {
      _gameCorePresenter = gameCorePresenter;
    }

    private void Start()
    {
      _aiWatercraftCharactersContainer = GameObject.Find(
        GameObjectName.AIWatercraftCharactersContainer
      );

      SpawnCharacter();
    }

    private Character SpawnCharacter()
    {
      Character character = Instantiate(
        original: _characterPrefab,
        parent: _aiWatercraftCharactersContainer.transform
      );
      character
        .GetComponent<WatercraftMovementPresenter>()
        .Init(_gameCorePresenter);
      character.GetComponent<AIWatercraftMovementPresenter>().Spawner = this;

      Vector2 randomPosition = RandomPositionOnCircle(
        new Vector2(transform.position.x, transform.position.z),
        Radius
      );
      character.transform.position = new Vector3(
        randomPosition.x,
        0,
        randomPosition.y
      );
      return character;
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
#if UNITY_EDITOR
      Handles.color = Color.blue;
      Handles.DrawWireArc(
        transform.position,
        Vector3.up,
        Vector3.forward,
        360,
        Radius
      );
#endif
    }
  }
}
