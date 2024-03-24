using UnityEngine;

namespace LNE.Utilities
{
  public class FastForwardParticleEffect : MonoBehaviour
  {
    [SerializeField]
    private ParticleSystem particleSystem;

    [SerializeField]
    private float time = 0;

    private void Awake()
    {
      particleSystem.Simulate(time, true, false);
      particleSystem.Play();
    }
  }
}
