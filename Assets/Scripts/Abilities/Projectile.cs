using UnityEngine;

namespace LNE.Abilities
{
  public class Projectile : MonoBehaviour
  {
    [SerializeField]
    public GameObject onCollideOceanParticleEffectPrefab;

    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.layer == LayerMask.NameToLayer("Ocean"))
      {
        if (onCollideOceanParticleEffectPrefab != null)
        {
          GameObject particleEffect = Instantiate(
            onCollideOceanParticleEffectPrefab,
            transform.position,
            Quaternion.identity
          );

          Destroy(particleEffect, 2f);
        }

        Destroy(gameObject);
      }
    }
  }
}
