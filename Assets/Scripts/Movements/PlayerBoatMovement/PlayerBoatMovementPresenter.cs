using System;
using LNE.Inputs;
using LNE.Utilities;
using UnityEngine;
using Zenject;

namespace LNE.Movements
{
  public class PlayerBoatMovementPresenter : MonoBehaviour
  {
    [SerializeField]
    private BoatMovementSettingsSO _boatMovementSettings;

    [SerializeField]
    private PlayerBoatMovementView _playerBoatMovementView;

    // Injected
    private PlayerInput _playerInput;
    private PlayerInputActions _playerInputActions;

    private Rigidbody _rigidbody;
    private Transform _steerPosition;
    private Vector2 _moveInput;

    [Inject]
    public void Init(PlayerInput playerInput)
    {
      _playerInput = playerInput;
      _playerInput.Init();

      _playerInputActions = _playerInput.GetPlayerInputActions();
    }

    private void Awake()
    {
      _rigidbody = GetComponent<Rigidbody>();
      _steerPosition = transform.Find(Constant.SteerPosition);
    }

    private void Update()
    {
      _moveInput = _playerInputActions.Boat.Move.ReadValue<Vector2>();

      Move();

      LimitVelocity();
    }

    private void Move()
    {
      Vector2 moveDirection = new Vector2(
        _moveInput.x,
        _moveInput.y
      ).normalized;

      _playerBoatMovementView.Move(
        _rigidbody,
        moveDirection,
        _boatMovementSettings.MoveSpeed,
        _boatMovementSettings.SteerSpeed,
        _steerPosition
      );
    }

    private void LimitVelocity()
    {
      if (Math.Abs(_rigidbody.velocity.x) > _boatMovementSettings.MaxMoveSpeed)
      {
        _rigidbody.velocity = new Vector3(
          _rigidbody.velocity.x,
          _rigidbody.velocity.y,
          _boatMovementSettings.MaxMoveSpeed
            * _boatMovementSettings.MaxMoveSpeed
        );
      }

      if (Math.Abs(_rigidbody.velocity.z) > _boatMovementSettings.MaxSteerSpeed)
      {
        _rigidbody.velocity = new Vector3(
          _boatMovementSettings.MaxSteerSpeed
            * _boatMovementSettings.MaxSteerSpeed,
          _rigidbody.velocity.y,
          _rigidbody.velocity.z
        );
      }
    }
  }
}
