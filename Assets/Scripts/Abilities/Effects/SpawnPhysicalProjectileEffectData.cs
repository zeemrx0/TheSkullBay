using LNE.Combat;
using LNE.Inputs;
using LNE.Utilities.Constants;
using UnityEngine;
using UnityEngine.Pool;

namespace LNE.Abilities.Effects
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
    private float _projectSpeed;

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
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      AbilityModel abilityModel,
      IObjectPool<Projectile> projectilePool
    )
    {
      string abilityName = name.Split(DefaultFileName)[0];
      Vector3 initialPosition = playerBoatAbilitiesPresenter.transform
        .Find($"{abilityName}{Constant.SpawnPoint}")
        .position;

      Projectile projectile = projectilePool.Get();
      projectile.transform.position = initialPosition;
      projectile.OwnerId = playerBoatAbilitiesPresenter.Id;

      float distance = Vector3.Distance(
        initialPosition,
        abilityModel.TargetPosition
      );

      float angle =
        Mathf.Asin(
          distance * Physics.gravity.magnitude / Mathf.Pow(_projectSpeed, 2)
        )
        * Mathf.Rad2Deg
        / 2;

      float speedX = _projectSpeed * Mathf.Cos(angle * Mathf.Deg2Rad);
      float speedY = _projectSpeed * Mathf.Sin(angle * Mathf.Deg2Rad);

      Vector3 velocity =
        (abilityModel.TargetPosition - initialPosition).normalized * speedX
        + Vector3.up * speedY;

      playerBoatAbilitiesPresenter.View.Direction = velocity;

      projectile.BelongingPool = projectilePool;
      projectile.SetVelocity(velocity);
      projectile.transform.rotation = Quaternion.LookRotation(velocity);
    }
  }
}
