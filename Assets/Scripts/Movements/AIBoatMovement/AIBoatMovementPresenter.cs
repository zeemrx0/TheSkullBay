using LNE.Spawners;
using UnityEngine;

namespace LNE.Movements
{
  public class AIBoatMovementPresenter : BoatMovementPresenter
  {
    public AIBoatSpawner Spawner { get; set; }

    private AIBoatMovementModel _model;

    protected override void Awake()
    {
      base.Awake();
      _view = GetComponent<AIBoatMovementView>();
    }

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
      UpdateWaterVFX();
    }

    private void Steer()
    {
      if (_model.CheckTargetIsOnWhichSide(transform) > 0)
      {
        _view.Steer(
          _rigidbody,
          1,
          _boatMovementData.SteerSpeed
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
          _boatMovementData.SteerSpeed
            * Mathf.Clamp01(
              Mathf.Abs(_model.CheckTargetIsOnWhichSide(transform)) / 180f
            )
        );
      }
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
          _boatMovementData.MoveSpeed * Mathf.Clamp01(distance / 100f)
        );
      }
    }
  }
}
