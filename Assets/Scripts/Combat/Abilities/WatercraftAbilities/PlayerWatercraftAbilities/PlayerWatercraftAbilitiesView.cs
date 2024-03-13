using UnityEngine;

namespace LNE.Combat.Abilities
{
  public class PlayerWatercraftAbilitiesView : WatercraftAbilitiesView
  {
    [SerializeField]
    private RectTransform _rangeIndicator;

    [SerializeField]
    private RectTransform _circleIndicator;

    [SerializeField]
    private LineRenderer _physicalTrajectoryRenderer;

    [SerializeField]
    private LineRenderer _aimRayRenderer;

    [SerializeField]
    private AbilityButton[] _abilityButtons;

    private void Start() { }

    public void SetAbilityButtonIconActive(int index, bool active)
    {
      _abilityButtons[index].SetIconActive(active);
    }

    public void SetAbilityButtonIcon(int index, Sprite icon)
    {
      _abilityButtons[index].SetIcon(icon);
    }

    public void SetAbilityCooldownTime(
      int index,
      float remainingTime,
      float initialTime
    )
    {
      _abilityButtons[index].SetCooldownTime(remainingTime, initialTime);
    }

    #region  Range Indicator
    public void ShowRangeIndicator()
    {
      _rangeIndicator.gameObject.SetActive(true);
    }

    public void HideRangeIndicator()
    {
      _rangeIndicator.gameObject.SetActive(false);
    }

    public void SetRangeIndicatorSize(Vector2 size)
    {
      _rangeIndicator.sizeDelta = size;
    }

    #endregion

    #region Aim Indicator

    public void ShowCircleIndicator()
    {
      _circleIndicator.gameObject.SetActive(true);
    }

    public void HideCircleIndicator()
    {
      _circleIndicator.gameObject.SetActive(false);
    }

    public void SetCircleIndicatorPosition(Vector3 position)
    {
      _circleIndicator.position = position;
    }

    public void SetCircleIndicatorSize(Vector2 size)
    {
      _circleIndicator.sizeDelta = size;
    }
    #endregion

    #region Physical Projectile Trajectory
    public void ShowPhysicalProjectileTrajectory()
    {
      _physicalTrajectoryRenderer.gameObject.SetActive(true);
    }

    public void HidePhysicalProjectileTrajectory()
    {
      _physicalTrajectoryRenderer.gameObject.SetActive(false);
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

      _physicalTrajectoryRenderer.positionCount = steps;

      for (int i = 0; i < steps; i++)
      {
        float t = i * timeStep;
        positions[i] = initialPosition + velocity * t;
        positions[i].y =
          initialPosition.y + velocity.y * t + 0.5f * Physics.gravity.y * t * t;

        _physicalTrajectoryRenderer.SetPosition(i, positions[i]);
      }
    }
    #endregion

    #region Aim Ray
    public void ShowAimRay()
    {
      _aimRayRenderer.gameObject.SetActive(true);
    }

    public void HideAimRay()
    {
      _aimRayRenderer.gameObject.SetActive(false);
    }

    public void SetAimRay(Vector3 start, Vector3 end)
    {
      _aimRayRenderer.positionCount = 2;
      _aimRayRenderer.SetPosition(0, start);
      _aimRayRenderer.SetPosition(1, end);
    }
    #endregion
  }
}
