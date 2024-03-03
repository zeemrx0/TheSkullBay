using LNE.Utilities.Constants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LNE.Combat.Abilities
{
  public class PlayerBoatAbilitiesView : MonoBehaviour
  {
    [field: SerializeField]
    public RectTransform Origin { get; private set; }
    public Vector3 Direction { get; set; }

    [SerializeField]
    private RectTransform _rangeIndicator;

    [SerializeField]
    private RectTransform _circleIndicator;

    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    private GameObject[] _abilityButtons;

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

    public void ShowPhysicalProjectileTrajectory()
    {
      _lineRenderer.gameObject.SetActive(true);
    }

    public void HidePhysicalProjectileTrajectory()
    {
      _lineRenderer.gameObject.SetActive(false);
    }

    public void SetPhysicalProjectileTrajectory(
      Vector3 initialPosition,
      Vector3 velocity
    )
    {
      float maxTime = 2 * Mathf.Abs(velocity.y) / Physics.gravity.magnitude;
      float timeStep = 0.1f;
      int steps = Mathf.RoundToInt(maxTime / timeStep * 0.8f);

      Vector3[] positions = new Vector3[steps];

      _lineRenderer.positionCount = steps;

      for (int i = 0; i < steps; i++)
      {
        float t = i * timeStep;
        positions[i] = initialPosition + velocity * t;
        positions[i].y =
          initialPosition.y + velocity.y * t + 0.5f * Physics.gravity.y * t * t;

        _lineRenderer.SetPosition(i, positions[i]);
      }
    }

    public void SetAbilityButtonIconActive(int index, bool active)
    {
      _abilityButtons[index].transform
        .Find(GameObjectName.Border)
        .Find(GameObjectName.Icon)
        .gameObject.SetActive(active);
    }

    public void SetAbilityButtonIcon(int index, Sprite icon)
    {
      _abilityButtons[index].transform
        .Find(GameObjectName.Border)
        .Find(GameObjectName.Icon)
        .GetComponent<Image>()
        .sprite = icon;
    }

    public void SetAbilityCooldownTime(
      int index,
      float remainingTime,
      float initialTime
    )
    {
      _abilityButtons[index].transform
        .Find(GameObjectName.Border)
        .Find(GameObjectName.Overlay)
        .GetComponent<Image>()
        .fillAmount = initialTime == 0 ? 0 : (remainingTime / initialTime);

      _abilityButtons[index].transform
        .Find(GameObjectName.Border)
        .Find(GameObjectName.CooldownTimeText)
        .GetComponent<TextMeshProUGUI>()
        .text = initialTime == 0 ? "" : Mathf.Ceil(remainingTime).ToString();
    }

    private void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawRay(Origin.position, Direction);
    }
  }
}
