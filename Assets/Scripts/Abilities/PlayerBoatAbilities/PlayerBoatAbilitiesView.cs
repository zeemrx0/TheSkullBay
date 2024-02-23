using UnityEngine;

namespace LNE.Abilities
{
  public class PlayerBoatAbilitiesView : MonoBehaviour
  {
    [field: SerializeField]
    public RectTransform Origin { get; private set; }

    [SerializeField]
    private RectTransform _rangeIndicator;

    [SerializeField]
    private RectTransform _circleIndicator;

    // Test
    public Vector3 Direction { get; set; }

    public void SetRangeIndicatorSize(Vector2 size)
    {
      _rangeIndicator.sizeDelta = size;
    }

    public void SetCircleIndicatorPosition(Vector3 position)
    {
      _circleIndicator.position = position;
    }

    public void SetCircleIndicatorSize(Vector2 size)
    {
      _circleIndicator.sizeDelta = size;
    }

    public void ShowCircleIndicator()
    {
      _circleIndicator.gameObject.SetActive(true);
    }

    public void HideCircleIndicator()
    {
      _circleIndicator.gameObject.SetActive(false);
    }

    public void ShowRangeIndicator()
    {
      _rangeIndicator.gameObject.SetActive(true);
    }

    public void HideRangeIndicator()
    {
      _rangeIndicator.gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected() {
      Gizmos.color = Color.red;
      Gizmos.DrawRay(Origin.position, Direction);
    }
  }
}
