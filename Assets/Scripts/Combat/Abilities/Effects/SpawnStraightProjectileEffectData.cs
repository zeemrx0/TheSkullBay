using LNE.Core;
using UnityEngine;
using UnityEngine.Pool;

namespace LNE.Combat.Abilities
{
  [CreateAssetMenu(
    fileName = DefaultFileName,
    menuName = "Abilities/Effects/Spawn Straight Projectile",
    order = 0
  )]
  public class SpawnStraightProjectileEffectData : EffectStrategy
  {
    public const string DefaultFileName = "_SpawnStraightProjectileEffectData";

    [SerializeField]
    private Projectile _projectilePrefab;

    [SerializeField]
    private VFX _projectVFX;

    [SerializeField]
    private AudioClip _projectSound;

    public override IObjectPool<Projectile> InitProjectilePool()
    {
      return new ObjectPool<Projectile>(
        () => Instantiate(_projectilePrefab),
        pooledProjectile => pooledProjectile.gameObject.SetActive(true),
        pooledProjectile => pooledProjectile.gameObject.SetActive(false),
        pooledProjectile => Destroy(pooledProjectile.gameObject),
        true,
        10,
        10
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
      projectile.SetUseGravity(false);
      projectile.transform.position = abilityModel.InitialPosition;
      projectile.Owner =
        watercraftAbilitiesPresenter.transform.GetComponent<Character>();
      projectile.AliveRange = abilityModel.AimRadius;

      Vector3 velocity = abilityModel.GetStraightProjectVelocity();

      watercraftAbilitiesPresenter.PlayAudioClip(_projectSound);

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
