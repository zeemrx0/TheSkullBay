using LNE.Combat;
using LNE.Inputs;
using UnityEngine;
using UnityEngine.Pool;

namespace LNE.Abilities
{
  public abstract class EffectStrategy : ScriptableObject
  {
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
  }
}
