// using System;
// using System.Collections;
// using LNE.Inputs;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using Application = UnityEngine.Device.Application;

// namespace LNE.Combat.Abilities.Targeting
// {
//   [CreateAssetMenu(
//     fileName = "_AOETargetingData",
//     menuName = "Abilities/Targeting/AOE Targeting",
//     order = 0
//   )]
//   public class AOETargetingData : TargetingStrategy
//   {
//     [SerializeField]
//     private float _targetRadius;

//     [SerializeField]
//     private LayerMask _layerMask;

//     public override void StartTargeting(
//       PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
//       PlayerInputPresenter playerInputPresenter,
//       Joystick joystick,
//       AbilityModel abilityModel,
//       Action onTargetAcquired
//     )
//     {
//       abilityModel.AimRadius = AimRadius;

//       playerWatercraftAbilitiesPresenter.StartCoroutine(
//         Target(
//           playerWatercraftAbilitiesPresenter,
//           playerInputPresenter,
//           abilityModel,
//           onTargetAcquired
//         )
//       );
//     }

//     private IEnumerator Target(
//       PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
//       PlayerInputPresenter playerInputPresenter,
//       AbilityModel abilityModel,
//       Action onTargetAcquired
//     )
//     {
//       PlayerInputActions playerInputActions =
//         playerInputPresenter.GetPlayerInputActions();

//       playerInputActions.Watercraft.Choose.performed += ctx =>
//         HandlePlayerConfirmTargetPosition(
//           playerWatercraftAbilitiesPresenter,
//           playerInputPresenter,
//           abilityModel
//         );
//       playerInputActions.Watercraft.Cancel.performed += ctx =>
//         HandlePlayerCancelTargeting(
//           playerWatercraftAbilitiesPresenter,
//           playerInputPresenter,
//           abilityModel
//         );

//       playerWatercraftAbilitiesPresenter.ShowRangeIndicator();
//       playerWatercraftAbilitiesPresenter.ShowCircleIndicator();

//       playerWatercraftAbilitiesPresenter.SetRangeIndicatorSize(
//         new Vector2(AimRadius * 2f, AimRadius * 2f)
//       );

//       playerWatercraftAbilitiesPresenter.SetCircleIndicatorSize(
//         new Vector2(_targetRadius * 2f, _targetRadius * 2f)
//       );

//       while (!abilityModel.IsPerformed && !abilityModel.IsCancelled)
//       {
//         Vector3 mousePosition = new Vector3(
//           Mouse.current.position.ReadValue().x,
//           Mouse.current.position.ReadValue().y,
//           0
//         );
//         if (Application.isMobilePlatform)
//         {
//           // TODO: Implement mobile targeting
//         }
//         else
//         {
//           FindTargetPositionPC(
//             playerWatercraftAbilitiesPresenter,
//             abilityModel,
//             mousePosition
//           );
//         }

//         playerWatercraftAbilitiesPresenter.SetCircleIndicatorPosition(
//           abilityModel.TargetPosition
//         );

//         if (abilityModel.IsPerformed)
//         {
//           break;
//         }

//         yield return null;
//       }

//       if (abilityModel.IsPerformed)
//       {
//         onTargetAcquired?.Invoke();
//       }
//     }

//     private void FindTargetPositionPC(
//       PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
//       AbilityModel abilityModel,
//       Vector3 mousePosition
//     )
//     {
//       if (
//         Physics.Raycast(
//           Camera.main.ScreenPointToRay(mousePosition),
//           out RaycastHit raycastHit,
//           Mathf.Infinity,
//           _layerMask
//         )
//       )
//       {
//         if (
//           Vector3.Distance(
//             raycastHit.point,
//             playerWatercraftAbilitiesPresenter.Origin
//           ) > AimRadius
//         )
//         {
//           abilityModel.TargetPosition =
//             playerWatercraftAbilitiesPresenter.Origin
//             + (
//               (
//                 raycastHit.point - playerWatercraftAbilitiesPresenter.Origin
//               ).normalized * AimRadius
//             );
//         }
//         else
//         {
//           abilityModel.TargetPosition = raycastHit.point;
//         }
//       }
//     }
//   }
// }
