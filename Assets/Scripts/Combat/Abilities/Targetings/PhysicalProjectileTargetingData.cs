using System;
using System.Collections;
using LNE.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using Application = UnityEngine.Device.Application;

namespace LNE.Combat.Abilities.Targeting
{
  [CreateAssetMenu(
    fileName = DefaultFileName,
    menuName = "Abilities/Targeting/Physical Projectile Targeting",
    order = 0
  )]
  public class PhysicalProjectileTargetingData : TargetingStrategy
  {
    public const string DefaultFileName = "_PhysicalProjectileTargetingData";

    [SerializeField]
    private float _aimRadius;

    [SerializeField]
    private LayerMask _layerMask;

    public override void StartTargeting(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      Joystick joystick,
      AbilityModel abilityModel,
      Action onTargetAcquired
    )
    {
      PlayerInputActions playerInputActions =
        playerInputPresenter.GetPlayerInputActions();

      abilityModel.AimRadius = _aimRadius;

      playerBoatAbilitiesPresenter.StartCoroutine(
        Target(
          playerBoatAbilitiesPresenter,
          playerInputPresenter,
          joystick,
          abilityModel,
          onTargetAcquired
        )
      );
    }

    private IEnumerator Target(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      Joystick joystick,
      AbilityModel abilityModel,
      Action onTargetAcquired
    )
    {
      PlayerInputActions playerInputActions =
        playerInputPresenter.GetPlayerInputActions();

      playerInputActions.Boat.Choose.performed += ctx =>
        HandleConfirmTargetPosition(
          playerBoatAbilitiesPresenter,
          playerInputPresenter,
          abilityModel
        );
      playerInputActions.Boat.Cancel.performed += ctx =>
        HandleCancelTargeting(
          playerBoatAbilitiesPresenter,
          playerInputPresenter,
          abilityModel
        );

      playerBoatAbilitiesPresenter.ShowPhysicalProjectileTrajectory();

      while (!abilityModel.IsPerformed && !abilityModel.IsCancelled)
      {
        string abilityName = GetAbilityName(DefaultFileName);
        abilityModel.InitialPosition =
          playerBoatAbilitiesPresenter.FindAbilitySpawnPosition(abilityName);

        if (Application.isMobilePlatform)
        {
          Debug.Log(
            playerBoatAbilitiesPresenter.GetJoystickDirection(joystick)
          );
          
          FindTargetPositionMobile(
            playerBoatAbilitiesPresenter,
            abilityModel,
            playerBoatAbilitiesPresenter.GetJoystickDirection(joystick)
          );
        }
        else
        {
          FindTargetPositionPC(
            playerBoatAbilitiesPresenter,
            abilityModel,
            playerInputPresenter.CurrentMousePosition
          );
        }

        playerBoatAbilitiesPresenter.SetPhysicalProjectileTrajectory(
          abilityModel.InitialPosition,
          abilityModel.GetProjectVelocity()
        );

        if (abilityModel.IsPerformed)
        {
          break;
        }

        yield return null;
      }

      if (abilityModel.IsPerformed)
      {
        onTargetAcquired?.Invoke();

        HandleConfirmTargetPosition(
          playerBoatAbilitiesPresenter,
          playerInputPresenter,
          abilityModel
        );
      }
    }

    private void FindTargetPositionMobile(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      AbilityModel abilityModel,
      Vector3 aimDirection
    )
    {
      if (aimDirection.magnitude > 0)
      {
        abilityModel.TargetPosition = new Vector3(
          playerBoatAbilitiesPresenter.Origin.position.x
            + aimDirection.x * _aimRadius,
          playerBoatAbilitiesPresenter.Origin.position.y,
          playerBoatAbilitiesPresenter.Origin.position.z
            + aimDirection.z * _aimRadius
        );
      }
    }

    private void FindTargetPositionPC(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      AbilityModel abilityModel,
      Vector2 mousePosition
    )
    {
      if (
        Physics.Raycast(
          Camera.main.ScreenPointToRay(mousePosition),
          out RaycastHit raycastHit,
          Mathf.Infinity,
          _layerMask
        )
      )
      {
        if (
          Vector3.Distance(
            raycastHit.point,
            playerBoatAbilitiesPresenter.Origin.position
          ) > _aimRadius
        )
        {
          abilityModel.TargetPosition =
            playerBoatAbilitiesPresenter.Origin.position
            + (
              (
                raycastHit.point - playerBoatAbilitiesPresenter.Origin.position
              ).normalized * _aimRadius
            );
        }
        else
        {
          abilityModel.TargetPosition = raycastHit.point;
        }
      }
    }
  }
}
