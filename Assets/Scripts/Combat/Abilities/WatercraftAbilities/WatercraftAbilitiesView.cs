using LNE.Utilities.Constants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LNE.Combat.Abilities
{
  public abstract class WatercraftAbilitiesView : MonoBehaviour
  {
    [SerializeField]
    protected RectTransform _rangeIndicator;

    [SerializeField]
    protected RectTransform _circleIndicator;

    [SerializeField]
    protected LineRenderer _lineRenderer;

    [SerializeField]
    protected GameObject[] _abilityButtons;

    protected AudioSource _audioSource;

    protected virtual void Awake()
    {
      _audioSource = gameObject.AddComponent<AudioSource>();
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
    #endregion

    public float PlayAudioClip(AudioClip audioClip)
    {
      _audioSource.PlayOneShot(audioClip);
      return audioClip.length;
    }
  }
}
