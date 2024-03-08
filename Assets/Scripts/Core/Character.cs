using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Core
{
  public class Character : MonoBehaviour
  {
    private Transform _origin;
    public Vector3 Position => _origin.position;
    public Vector3 AbilitySpawnPosition =>
      new Vector3(_origin.position.x, 1, _origin.position.z);

    public void Awake()
    {
      _origin = transform.Find(GameObjectName.Origin);
    }
  }
}
