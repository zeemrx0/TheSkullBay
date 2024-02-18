using System;
using UnityEngine;

namespace LNE.Movements
{
  public abstract class BoatMovementPresenter : MonoBehaviour
  {
    [SerializeField]
    protected BoatMovementSettingsSO _boatMovementSettings;

    protected Rigidbody _rigidbody;

    protected void LimitVelocity()
    {
      if (
        Math.Abs(_rigidbody.velocity.magnitude)
        > _boatMovementSettings.MaxMoveSpeed
      )
      {
        float fraction =
          _boatMovementSettings.MaxMoveSpeed / _rigidbody.velocity.magnitude;
          
        _rigidbody.velocity = new Vector3(
          _rigidbody.velocity.x * fraction,
          _rigidbody.velocity.y * fraction,
          _rigidbody.velocity.z * fraction
        );
      }

      if (
        Math.Abs(_rigidbody.angularVelocity.y)
        > _boatMovementSettings.MaxSteerSpeed
      )
      {
        _rigidbody.angularVelocity = new Vector3(
          _rigidbody.angularVelocity.x,
          _boatMovementSettings.MaxSteerSpeed
            * Math.Sign(_rigidbody.angularVelocity.y),
          _rigidbody.angularVelocity.z
        );
      }
    }
  }
}
