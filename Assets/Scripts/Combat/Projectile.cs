using System.Collections;
using LNE.Utilities.Constants;
using UnityEngine;
using UnityEngine.Pool;

namespace LNE.Combat
{
  public class Projectile : MonoBehaviour
  {
    public string OwnerId { get; set; }
    public IObjectPool<Projectile> BelongingPool { get; set; }

    [SerializeField]
    private GameObject _onCollideOceanParticleEffectPrefab;

    [SerializeField]
    private GameObject _onCollideObjectParticleEffectPrefab;

    [SerializeField]
    private float _damage;

    private Rigidbody _rigidbody;
    private bool _isDestroyedOnCollision = false;

    private void Awake()
    {
      _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
      if (_isDestroyedOnCollision)
      {
        return;
      }

      if (
        other.gameObject.GetInstanceID().ToString() == OwnerId
        || other.gameObject.tag == TagName.VFX
      )
      {
        return;
      }

      if (other.gameObject.layer == LayerMask.NameToLayer(LayerName.Ocean))
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
      Deactivate(0.01f);
    }

    private void Deactivate(float time)
    {
      StartCoroutine(DeactivateAfterTime(time));
    }

    private IEnumerator DeactivateAfterTime(float time)
    {
      yield return new WaitForSeconds(time);

      _rigidbody.velocity = Vector3.zero;
      _rigidbody.angularVelocity = Vector3.zero;
      _isDestroyedOnCollision = false;

      BelongingPool.Release(this);
    }

    public void SetVelocity(Vector3 velocity)
    {
      _rigidbody.velocity = velocity;
    }

    public void SetAngularVelocity(Vector3 angularVelocity)
    {
      _rigidbody.angularVelocity = angularVelocity;
    }
  }
}
