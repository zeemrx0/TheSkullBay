using LNE.Core;
using LNE.Utilities.Constants;
using UnityEngine;

namespace LNE.Movements
{
  public abstract class BoatMovementView : MonoBehaviour
  {
    protected ParticleSystem _boatWaterVFX;

    protected virtual void Awake()
    {
      _boatWaterVFX = transform
        .GetComponentInChildren<Vehicle>()
        .transform.Find(GameObjectName.MoveWaterFX)
        .GetComponent<ParticleSystem>();
    }

    public void Move(Rigidbody rigidbody, float direction, float moveSpeed)
    {
      rigidbody.AddRelativeForce(
        direction * Vector3.forward * moveSpeed * Time.deltaTime,
        ForceMode.Force
      );
    }

    public void Steer(Rigidbody rigidbody, float direction, float steerSpeed)
    {
      rigidbody.AddRelativeTorque(
        direction * Vector3.up * steerSpeed * Time.deltaTime,
        ForceMode.Force
      );
    }

    public void SetWaterVFXRateOverTime(float rate)
    {
      ParticleSystem.EmissionModule emission = _boatWaterVFX.emission;
      emission.rateOverTime = rate;
    }
  }
}
