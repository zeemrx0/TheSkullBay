using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
  public UnityEvent onPlayerEnter;

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      onPlayerEnter.Invoke();
    }
  }
}
