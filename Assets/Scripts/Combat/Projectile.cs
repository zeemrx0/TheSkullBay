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
    private VFX _onCollideOceanVFXPrefab;

    [SerializeField]
    private VFX _onCollideObjectVFXPrefab;

    [SerializeField]
    private AudioClip _onCollideOceanAudioClip;

    [SerializeField]
    private AudioClip _onCollideObjectAudioClip;

    [SerializeField]
    private float _damage;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private bool _isDestroyedOnCollision = false;

    private void Awake()
    {
      _rigidbody = GetComponent<Rigidbody>();
      _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
      if (
        _isDestroyedOnCollision
        || other.gameObject.GetInstanceID().ToString() == OwnerId
      )
      {
        return;
      }

      switch (other.tag)
      {
        case TagName.Ocean:
          if (_onCollideOceanVFXPrefab != null)
          {
            SpawnVFX(_onCollideOceanVFXPrefab);
            _audioSource.PlayOneShot(_onCollideOceanAudioClip);
            Deactivate(
              Mathf.Max(
                _onCollideOceanVFXPrefab.Duration,
                _onCollideOceanAudioClip.length
              )
            );
          }

          break;

        case TagName.Projectile:
          if (other.GetComponent<Projectile>().OwnerId == OwnerId)
          {
            return;
          }
          break;

        case TagName.VFX:
          break;

        default:
          if (_onCollideObjectVFXPrefab != null)
          {
            SpawnVFX(_onCollideObjectVFXPrefab);
            _audioSource.PlayOneShot(_onCollideObjectAudioClip);

            other.TryGetComponent<HealthPresenter>(out HealthPresenter health);
            health?.TakeDamage(_damage);

            _isDestroyedOnCollision = true;
            Deactivate(
              Mathf.Max(
                _onCollideObjectVFXPrefab.Duration,
                _onCollideObjectAudioClip.length
              )
            );
          }
          break;
      }
    }

    private void SpawnVFX(VFX vfx)
    {
      if (vfx != null)
      {
        GameObject particleEffect = Instantiate(
          vfx.gameObject,
          transform.position,
          Quaternion.identity
        );

        Destroy(particleEffect, vfx.Duration);
      }
    }

    private void Deactivate(float time)
    {
      StartCoroutine(DeactivateAfterTime(time));
    }

    private IEnumerator DeactivateAfterTime(float time)
    {
      yield return new WaitForSeconds(0.01f);

      foreach (Transform child in transform)
      {
        child.gameObject.SetActive(false);
      }

      yield return new WaitForSeconds(time);

      _rigidbody.velocity = Vector3.zero;
      _rigidbody.angularVelocity = Vector3.zero;
      _isDestroyedOnCollision = false;
      foreach (Transform child in transform)
      {
        child.gameObject.SetActive(true);
      }

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
