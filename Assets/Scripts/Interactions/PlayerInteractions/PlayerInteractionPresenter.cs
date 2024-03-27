using LNE.Inputs;
using UnityEngine;
using Zenject;

namespace LNE.Interactions
{
  public class PlayerInteractionPresenter : MonoBehaviour
  {
    [SerializeField]
    private float _interactDistance = 2f;

    private PlayerInteractionView _view;
    private PlayerInteractionModel _model = new PlayerInteractionModel();

    private void Awake()
    {
      _view = GetComponent<PlayerInteractionView>();
    }

    private void Update()
    {
      FindTarget();
    }

    private void FindTarget()
    {
      Collider[] colliders = Physics.OverlapSphere(
        transform.position,
        _interactDistance
      );

      Interactable closestTarget = GetClosestTarget(colliders);

      if (_model.CurrentTarget == closestTarget)
      {
        return;
      }

      _model.CurrentTarget = closestTarget;

      if (_model.CurrentTarget != null)
      {
        _view.ShowInteractButton();
      }
      else
      {
        _view.HideInteractButton();
      }
    }

    private Interactable GetClosestTarget(Collider[] colliders)
    {
      float closestDistance = _interactDistance;
      Interactable target = null;

      foreach (var collider in colliders)
      {
        Interactable interactableObject = collider.GetComponent<Interactable>();

        if (interactableObject == null)
          continue;

        float distanceToTarget = Vector3.Distance(
          transform.position,
          interactableObject.transform.position
        );

        if (distanceToTarget < closestDistance)
        {
          target = interactableObject;
          closestDistance = distanceToTarget;
        }
      }

      return target;
    }

    public bool InteractWithCurrentTarget()
    {
      return Interact(_model.CurrentTarget);
    }

    private bool Interact(Interactable target)
    {
      if (target == null || !IsInRange(target))
      {
        return false;
      }

      target.Interact(gameObject);
      return true;
    }

    private bool IsInRange(Interactable target)
    {
      return Vector3.Distance(target.transform.position, transform.position)
        < _interactDistance;
    }
  }
}
