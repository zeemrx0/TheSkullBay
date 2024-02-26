using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Combat.Loots
{
  public class CollectableLoot : MonoBehaviour
  {
    [SerializeField]
    private CollectingStrategy _collectingStrategy;

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag(TagName.Player))
      {
        _collectingStrategy.StartCollecting(
          other.GetComponent<PlayerLootsPresenter>()
        );

        Destroy(gameObject);
      }
    }
  }
}
