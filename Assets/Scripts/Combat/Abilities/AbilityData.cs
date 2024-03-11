using LNE.Inputs;
using UnityEngine;
using UnityEngine.Pool;

namespace LNE.Combat.Abilities
{
  [CreateAssetMenu(
    fileName = DefaultFileName,
    menuName = "Abilities/Ability",
    order = 0
  )]
  public class AbilityData : ScriptableObject
  {
    public const string DefaultFileName = "_AbilityData";
    public Sprite Icon;

    [SerializeField]
    private TargetingStrategy _targetingStrategy;

    [SerializeField]
    private EffectStrategy _effectStrategy;

    [SerializeField]
    private float _cooldownTime;

    public IObjectPool<Projectile> InitProjectilePool()
    {
      return _effectStrategy.InitProjectilePool();
    }

    public bool Perform(
      WatercraftAbilitiesPresenter watercraftAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      Joystick joystick,
      IObjectPool<Projectile> projectilePool,
      AbilityModel abilityModel
    )
    {
      if (
        watercraftAbilitiesPresenter.GetAbilityCooldownRemainingTime(this) > 0
      )
      {
        return false;
      }

      string abilityName = GetAbilityName(DefaultFileName);

      abilityModel.InitialPosition =
        watercraftAbilitiesPresenter.FindAbilitySpawnPosition(abilityName);
      abilityModel.ProjectSpeed = _effectStrategy.ProjectSpeed;

      _targetingStrategy.StartTargeting(
        watercraftAbilitiesPresenter,
        playerInputPresenter,
        joystick,
        abilityModel,
        () =>
        {
          OnTargetAcquired(
            watercraftAbilitiesPresenter,
            playerInputPresenter,
            abilityModel,
            projectilePool
          );
        }
      );

      return true;
    }

    public void OnTargetAcquired(
      WatercraftAbilitiesPresenter watercraftAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      AbilityModel abilityModel,
      IObjectPool<Projectile> projectilePool
    )
    {
      watercraftAbilitiesPresenter.StartCooldown(this, _cooldownTime);

      _effectStrategy.StartEffect(
        watercraftAbilitiesPresenter,
        playerInputPresenter,
        abilityModel,
        projectilePool
      );
    }

    private string GetAbilityName(string defaultFileName)
    {
      return name.Split(defaultFileName)[0];
    }

    public float AimRadius => _targetingStrategy.AimRadius;

    public float ProjectileSpeed => _effectStrategy.ProjectSpeed;
  }
}
