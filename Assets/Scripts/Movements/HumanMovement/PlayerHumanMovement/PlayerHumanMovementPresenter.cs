using System;
using LNE.Core;
using LNE.Inputs;
using UnityEngine;
using Zenject;
using ECM2;

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
    private Rigidbody _rigidbody;
    private Character _character;
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
      _rigidbody = GetComponent<Rigidbody>();
      _character = GetComponent<Character>();
      _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
      if (_gameCorePresenter.IsGameOver)
      {
        return;
      }

      Move();
    }

    private void Move()
    {
      Transform cameraTransform = _mainCamera.transform;
      float cameraAngle = cameraTransform.eulerAngles.y;

      Vector3 moveDirection = (
        Quaternion.Euler(0, cameraAngle, 0)
        * new Vector3(
          _playerInputPresenter.MoveInput.x,
          0,
          _playerInputPresenter.MoveInput.y
        )
      ).normalized;

      _model.MoveVelocity = new Vector2(
        moveDirection.x * _humanMovementData.MoveSpeed,
        moveDirection.z * _humanMovementData.MoveSpeed
      );

      _character.SetMovementDirection(moveDirection);
      _view.Move(_model);
    }
  }
}
