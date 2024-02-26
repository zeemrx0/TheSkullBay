using System;
using System.Collections;
using LNE.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

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
      PlayerInputActions playerInputActions,
      AbilityModel abilityModel,
      Action onTargetAcquired
    )
    {
      _playerInputActions = playerInputActions;

      abilityModel.AimRadius = _aimRadius;

      playerBoatAbilitiesPresenter.StartCoroutine(
        Target(playerBoatAbilitiesPresenter, abilityModel, onTargetAcquired)
      );
    }

    private IEnumerator Target(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      AbilityModel abilityModel,
      Action onTargetAcquired
    )
    {
      _playerInputActions.Boat.Choose.performed += ctx =>
        HandleConfirmTargetPosition(playerBoatAbilitiesPresenter, abilityModel);
      _playerInputActions.Boat.Cancel.performed += ctx =>
        HandleCancelTargeting(playerBoatAbilitiesPresenter, abilityModel);

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
                  raycastHit.point
                  - playerBoatAbilitiesPresenter.Origin.position
                ).normalized * _aimRadius
              );
          }
          else
          {
            abilityModel.TargetPosition = raycastHit.point;
          }

          playerBoatAbilitiesPresenter.SetCircleIndicatorPosition(
            abilityModel.TargetPosition
          );
        }

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
  }
}
