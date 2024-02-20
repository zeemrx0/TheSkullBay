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

    private Vector3 _targetPosition;

    private PlayerInputActions _playerInputActions;
    private PlayerBoatAbilitiesView _playerBoatAbilitiesView;
    private bool _hasConfirmedOrCanceledTargetPosition = false;

    public override void StartTargeting(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      AbilityModel abilityModel,
      Action onTargetAcquired
    )
    {
      _playerInputActions = playerInputActions;
      _playerBoatAbilitiesView = playerBoatAbilitiesPresenter.View;

      playerBoatAbilitiesPresenter.StartCoroutine(
        Target(abilityModel, onTargetAcquired)
      );
    }

    private IEnumerator Target(
      AbilityModel abilityModel,
      Action onTargetAcquired
    )
    {
      _playerInputActions.Boat.Choose.performed += HandleConfirmTargetPosition;
      _playerInputActions.Boat.Cancel.performed += HandleCancelTargeting;

      _playerBoatAbilitiesView.ShowRangeIndicator();
      _playerBoatAbilitiesView.ShowCircleIndicator();

      _playerBoatAbilitiesView.SetRangeIndicatorSize(
        new Vector2(_aimRadius * 2f, _aimRadius * 2f)
      );

      _playerBoatAbilitiesView.SetCircleIndicatorSize(
        new Vector2(_targetRadius * 2f, _targetRadius * 2f)
      );

      _hasConfirmedOrCanceledTargetPosition = false;

      while (
        !abilityModel.IsCancelled && !_hasConfirmedOrCanceledTargetPosition
      )
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

        if (_hasConfirmedOrCanceledTargetPosition)
        {
          break;
        }

        yield return null;
      }

      onTargetAcquired?.Invoke();
    }

    private void HandleCancelTargeting(InputAction.CallbackContext context)
    {
      _playerInputActions.Boat.Choose.performed -= HandleConfirmTargetPosition;
      _playerInputActions.Boat.Cancel.performed -= HandleCancelTargeting;

      _playerBoatAbilitiesView.HideRangeIndicator();
      _playerBoatAbilitiesView.HideCircleIndicator();

      _hasConfirmedOrCanceledTargetPosition = true;
    }

    private void HandleConfirmTargetPosition(
      InputAction.CallbackContext context
    )
    {
      _playerInputActions.Boat.Choose.performed -= HandleConfirmTargetPosition;
      _playerInputActions.Boat.Cancel.performed -= HandleCancelTargeting;

      Debug.Log("Shoot!");
      _playerBoatAbilitiesView.HideRangeIndicator();
      _playerBoatAbilitiesView.HideCircleIndicator();

      _hasConfirmedOrCanceledTargetPosition = true;
    }
  }
}
