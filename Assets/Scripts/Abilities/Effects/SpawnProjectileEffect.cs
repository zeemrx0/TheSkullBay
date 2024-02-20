using LNE.Inputs;
using UnityEngine;

namespace LNE.Abilities.Effects
{
  [CreateAssetMenu(
    fileName = "_SpawnProjectileEffect",
    menuName = "Abilities/Effects/Spawn Projectile",
    order = 0
  )]
  public class SpawnProjectileEffect : EffectStrategy
  {
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private float _minAngle;

    [SerializeField]
    private float _maxAngle;

    [SerializeField]
    public override void StartEffect(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      AbilityModel abilityModel
    )
    {
      string abilityName = name.Split("_SpawnProjectileEffect")[0];
      Vector3 initialPosition = playerBoatAbilitiesPresenter.transform
        .Find($"{abilityName}SpawnPoint")
        .position;

      GameObject projectile = SpawnProjectile(initialPosition);

      float distance = Vector3.Distance(
        initialPosition,
        abilityModel.TargetPosition
      );

      float angle = Mathf.Lerp(
        _maxAngle,
        _minAngle,
        distance / abilityModel.AimRadius
      );

      float speed = Mathf.Sqrt(
        Physics.gravity.magnitude
          * distance
          / Mathf.Sin(2 * angle * Mathf.Deg2Rad)
      );

      float speedX = speed * Mathf.Cos(angle * Mathf.Deg2Rad);
      float speedY = speed * Mathf.Sin(angle * Mathf.Deg2Rad);

      Vector3 velocity =
        (abilityModel.TargetPosition - initialPosition).normalized * speedX
        + Vector3.up * speedY;

      playerBoatAbilitiesPresenter.View.Direction = velocity;

      projectile.GetComponent<Rigidbody>().velocity = velocity;
      projectile.transform.rotation = Quaternion.LookRotation(velocity);
    }

    private GameObject SpawnProjectile(Vector3 position)
    {
      GameObject projectile = Instantiate(_projectilePrefab);
      projectile.transform.position = position;
      return projectile;
    }
  }
}
