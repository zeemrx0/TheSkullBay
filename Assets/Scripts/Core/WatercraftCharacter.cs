using UnityEngine;

namespace LNE.Core
{
  public class WatercraftCharacter : MonoBehaviour
  {
    [SerializeField]
    private Transform _origin;
    public Vector3 Position => _origin.position;
    public Vector3 AbilitySpawnPosition =>
      new Vector3(_origin.position.x, 1, _origin.position.z);
  }
}
