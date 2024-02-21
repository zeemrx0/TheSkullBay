using LNE.Combat;
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
    private float _speed;

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
      projectile.GetComponent<Projectile>().OwnerId =
        playerBoatAbilitiesPresenter.Id;

      float distance = Vector3.Distance(
        initialPosition,
        abilityModel.TargetPosition
      );

      float angle =
        Mathf.Asin(distance * Physics.gravity.magnitude / Mathf.Pow(_speed, 2))
        * Mathf.Rad2Deg
        / 2;

      float speedX = _speed * Mathf.Cos(angle * Mathf.Deg2Rad);
      float speedY = _speed * Mathf.Sin(angle * Mathf.Deg2Rad);

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
