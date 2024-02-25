using LNE.Spawners;
using UnityEngine;

namespace LNE.Movements
{
  public class AIBoatMovementPresenter : BoatMovementPresenter
  {
    public AIBoatSpawner Spawner { get; set; }

    [SerializeField]
    private BoatMovementView _view;

    private AIBoatMovementModel _model;

    private void Start()
    {
      _model = new AIBoatMovementModel
      {
        CurrentPosition = new Vector2(
          transform.position.x,
          transform.position.z
        )
      };

      _model.RandomNewTargetPosition(
        new Vector2(Spawner.transform.position.x, Spawner.transform.position.z),
        Spawner.Radius
      );
    }

    private void Update()
    {
      if (_gameCorePresenter.IsGameOver)
      {
        return;
      }

      _model.CheckIfArrived();
      if (_model.IsArrived)
      {
        _model.RandomNewTargetPosition(
          new Vector2(
            Spawner.transform.position.x,
            Spawner.transform.position.z
          ),
          Spawner.Radius
        );
      }

      LimitVelocity();
      Move();
      Steer();
    }

    private void Move()
    {
      _model.CurrentPosition = new Vector2(
        transform.position.x,
        transform.position.z
      );

      float distance = Vector2.Distance(
        _model.CurrentPosition,
        _model.TargetPosition
      );

      if (
        !_model.IsArrived
        && Mathf.Abs(_model.CheckTargetIsOnWhichSide(transform)) < 5f
      )
      {
        _view.Move(
          _rigidbody,
          1,
          _boatMovementSettings.MoveSpeed * Mathf.Clamp01(distance / 100f)
        );
      }
    }

    private void Steer()
    {
      if (_model.CheckTargetIsOnWhichSide(transform) > 0)
      {
        _view.Steer(
          _rigidbody,
          1,
          _boatMovementSettings.SteerSpeed
            * Mathf.Clamp01(
              Mathf.Abs(_model.CheckTargetIsOnWhichSide(transform)) / 30f
            )
        );
      }
      else
      {
        _view.Steer(
          _rigidbody,
          -1,
          _boatMovementSettings.SteerSpeed
            * Mathf.Clamp01(
              Mathf.Abs(_model.CheckTargetIsOnWhichSide(transform)) / 180f
            )
        );
      }
    }

    private void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawRay(transform.position, transform.forward * 50);

      Gizmos.color = Color.red;
      Gizmos.DrawLine(
        transform.position,
        new Vector3(_model.TargetPosition.x, 0, _model.TargetPosition.y)
      );
    }
  }
}
