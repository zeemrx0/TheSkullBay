using LNE.Inputs;
using UnityEngine;
using Zenject;
using Application = UnityEngine.Device.Application;

namespace LNE.Movements
{
  public class PlayerBoatMovementPresenter : BoatMovementPresenter
  {
    // Injected
    private PlayerInputPresenter _playerInputPresenter;
    private PlayerInputActions _playerInputActions;

    private Vector2 _moveInput;

    [Inject]
    public void Init(PlayerInputPresenter playerInputPresenter)
    {
      _playerInputPresenter = playerInputPresenter;
      _playerInputPresenter.Init();

      _playerInputActions = _playerInputPresenter.GetPlayerInputActions();
    }

    protected override void Awake()
    {
      base.Awake();
      _view = GetComponent<PlayerBoatMovementView>();
    }

    private void Update()
    {
      if (_gameCorePresenter.IsGameOver)
      {
        return;
      }

      if (Application.isMobilePlatform)
      {
        _moveInput = _playerInputPresenter.MoveInput;
      }
      else
      {
        _moveInput = _playerInputActions.Boat.Move.ReadValue<Vector2>();
      }

      LimitVelocity();

      Transform cameraTransform = Camera.main.transform;
      float cameraAngle = cameraTransform.eulerAngles.y;

      Vector3 moveDirection =
        Quaternion.Euler(0, cameraAngle, 0)
        * new Vector3(_moveInput.x, 0, _moveInput.y);
      moveDirection = moveDirection.normalized;

      float angle = Vector3.SignedAngle(
        transform.forward,
        moveDirection,
        Vector3.up
      );

      if (_moveInput.magnitude > 0f)
      {
        // Steer
        float steerSpeed =
          _boatMovementData.SteerSpeed * Mathf.Clamp01(Mathf.Abs(angle) / 90f);

        if (angle > 0f)
        {
          Steer(1, steerSpeed);
        }
        else
        {
          Steer(-1, steerSpeed);
        }

        if (Mathf.Abs(angle) < _boatMovementData.AngleThreshold)
        {
          MoveForward();
        }
      }

      UpdateWaterVFX();
    }

    private void MoveForward()
    {
      _view.Move(_rigidbody, 1, _boatMovementData.MoveSpeed);
    }

    private void Steer(float direction, float speed)
    {
      _view.Steer(_rigidbody, direction, speed);
    }
  }
}
