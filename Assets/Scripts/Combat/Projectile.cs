using System.Collections;
using LNE.Core;
using LNE.Utilities.Constants;
using UnityEngine;
using UnityEngine.Pool;

namespace LNE.Combat
{
  public class Projectile : MonoBehaviour
  {
    public Character Owner { get; set; }
    public IObjectPool<Projectile> BelongingPool { get; set; }
    public float AliveRange { get; set; } = 1000f;

    [SerializeField]
    private VFX _onCollideOceanVFXPrefab;

    [SerializeField]
    private VFX _onCollideObjectVFXPrefab;

    [SerializeField]
    private AudioClip _onCollideOceanSound;

    [SerializeField]
    private AudioClip _onCollideObjectSound;

    [SerializeField]
    private float _damage;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private bool _isDestroyedOnCollision = false;
    private Vector3 _lastOwnerPosition;

    private void Awake()
    {
      _rigidbody = GetComponent<Rigidbody>();
      _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
      other.gameObject.TryGetComponent<Character>(out Character owner);
      if (_isDestroyedOnCollision || owner == Owner)
      {
        return;
      }

      switch (other.tag)
      {
        case TagName.Ocean:
          if (_onCollideOceanVFXPrefab != null)
          {
            SpawnVFX(_onCollideOceanVFXPrefab);
            _audioSource.PlayOneShot(_onCollideOceanSound);
            Deactivate(
              Mathf.Max(
                _onCollideOceanVFXPrefab.Duration,
                _onCollideOceanSound.length
              )
            );
          }

          break;

        case TagName.Projectile:
          if (other.GetComponent<Projectile>().Owner == Owner)
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
            _audioSource.PlayOneShot(_onCollideObjectSound);

            other.TryGetComponent<HealthPresenter>(out HealthPresenter health);
            health?.TakeDamage(_damage);

            _isDestroyedOnCollision = true;
            Deactivate(
              Mathf.Max(
                _onCollideObjectVFXPrefab.Duration,
                _onCollideObjectSound.length
              )
            );
          }
          break;
      }
    }

    private void Update()
    {
      if (Owner != null)
      {
        _lastOwnerPosition = Owner.transform.position;
      }

      if (Vector3.Distance(transform.position, _lastOwnerPosition) > AliveRange)
      {
        Deactivate(0);
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

      GameObject child = transform.GetChild(0).gameObject;
      child?.SetActive(false);

      yield return new WaitForSeconds(time);

      _rigidbody.velocity = Vector3.zero;
      _rigidbody.angularVelocity = Vector3.zero;
      _isDestroyedOnCollision = false;

      child?.SetActive(true);

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

    public void SetUseGravity(bool useGravity)
    {
      _rigidbody.useGravity = useGravity;
    }
  }
}
