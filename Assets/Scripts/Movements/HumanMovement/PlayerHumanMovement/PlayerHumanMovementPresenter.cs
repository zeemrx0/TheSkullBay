using LNE.Core;
using LNE.Inputs;
using UnityEngine;
using Zenject;

namespace LNE.Movements.Human
{
  public class PlayerHumanMovementPresenter : MonoBehaviour
  {
    [SerializeField]
    private HumanMovementData _humanMovementData;

    #region Injected
    private GameCorePresenter _gameCorePresenter;
    private PlayerInputPresenter _playerInputPresenter;
    #endregion

    private PlayerHumanMovementView _view;
    private Camera _mainCamera;
    private HumanMovementModel _model = new HumanMovementModel();

    [Inject]
    public void Construct(
      GameCorePresenter gameCorePresenter,
      PlayerInputPresenter playerInputPresenter
    )
    {
      _gameCorePresenter = gameCorePresenter;
      _playerInputPresenter = playerInputPresenter;
    }

    private void Awake()
    {
      _view = GetComponent<PlayerHumanMovementView>();
      _mainCamera = Camera.main;
    }

    private void Update()
    {
      if (_gameCorePresenter.IsGameOver)
      {
        return;
      }
    }

    private void FixedUpdate()
    {
      if (_gameCorePresenter.IsGameOver)
      {
        return;
      }

      ApplyGravity();

      Move();
    }

    private void Move()
    {
      Transform cameraTransform = _mainCamera.transform;
      float cameraAngle = cameraTransform.eulerAngles.y;

      Vector3 moveDirection =
        Quaternion.Euler(0, cameraAngle, 0)
        * new Vector3(
          _playerInputPresenter.MoveInput.x,
          0,
          _playerInputPresenter.MoveInput.y
        );

      _model.MoveDirection = new Vector3(
        moveDirection.x,
        _model.MoveDirection.y,
        moveDirection.z
      );

      Vector3 velocity = new Vector3(
        _model.MoveDirection.x * _humanMovementData.MoveSpeed / 10f,
        _model.MoveDirection.y,
        _model.MoveDirection.z * _humanMovementData.MoveSpeed / 10f
      );

      _view.Move(velocity);
    }

    private void ApplyGravity()
    {
      if (_model.IsGrounded && _model.MoveDirection.y < 0.0f)
      {
        _model.MoveDirection.y = -2f;
      }
      else
      {
        _model.MoveDirection.y +=
          Physics.gravity.y * Time.fixedDeltaTime * Time.fixedDeltaTime;
      }
    }
  }
}
