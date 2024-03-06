using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Core
{
  public class Boat : MonoBehaviour
  {
    private Transform _origin;
    public Vector3 Position => _origin.position;

    public void Awake()
    {
      _origin = transform.Find(GameObjectName.Origin);
    }
  }
}
