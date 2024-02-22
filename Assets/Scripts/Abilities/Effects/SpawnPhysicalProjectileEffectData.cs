using LNE.Combat;
using LNE.Inputs;
using LNE.Utilities.Constants;
using UnityEngine;

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
    private GameObject _projectilePrefab;

    [SerializeField]
    private float _projectSpeed;

    [SerializeField]
    public override void StartEffect(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      AbilityModel abilityModel
    )
    {
      string abilityName = name.Split(DefaultFileName)[0];
      Vector3 initialPosition = playerBoatAbilitiesPresenter.transform
        .Find($"{abilityName}{Constant.SpawnPoint}")
        .position;

      GameObject projectile = SpawnProjectile(initialPosition);
      projectile.GetComponent<Projectile>().OwnerId =
        playerBoatAbilitiesPresenter.Id;

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
