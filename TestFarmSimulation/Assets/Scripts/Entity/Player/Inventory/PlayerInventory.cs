using UnityEngine;
using Items;

namespace Player
{
    namespace Inventory
    {
        public class PlayerInventory : MonoBehaviour
        {
            private PlayerUpdateInputInventory _playerUpdateInputInventory;

            [SerializeField] private CircleInventory _circleInventory;
            [SerializeField] private PlayerActiveHand _activeHand;

            private void Awake()
            {
                _playerUpdateInputInventory = new PlayerUpdateInputInventory(this);
                _circleInventory.onChangeInstrument += _activeHand.PickUpItem;
                _playerUpdateInputInventory.OnToggleInventory += ToggleInventory;
            }

            private void OnEnable()
            {
                _playerUpdateInputInventory.Enable();
            }

            private void OnDisable()
            {
                _playerUpdateInputInventory.Disable();
            }

            private void ToggleInventory(bool isOpen)
            {
                _circleInventory.gameObject.SetActive(isOpen);
            }
        }
    }
}
