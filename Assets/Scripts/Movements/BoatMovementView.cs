using UnityEngine;

namespace LNE.Movements
{
  public class BoatMovementView : MonoBehaviour
  {
    [SerializeField]
    private ParticleSystem _boatWaterVFX;

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
