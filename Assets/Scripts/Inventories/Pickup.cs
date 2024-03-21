// using LNE.Interactions;
// using UnityEngine;

// namespace LNE.Inventories
// {
//   [RequireComponent(typeof(InteractableObject))]
//   public class Pickup : MonoBehaviour
//   {
//     [SerializeField]
//     private InventoryItemData _inventoryItemData;

//     [SerializeField]
//     private int _quantity = 1;

//     private InteractableObject _interactableObject;

//     private void Awake()
//     {
//       _interactableObject = GetComponent<InteractableObject>();
//     }

//     private void OnEnable()
//     {
//       _interactableObject.OnInteracting.AddListener(OnInteract);
//     }

//     private void OnInteract(GameObject interactingPerson)
//     {
//       InventoryController interactingInventoryController =
//         interactingPerson.GetComponent<InventoryController>();

//       if (interactingInventoryController == null)
//       {
//         return;
//       }

//       PickUp(interactingPerson.GetComponent<InventoryController>());
//     }

//     private void PickUp(InventoryController interactingInventoryController)
//     {
//       interactingInventoryController.InsertItem(_inventoryItemData, _quantity);
//       Destroy(gameObject);
//     }
//   }
// }
