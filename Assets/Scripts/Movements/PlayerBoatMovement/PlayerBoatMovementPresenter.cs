using System;
using LNE.Inputs;
using UnityEngine;
using Zenject;

namespace LNE.Movements
{
  public class PlayerBoatMovementPresenter : BoatMovementPresenter
  {
    [SerializeField]
    private BoatMovementView _boatMovementView;

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

    private void Awake()
    {
      _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
      _moveInput = _playerInputActions.Boat.Move.ReadValue<Vector2>();

      LimitVelocity();
      Move();
    }

    private void Move()
    {
      Vector2 moveDirection = new Vector2(
        _moveInput.x,
        _moveInput.y
      ).normalized;

      _boatMovementView.Move(
        _rigidbody,
        moveDirection.y,
        _boatMovementSettings.MoveSpeed
      );

      _boatMovementView.Steer(
        _rigidbody,
        moveDirection.x,
        _boatMovementSettings.SteerSpeed
      );
    }
  }
}
