using UnityEngine;
using Items;

namespace Player
{
    namespace Interaction
    {
        // Module Player interaction
        public class PlayerInteraction : MonoBehaviour
        {
            [Header("Модули игрока")]
            [SerializeField] private PlayerInteractionRay _playerInteractionRay;
            [SerializeField] private PlayerActiveHand _activeHand;
            [SerializeField] public Player Player;

            // Input interaction controller
            private PlayerUpdateInputInteraction _playerUpdateInputInteraction;

            // Current detetcted interactable object 
            private IInteractable _currentInteractable;

            private bool _onInteract = false;

            private void Awake()
            {
                // Instance Input interaction controller 
                _playerUpdateInputInteraction = new PlayerUpdateInputInteraction();
                // Lock cursor
                Cursor.lockState = CursorLockMode.Locked;
            }

            // Set currentInteractable when interactable object detected
            void Interact(IInteractable interactable)
            {
                _currentInteractable = interactable;
            }

            // On event and interaction controller
            private void OnEnable()
            {
                _playerUpdateInputInteraction.Enable();
                _playerUpdateInputInteraction.onInteract += HandleInteractInput;
                _playerUpdateInputInteraction.onThrow += ThorwItem;
                _playerInteractionRay.onItemDetected += Interact;
            }

            // Off event and interaction controller
            private void OnDisable()
            {
                _playerUpdateInputInteraction.Disable();
                _playerUpdateInputInteraction.onInteract -= HandleInteractInput;
                _playerUpdateInputInteraction.onThrow -= ThorwItem;
                _playerInteractionRay.onItemDetected -= Interact;
            }

            // Interact with current interactable object
            private void HandleInteractInput()
            {
                Debug.Log("Try intecart " +  _currentInteractable);
                if (_currentInteractable != null)
                {
                    _currentInteractable.Interact(this);
                }
            }

            // Throw active hand`s item
            private void ThorwItem()
            {
                _activeHand.ThrowItem();
                _currentInteractable = null;
            }

            // Pick up interactable object
            public void PickUpItem(ItemObject item)
            {
                _activeHand.PickUpItem(item);
            }


        }
    }
}