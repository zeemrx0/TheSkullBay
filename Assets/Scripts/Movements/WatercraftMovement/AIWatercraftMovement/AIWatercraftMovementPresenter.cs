using LNE.Spawners;
using UnityEngine;

namespace LNE.Movements
{
  public class AIWatercraftMovementPresenter : WatercraftMovementPresenter
  {
    public AIWatercraftCharacterSpawner Spawner { get; set; }

    [field: SerializeField]
    public float FieldOfViewRadius { get; private set; } = 200f;
    private AIWatercraftMovementModel _model;
    private float _targetAngle;

    protected override void Awake()
    {
      base.Awake();
      _view = GetComponent<AIWatercraftMovementView>();
    }

    private void Start()
    {
      _model = new AIWatercraftMovementModel
      {
        CurrentPosition = new Vector2(
          transform.position.x,
          transform.position.z
        )
      };

      _model.RandomNewTargetPosition(
        Spawner.transform.position,
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
          Spawner.transform.position,
          Spawner.Radius
        );
      }

      UpdateWaterVFX();
    }

    private void FixedUpdate()
    {
      if (_gameCorePresenter.IsGameOver)
      {
        return;
      }

      Steer();
      Move();
      LimitVelocity();
    }

    private void Steer()
    {
      _targetAngle = _model.CheckTargetIsOnWhichSide(transform.eulerAngles.y);

      if (_targetAngle > 0)
      {
        _view.Steer(
          _rigidbody,
          1,
          _watercraftMovementData.SteerSpeed
            * Mathf.Clamp01(Mathf.Abs(_targetAngle) / 30f)
        );
      }
      else
      {
        _view.Steer(
          _rigidbody,
          -1,
          _watercraftMovementData.SteerSpeed
            * Mathf.Clamp01(Mathf.Abs(_targetAngle) / 30f)
        );
      }
    }

    private void Move()
    {
      _model.CurrentPosition = transform.position;

      float distance = Vector3.Distance(
        _model.CurrentPosition,
        _model.TargetPosition
      );

      if (!_model.IsArrived && Mathf.Abs(_targetAngle) < 5f)
      {
        _view.Move(
          _rigidbody,
          1,
          _watercraftMovementData.MoveSpeed * Mathf.Clamp01(distance / 100f)
        );
      }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
      _model.TargetPosition = targetPosition;
    }
  }
}
