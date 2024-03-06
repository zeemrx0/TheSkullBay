using System;
using LNE.Core;
using UnityEngine;
using Zenject;

namespace LNE.Movements
{
  public abstract class BoatMovementPresenter : MonoBehaviour
  {
    [SerializeField]
    protected BoatMovementData _boatMovementData;

    [SerializeField]
    protected BoatMovementView _view;

    // Injected
    protected GameCorePresenter _gameCorePresenter;

    protected Rigidbody _rigidbody;

    [Inject]
    public void Init(GameCorePresenter gameCorePresenter)
    {
      _gameCorePresenter = gameCorePresenter;
      _gameCorePresenter.OnGameOver += HandleGameOver;
    }

    protected virtual void Awake()
    {
      _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable() { }

    private void OnDisable()
    {
      _gameCorePresenter.OnGameOver -= HandleGameOver;
    }

    protected void LimitVelocity()
    {
      if (
        Math.Abs(_rigidbody.velocity.magnitude) > _boatMovementData.MaxMoveSpeed
      )
      {
        float fraction =
          _boatMovementData.MaxMoveSpeed / _rigidbody.velocity.magnitude;

        _rigidbody.velocity = new Vector3(
          _rigidbody.velocity.x * fraction,
          _rigidbody.velocity.y * fraction,
          _rigidbody.velocity.z * fraction
        );
      }

      if (
        Math.Abs(_rigidbody.angularVelocity.y) > _boatMovementData.MaxSteerSpeed
      )
      {
        _rigidbody.angularVelocity = new Vector3(
          _rigidbody.angularVelocity.x,
          _boatMovementData.MaxSteerSpeed
            * Math.Sign(_rigidbody.angularVelocity.y),
          _rigidbody.angularVelocity.z
        );
      }
    }

    private void HandleGameOver()
    {
      _rigidbody.velocity = Vector3.zero;
      _rigidbody.angularVelocity = Vector3.zero;
    }

    protected void UpdateWaterVFX()
    {
      _view.SetWaterVFXRateOverTime(
        _rigidbody.velocity.magnitude * 20
          + _rigidbody.angularVelocity.magnitude * 80
      );
    }
  }
}
