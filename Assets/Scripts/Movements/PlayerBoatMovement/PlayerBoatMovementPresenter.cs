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

    private void Update()
    {
      if (_gameCorePresenter.IsGameOver)
      {
        return;
      }

      _moveInput = _playerInputActions.Boat.Move.ReadValue<Vector2>();
      Vector2 moveDirection = new Vector2(
        _moveInput.x,
        _moveInput.y
      ).normalized;

      LimitVelocity();
      Move(moveDirection.y);
      Steer(moveDirection.x);
    }

    private void Move(float direction)
    {
      _boatMovementView.Move(
        _rigidbody,
        direction,
        _boatMovementSettings.MoveSpeed
      );
    }

    private void Steer(float direction)
    {
      _boatMovementView.Steer(
        _rigidbody,
        direction,
        _boatMovementSettings.SteerSpeed
      );
    }
  }
}
