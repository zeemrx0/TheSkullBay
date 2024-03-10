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
      PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      Joystick joystick,
      AbilityModel abilityModel,
      Action onTargetAcquired
    )
    {
      PlayerInputActions playerInputActions =
        playerInputPresenter.GetPlayerInputActions();

      abilityModel.AimRadius = _aimRadius;

      playerWatercraftAbilitiesPresenter.StartCoroutine(
        Target(
          playerWatercraftAbilitiesPresenter,
          playerInputPresenter,
          joystick,
          abilityModel,
          onTargetAcquired
        )
      );
    }

    private IEnumerator Target(
      PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      Joystick joystick,
      AbilityModel abilityModel,
      Action onTargetAcquired
    )
    {
      PlayerInputActions playerInputActions =
        playerInputPresenter.GetPlayerInputActions();

      playerInputActions.Watercraft.Choose.performed += ctx =>
        HandleConfirmTargetPosition(
          playerWatercraftAbilitiesPresenter,
          playerInputPresenter,
          abilityModel
        );
      playerInputActions.Watercraft.Cancel.performed += ctx =>
        HandleCancelTargeting(
          playerWatercraftAbilitiesPresenter,
          playerInputPresenter,
          abilityModel
        );

      playerWatercraftAbilitiesPresenter.ShowRangeIndicator();
      playerWatercraftAbilitiesPresenter.ShowPhysicalProjectileTrajectory();

      playerWatercraftAbilitiesPresenter.SetRangeIndicatorSize(
        new Vector2(_aimRadius * 2f, _aimRadius * 2f)
      );

      while (!abilityModel.IsPerformed && !abilityModel.IsCancelled)
      {
        string abilityName = GetAbilityName(DefaultFileName);
        abilityModel.InitialPosition =
          playerWatercraftAbilitiesPresenter.FindAbilitySpawnPosition(abilityName);

        if (Application.isMobilePlatform)
        {
          FindTargetPositionMobile(
            playerWatercraftAbilitiesPresenter,
            abilityModel,
            playerWatercraftAbilitiesPresenter.GetJoystickDirection(joystick)
          );
        }
        else
        {
          FindTargetPositionPC(
            playerWatercraftAbilitiesPresenter,
            abilityModel,
            playerInputPresenter.CurrentMousePosition
          );
        }

        playerWatercraftAbilitiesPresenter.SetPhysicalProjectileTrajectory(
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
          playerWatercraftAbilitiesPresenter,
          playerInputPresenter,
          abilityModel
        );
      }
    }

    private void FindTargetPositionMobile(
      PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
      AbilityModel abilityModel,
      Vector3 aimDirection
    )
    {
      if (aimDirection.magnitude > 0)
      {
        abilityModel.TargetPosition = new Vector3(
          playerWatercraftAbilitiesPresenter.Origin.x + aimDirection.x * _aimRadius,
          playerWatercraftAbilitiesPresenter.Origin.y,
          playerWatercraftAbilitiesPresenter.Origin.z + aimDirection.z * _aimRadius
        );
      }
    }

    private void FindTargetPositionPC(
      PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
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
            playerWatercraftAbilitiesPresenter.Origin
          ) > _aimRadius
        )
        {
          abilityModel.TargetPosition =
            playerWatercraftAbilitiesPresenter.Origin
            + (
              (
                raycastHit.point - playerWatercraftAbilitiesPresenter.Origin
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
