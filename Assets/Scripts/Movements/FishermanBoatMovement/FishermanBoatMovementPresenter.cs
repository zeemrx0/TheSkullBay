using UnityEngine;

namespace LNE.Movements
{
  public class FishermanBoatMovementPresenter : BoatMovementPresenter
  {
    [SerializeField]
    private BoatMovementView _boatMovementView;

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
        + new Vector2(Random.Range(-200, 200), Random.Range(-200, 200));
    }

    private void Update()
    {
      LimitVelocity();
      Move();
      Steer();
    }

    private void Move()
    {
      _currentPosition = new Vector2(
        transform.position.x,
        transform.position.z
      );

      float distance = Vector2.Distance(_currentPosition, _targetPosition);

      if (
        distance > 10f
        && Mathf.Abs(CheckTargetIsOnWhichSide(_targetPosition)) < 5f
      )
      {
        _boatMovementView.Move(
          _rigidbody,
          1,
          _boatMovementSettings.MoveSpeed * Mathf.Clamp01(distance / 100f)
        );
      }
    }

    private void Steer()
    {
      if (CheckTargetIsOnWhichSide(_targetPosition) > 0)
      {
        _boatMovementView.Steer(
          _rigidbody,
          1,
          _boatMovementSettings.SteerSpeed
            * Mathf.Clamp01(
              Mathf.Abs(CheckTargetIsOnWhichSide(_targetPosition)) / 180f
            )
        );
      }
      else
      {
        _boatMovementView.Steer(
          _rigidbody,
          -1,
          _boatMovementSettings.SteerSpeed
            * Mathf.Clamp01(
              Mathf.Abs(CheckTargetIsOnWhichSide(_targetPosition)) / 180f
            )
        );
      }
    }

    // Positive = Right, Negative = Left
    public float CheckTargetIsOnWhichSide(Vector2 target)
    {
      _targetPosition = target;

      float alpha = -transform.eulerAngles.y + 90f;
      while (alpha < 0)
      {
        alpha += 360;
      }

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
        beta = Mathf.PI * 2f - Mathf.Atan(slopeAbs);
      }

      // Beta to degrees
      beta = beta * 180f / Mathf.PI;
      beta %= 360;

      float betaMinusAlpha = beta - alpha;
      while (betaMinusAlpha < 0)
      {
        betaMinusAlpha += 360;
      }
      float alphaMinusBeta = alpha - beta;
      while (alphaMinusBeta < 0)
      {
        alphaMinusBeta += 360;
      }
      if (betaMinusAlpha < alphaMinusBeta)
      {
        return -betaMinusAlpha;
      }
      else
      {
        return alphaMinusBeta;
      }
    }

    private void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawRay(transform.position, transform.forward * 50);

      Gizmos.color = Color.red;
      Gizmos.DrawLine(
        transform.position,
        new Vector3(_targetPosition.x, 0, _targetPosition.y)
      );
    }
  }
}
