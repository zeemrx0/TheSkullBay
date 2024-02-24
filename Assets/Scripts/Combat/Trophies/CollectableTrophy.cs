using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Combat.Trophies
{
  public class CollectableTrophy : MonoBehaviour
  {
    [SerializeField]
    private CollectingStrategy _collectingStrategy;

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag(TagName.Player))
      {
        _collectingStrategy.StartCollecting(
          other.GetComponent<PlayerTrophiesPresenter>()
        );

        Destroy(gameObject);
      }
    }
  }
}
