using System;
using System.Collections;
using LNE.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using Application = UnityEngine.Device.Application;

namespace LNE.Combat.Abilities.Targeting
{
  [CreateAssetMenu(
    fileName = "_AOETargetingData",
    menuName = "Abilities/Targeting/AOE Targeting",
    order = 0
  )]
  public class AOETargetingData : TargetingStrategy
  {
    [SerializeField]
    private float _aimRadius;

    [SerializeField]
    private float _targetRadius;

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
      abilityModel.AimRadius = _aimRadius;

      playerBoatAbilitiesPresenter.StartCoroutine(
        Target(
          playerBoatAbilitiesPresenter,
          playerInputPresenter,
          abilityModel,
          onTargetAcquired
        )
      );
    }

    private IEnumerator Target(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputPresenter playerInputPresenter,
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
      playerBoatAbilitiesPresenter.ShowCircleIndicator();

      playerBoatAbilitiesPresenter.SetRangeIndicatorSize(
        new Vector2(_aimRadius * 2f, _aimRadius * 2f)
      );

      playerBoatAbilitiesPresenter.SetCircleIndicatorSize(
        new Vector2(_targetRadius * 2f, _targetRadius * 2f)
      );

      while (!abilityModel.IsPerformed && !abilityModel.IsCancelled)
      {
        Vector3 mousePosition = new Vector3(
          Mouse.current.position.ReadValue().x,
          Mouse.current.position.ReadValue().y,
          0
        );
        if (Application.isMobilePlatform)
        {
          // TODO: Implement mobile targeting
        }
        else
        {
          FindTargetPositionPC(
            playerBoatAbilitiesPresenter,
            abilityModel,
            mousePosition
          );
        }

        playerBoatAbilitiesPresenter.SetCircleIndicatorPosition(
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
      }
    }

    private void FindTargetPositionPC(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      AbilityModel abilityModel,
      Vector3 mousePosition
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
