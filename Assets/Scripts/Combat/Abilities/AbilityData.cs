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
      PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      Joystick joystick,
      IObjectPool<Projectile> projectilePool,
      AbilityModel abilityModel
    )
    {
      if (
        playerWatercraftAbilitiesPresenter.GetAbilityCooldownRemainingTime(this) > 0
      )
      {
        return false;
      }

      string abilityName = GetAbilityName(DefaultFileName);

      abilityModel.InitialPosition =
        playerWatercraftAbilitiesPresenter.FindAbilitySpawnPosition(abilityName);
      abilityModel.ProjectSpeed = _effectStrategy.ProjectSpeed;

      _targetingStrategy.StartTargeting(
        playerWatercraftAbilitiesPresenter,
        playerInputPresenter,
        joystick,
        abilityModel,
        () =>
        {
          OnTargetAcquired(
            playerWatercraftAbilitiesPresenter,
            playerInputPresenter,
            abilityModel,
            projectilePool
          );
        }
      );

      return true;
    }

    public void OnTargetAcquired(
      PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      AbilityModel abilityModel,
      IObjectPool<Projectile> projectilePool
    )
    {
      playerWatercraftAbilitiesPresenter.StartCooldown(this, _cooldownTime);

      _effectStrategy.StartEffect(
        playerWatercraftAbilitiesPresenter,
        playerInputPresenter.GetPlayerInputActions(),
        abilityModel,
        projectilePool
      );
    }

    private string GetAbilityName(string defaultFileName)
    {
      return name.Split(defaultFileName)[0];
    }
  }
}
