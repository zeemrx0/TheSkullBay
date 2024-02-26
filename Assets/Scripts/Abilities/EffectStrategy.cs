using LNE.Combat;
using LNE.Inputs;
using UnityEngine;
using UnityEngine.Pool;

namespace LNE.Abilities
{
  public abstract class EffectStrategy : ScriptableObject
  {
    [field: SerializeField]
    public float ProjectSpeed { get; protected set; } = 0f;

    public virtual IObjectPool<Projectile> InitProjectilePool()
    {
      return null;
    }

    public abstract void StartEffect(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      AbilityModel abilityModel,
      IObjectPool<Projectile> projectilePool
    );

    protected virtual string GetAbilityName(string defaultFileName)
    {
      return name.Split(defaultFileName)[0];
    }
  }
}
