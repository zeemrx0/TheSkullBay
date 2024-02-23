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

    public IObjectPool<Projectile> InitProjectilePool()
    {
      return _effectStrategy.InitProjectilePool();
    }

    public void Perform(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      IObjectPool<Projectile> projectilePool
    )
    {
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
    }

    public void OnTargetAcquired(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      AbilityModel abilityModel,
      IObjectPool<Projectile> projectilePool
    )
    {
      _effectStrategy.StartEffect(
        playerBoatAbilitiesPresenter,
        playerInputActions,
        abilityModel,
        projectilePool
      );
    }
  }
}
