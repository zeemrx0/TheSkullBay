using UnityEngine;
using UnityEngine.Events;

namespace LNE.Interactions
{
  public class Interactable : MonoBehaviour
  {
    [SerializeField]
    public UnityEvent<GameObject> OnInteracting;

    public void Interact(GameObject interactingPerson)
    {
      OnInteracting.Invoke(interactingPerson);
    }
  }
}
