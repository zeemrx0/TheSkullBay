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
      PlayerWatercraftAbilitiesPresenter playerBoatAbilitiesPresenter,
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
      PlayerWatercraftAbilitiesPresenter playerBoatAbilitiesPresenter,
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

      playerBoatAbilitiesPresenter.ShowRangeIndicator();
      playerBoatAbilitiesPresenter.ShowPhysicalProjectileTrajectory();

      playerBoatAbilitiesPresenter.SetRangeIndicatorSize(
        new Vector2(_aimRadius * 2f, _aimRadius * 2f)
      );

      while (!abilityModel.IsPerformed && !abilityModel.IsCancelled)
      {
        string abilityName = GetAbilityName(DefaultFileName);
        abilityModel.InitialPosition =
          playerBoatAbilitiesPresenter.FindAbilitySpawnPosition(abilityName);

        if (Application.isMobilePlatform)
        {
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
      PlayerWatercraftAbilitiesPresenter playerBoatAbilitiesPresenter,
      AbilityModel abilityModel,
      Vector3 aimDirection
    )
    {
      if (aimDirection.magnitude > 0)
      {
        abilityModel.TargetPosition = new Vector3(
          playerBoatAbilitiesPresenter.Origin.x + aimDirection.x * _aimRadius,
          playerBoatAbilitiesPresenter.Origin.y,
          playerBoatAbilitiesPresenter.Origin.z + aimDirection.z * _aimRadius
        );
      }
    }

    private void FindTargetPositionPC(
      PlayerWatercraftAbilitiesPresenter playerBoatAbilitiesPresenter,
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
            playerBoatAbilitiesPresenter.Origin
          ) > _aimRadius
        )
        {
          abilityModel.TargetPosition =
            playerBoatAbilitiesPresenter.Origin
            + (
              (
                raycastHit.point - playerBoatAbilitiesPresenter.Origin
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
