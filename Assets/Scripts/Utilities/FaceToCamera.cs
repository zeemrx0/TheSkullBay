using UnityEngine;

namespace LNE.Utilities
{
  public class FaceToCamera : MonoBehaviour
  {
    private void LateUpdate()
    {
      transform.forward = Camera.main.transform.forward;
    }
  }
}
