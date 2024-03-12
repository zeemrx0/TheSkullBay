using System;
using System.Collections;
using LNE.Inputs;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace LNE.Combat.Abilities
{
  [CreateAssetMenu(
    fileName = DefaultFileName,
    menuName = "Abilities/Targeting/Directional Ray Targeting",
    order = 0
  )]
  public class RaycastTargetingData : TargetingStrategy
  {
    public const string DefaultFileName = "_RaycastTargetingData";

    public override void StartTargeting(
      WatercraftAbilitiesPresenter watercraftAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      Joystick joystick,
      AbilityModel abilityModel,
      Action onTargetAcquired
    )
    {
      abilityModel.AimRadius = AimRadius;

      if (
        watercraftAbilitiesPresenter
        is PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter
      )
      {
        playerWatercraftAbilitiesPresenter.StartCoroutine(
          PlayerTargetCoroutine(
            playerWatercraftAbilitiesPresenter,
            playerInputPresenter,
            joystick,
            abilityModel,
            onTargetAcquired
          )
        );
      }
      else if (
        watercraftAbilitiesPresenter
        is AIWatercraftAbilitiesPresenter aiWatercraftAbilitiesPresenter
      )
      {
        // TODO: Implement AI targeting
      }
    }

    private IEnumerator PlayerTargetCoroutine(
      PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
      Joystick joystick,
      AbilityModel abilityModel,
      Action onTargetAcquired
    )
    {
      playerWatercraftAbilitiesPresenter.ShowAimRay();

      while (!abilityModel.IsPerformed && !abilityModel.IsCancelled)
      {
        string abilityName = GetAbilityName(DefaultFileName);
        abilityModel.InitialPosition =
          playerWatercraftAbilitiesPresenter.FindAbilitySpawnPosition(
            abilityName
          );

        if (Application.isMobilePlatform)
        {
          FindTargetPositionMobile(
            abilityModel,
            playerWatercraftAbilitiesPresenter.GetJoystickDirection(joystick)
          );
        }
        else
        {
          // TODO: Implement PC targeting
        }

        playerWatercraftAbilitiesPresenter.SetAimRay(
          abilityModel.InitialPosition,
          abilityModel.TargetPosition
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
      AbilityModel abilityModel,
      Vector3 aimDirection
    )
    {
      abilityModel.TargetPosition =
        abilityModel.InitialPosition + aimDirection.normalized * AimRadius;
    }
  }
}
