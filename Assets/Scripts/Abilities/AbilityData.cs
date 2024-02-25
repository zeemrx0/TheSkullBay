using LNE.Combat;
using LNE.Inputs;
using UnityEngine;
using UnityEngine.Pool;

namespace LNE.Abilities
{
  [CreateAssetMenu(
    fileName = "_AbilityData",
    menuName = "Abilities/Ability",
    order = 0
  )]
  public class AbilityData : ScriptableObject
  {
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
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      IObjectPool<Projectile> projectilePool
    )
    {
      if (
        playerBoatAbilitiesPresenter.GetAbilityCooldownRemainingTime(this) > 0
      )
      {
        return false;
      }

      AbilityModel abilityModel = new AbilityModel();

      _targetingStrategy.StartTargeting(
        playerBoatAbilitiesPresenter,
        playerInputActions,
        abilityModel,
        () =>
        {
          OnTargetAcquired(
            playerBoatAbilitiesPresenter,
            playerInputActions,
            abilityModel,
            projectilePool
          );
        }
      );

      return true;
    }

    public void OnTargetAcquired(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      AbilityModel abilityModel,
      IObjectPool<Projectile> projectilePool
    )
    {
      playerBoatAbilitiesPresenter.StartCooldown(this, _cooldownTime);

      _effectStrategy.StartEffect(
        playerBoatAbilitiesPresenter,
        playerInputActions,
        abilityModel,
        projectilePool
      );
    }
  }
}
