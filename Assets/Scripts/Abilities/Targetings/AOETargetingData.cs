using System;
using System.Collections;
using LNE.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LNE.Abilities.Targeting
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

    private PlayerInputActions _playerInputActions;
    private PlayerBoatAbilitiesView _playerBoatAbilitiesView;

    public override void StartTargeting(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      AbilityModel abilityModel,
      Action onTargetAcquired
    )
    {
      _playerInputActions = playerInputActions;
      _playerBoatAbilitiesView = playerBoatAbilitiesPresenter.View;

      abilityModel.AimRadius = _aimRadius;

      playerBoatAbilitiesPresenter.StartCoroutine(
        Target(abilityModel, onTargetAcquired)
      );
    }

    private IEnumerator Target(
      AbilityModel abilityModel,
      Action onTargetAcquired
    )
    {
      _playerInputActions.Boat.Choose.performed += ctx =>
        HandleConfirmTargetPosition(abilityModel);
      _playerInputActions.Boat.Cancel.performed += ctx =>
        HandleCancelTargeting(abilityModel);

      _playerBoatAbilitiesView.ShowRangeIndicator();
      _playerBoatAbilitiesView.ShowCircleIndicator();

      _playerBoatAbilitiesView.SetRangeIndicatorSize(
        new Vector2(_aimRadius * 2f, _aimRadius * 2f)
      );

      _playerBoatAbilitiesView.SetCircleIndicatorSize(
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
              _playerBoatAbilitiesView.Origin.position
            ) > _aimRadius
          )
          {
            abilityModel.TargetPosition =
              _playerBoatAbilitiesView.Origin.position
              + (
                (
                  raycastHit.point - _playerBoatAbilitiesView.Origin.position
                ).normalized * _aimRadius
              );
          }
          else
          {
            abilityModel.TargetPosition = raycastHit.point;
          }

          _playerBoatAbilitiesView.SetCircleIndicatorPosition(
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

    private void HandleCancelTargeting(AbilityModel abilityModel)
    {
      UnsubscribeFromInputEvents(abilityModel);

      _playerBoatAbilitiesView.HideRangeIndicator();
      _playerBoatAbilitiesView.HideCircleIndicator();

      abilityModel.IsCancelled = true;
    }

    private void HandleConfirmTargetPosition(AbilityModel abilityModel)
    {
      UnsubscribeFromInputEvents(abilityModel);

      _playerBoatAbilitiesView.HideRangeIndicator();
      _playerBoatAbilitiesView.HideCircleIndicator();

      abilityModel.IsPerformed = true;
    }

    private void UnsubscribeFromInputEvents(AbilityModel abilityModel)
    {
      _playerInputActions.Boat.Choose.performed -= ctx =>
        HandleConfirmTargetPosition(abilityModel);
      _playerInputActions.Boat.Cancel.performed -= ctx =>
        HandleCancelTargeting(abilityModel);
    }
  }
}
