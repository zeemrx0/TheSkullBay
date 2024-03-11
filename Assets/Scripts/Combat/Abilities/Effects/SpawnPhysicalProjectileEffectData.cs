using LNE.Core;
using UnityEngine;
using UnityEngine.Pool;

namespace LNE.Combat.Abilities.Effects
{
  [CreateAssetMenu(
    fileName = DefaultFileName,
    menuName = "Abilities/Effects/Spawn Projectile",
    order = 0
  )]
  public class SpawnPhysicalProjectileEffectData : EffectStrategy
  {
    public const string DefaultFileName = "_SpawnPhysicalProjectileEffectData";

    [SerializeField]
    private Projectile _projectilePrefab;

    [SerializeField]
    private VFX _projectVFX;

    [SerializeField]
    private AudioClip _audioClip;

    public override IObjectPool<Projectile> InitProjectilePool()
    {
      return new ObjectPool<Projectile>(
        () => Instantiate(_projectilePrefab),
        pooledProjectile => pooledProjectile.gameObject.SetActive(true),
        pooledProjectile => pooledProjectile.gameObject.SetActive(false),
        pooledProjectile => Destroy(pooledProjectile.gameObject),
        true,
        30,
        30
      );
    }

    public override void StartEffect(
      WatercraftAbilitiesPresenter watercraftAbilitiesPresenter,
      AbilityModel abilityModel,
      IObjectPool<Projectile> projectilePool
    )
    {
      string abilityName = GetAbilityName(DefaultFileName);
      abilityModel.InitialPosition =
        watercraftAbilitiesPresenter.FindAbilitySpawnPosition(abilityName);

      Projectile projectile = projectilePool.Get();
      projectile.transform.position = abilityModel.InitialPosition;
      projectile.Owner =
        watercraftAbilitiesPresenter.transform.GetComponent<Character>();

      Vector3 velocity = abilityModel.GetPhysicalProjectVelocity();

      watercraftAbilitiesPresenter.PlayAudioClip(_audioClip);

      VFX instantiatedProjectVFX = Instantiate(
        _projectVFX,
        abilityModel.InitialPosition,
        Quaternion.LookRotation(velocity)
      );

      Destroy(instantiatedProjectVFX.gameObject, _projectVFX.Duration);

      projectile.BelongingPool = projectilePool;
      projectile.SetVelocity(
        velocity + watercraftAbilitiesPresenter.GetCurrentVelocity()
      );
      projectile.transform.rotation = Quaternion.LookRotation(velocity);
    }
  }
}
