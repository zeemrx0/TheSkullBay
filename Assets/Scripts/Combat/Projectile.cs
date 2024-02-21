using UnityEngine;

namespace LNE.Combat
{
  public class Projectile : MonoBehaviour
  {
    public string OwnerId { get; set; }

    [SerializeField]
    private GameObject _onCollideOceanParticleEffectPrefab;

    [SerializeField]
    private GameObject _onCollideObjectParticleEffectPrefab;

    [SerializeField]
    private float _damage;

    private bool _isDestroyedOnCollision = false;

    private void OnTriggerEnter(Collider other)
    {
      if (_isDestroyedOnCollision)
      {
        return;
      }

      if (other.gameObject.GetInstanceID().ToString() == OwnerId)
      {
        return;
      }

      if (other.gameObject.layer == LayerMask.NameToLayer("Ocean"))
      {
        if (_onCollideOceanParticleEffectPrefab != null)
        {
          GameObject particleEffect = Instantiate(
            _onCollideOceanParticleEffectPrefab,
            transform.position,
            Quaternion.identity
          );

          Destroy(particleEffect, 2f);
        }
      }
      else
      {
        GameObject objectParticleEffect = Instantiate(
          _onCollideObjectParticleEffectPrefab,
          transform.position,
          Quaternion.identity
        );

        Destroy(objectParticleEffect, 4f);

        other.TryGetComponent<Health>(out Health health);
        health?.TakeDamage(_damage);
      }

      _isDestroyedOnCollision = true;
      Destroy(gameObject, 0.01f);
    }
  }
}
