using LNE.Core;
using UnityEngine;

namespace LNE.Combat.Abilities
{
  public class AIWatercraftAbilitiesPresenter : WatercraftAbilitiesPresenter
  {
    public Character Target { get; set; }
    private HealthPresenter _healthPresenter;

    private bool canPerformAbilities = true;

    protected override void Awake()
    {
      base.Awake();
      _view = GetComponent<AIWatercraftAbilitiesView>();
      _healthPresenter = GetComponent<HealthPresenter>();
    }

    private void OnEnable()
    {
      _healthPresenter.OnDie += HandleDie;
    }

    private void OnDisable()
    {
      _healthPresenter.OnDie -= HandleDie;
    }

    private Vector3 GetTargetPosition()
    {
      return Target.Position;
    }

    public Vector3 GetPredictiveTargetPosition()
    {
      Vector3 targetVelocity = Target.GetComponent<Rigidbody>().velocity;

      float timeToImpact =
        GetDistanceToTarget() / _abilityDataList[0].ProjectileSpeed;

      return GetTargetPosition() + targetVelocity * timeToImpact;
    }

    public float GetDistanceToTarget()
    {
      return Vector3.Distance(transform.position, Target.Position);
    }

    public bool PerformAbilities()
    {
      if (!canPerformAbilities)
      {
        return false;
      }

      for (int i = 0; i < _abilityDataList.Count; ++i)
      {
        AbilityData ability = _abilityDataList[i];

        if (Target != null && GetDistanceToTarget() <= ability.AimRadius)
        {
          AbilityModel abilityModel = new AbilityModel();

          ability.Perform(this, null, null, _projectilePools[i], abilityModel);
        }
      }

      return true;
    }

    private void HandleDie()
    {
      canPerformAbilities = false;
    }
  }
}
