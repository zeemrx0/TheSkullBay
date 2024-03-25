using LNE.Inputs;
using UnityEngine;
using Zenject;
using Application = UnityEngine.Device.Application;

namespace LNE.Movements
{
  public class PlayerWatercraftMovementPresenter : WatercraftMovementPresenter
  {
    // Injected
    private PlayerInputPresenter _playerInputPresenter;
    private PlayerInputActions _playerInputActions;

    private Camera _mainCamera;
    private Vector2 _moveInput;

    [Inject]
    public void Construct(PlayerInputPresenter playerInputPresenter)
    {
      _playerInputPresenter = playerInputPresenter;
      _playerInputPresenter.Init();

      _playerInputActions = _playerInputPresenter.GetPlayerInputActions();
    }

    protected override void Awake()
    {
      base.Awake();
      _view = GetComponent<PlayerWatercraftMovementView>();
      _mainCamera = Camera.main;
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
        _moveInput = _playerInputActions.Watercraft.Move.ReadValue<Vector2>();
      }

      UpdateWaterVFX();
    }

    private void FixedUpdate()
    {
      if (_gameCorePresenter.IsGameOver)
      {
        return;
      }
      if (_gameCorePresenter.IsGameOver)
      {
        return;
      }

      LimitVelocity();

      Transform cameraTransform = _mainCamera.transform;
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
          _watercraftMovementData.SteerSpeed
          * Mathf.Clamp01(Mathf.Abs(angle) / 30f);

        if (angle > 0f)
        {
          Steer(1, steerSpeed);
        }
        else
        {
          Steer(-1, steerSpeed);
        }

        if (Mathf.Abs(angle) < _watercraftMovementData.AngleThreshold)
        {
          MoveForward();
        }
      }
    }

    private void MoveForward()
    {
      _view.Move(_rigidbody, 1, _watercraftMovementData.MoveSpeed);
    }

    private void Steer(float direction, float speed)
    {
      _view.Steer(_rigidbody, direction, speed);
    }
  }
}
