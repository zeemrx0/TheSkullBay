using System.Collections;
using UnityEngine;

namespace LNE.Movements
{
  public class FishermanBoatMovementPresenter : MonoBehaviour
  {
    [SerializeField]
    private BoatMovementSettingsSO _boatMovementSettings;

    [SerializeField]
    private BoatMovementView _boatMovementView;

    private Rigidbody _rigidbody;
    private Vector2 _targetPosition;
    private Vector2 _currentPosition;

    private void Awake()
    {
      _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
      _currentPosition = new Vector2(
        transform.position.x,
        transform.position.z
      );

      _targetPosition =
        _currentPosition
        + new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));

      StartCoroutine(Steer());
    }

    private IEnumerator Steer()
    {
      while (true)
      {
        if (CheckTargetIsOnWhichSide(_targetPosition))
        {
          _boatMovementView.Steer(
            _rigidbody,
            1,
            _boatMovementSettings.SteerSpeed
          );
        }
        else
        {
          _boatMovementView.Steer(
            _rigidbody,
            -1,
            _boatMovementSettings.SteerSpeed
          );
        }

        yield return new WaitForSeconds(0.1f);
      }
    }

    // True = Right, False = Left
    public bool CheckTargetIsOnWhichSide(Vector2 target)
    {
      _targetPosition = target;

      float alpha = -transform.eulerAngles.y + 90f;
      alpha = alpha < 0 ? alpha + 360 : alpha;

      float slopeAbs =
        Mathf.Abs(target.y - _currentPosition.y)
        / Mathf.Abs(target.x - _currentPosition.x);

      float beta = 0;

      // Quadrant I
      if (
        target.x - _currentPosition.x > 0 && target.y - _currentPosition.y > 0
      )
      {
        beta = Mathf.Atan(slopeAbs);
      }
      else
      // Quadrant II
      if (
        target.x - _currentPosition.x < 0 && target.y - _currentPosition.y > 0
      )
      {
        beta = Mathf.PI - Mathf.Atan(slopeAbs);
      }
      // Quadrant III
      else if (
        target.x - _currentPosition.x < 0 && target.y - _currentPosition.y < 0
      )
      {
        beta = Mathf.PI + Mathf.Atan(slopeAbs);
      }
      // Quadrant IV
      else
      {
        beta = 3 * Mathf.PI * 2 + Mathf.Atan(slopeAbs);
      }

      // Beta to degrees
      beta = beta * 180f / Mathf.PI;

      if (beta - alpha > 180 || (alpha - beta < 180 && alpha - beta > 0))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    private void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawRay(transform.position, transform.forward * 100);
      Gizmos.DrawLine(
        transform.position,
        new Vector3(_targetPosition.x, 0, _targetPosition.y)
      );
    }
  }
}
