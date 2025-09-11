using UnityEngine;

namespace Player
{
    namespace Inventory
    {
        public class PlayerInventory : MonoBehaviour
        {
            private PlayerInputInventory _playerInputInventory;
            private PlayerUpdateInputInventory _playerUpdateInputInventory;

            [SerializeField] private CircleInventory _circleInventory;
            [SerializeField] private PlayerActiveHand _activeHand;

            private void Awake()
            {
                _playerInputInventory = new PlayerInputInventory();
                _playerUpdateInputInventory = new PlayerUpdateInputInventory(this, _playerInputInventory);
                _circleInventory.onChangeInstrument += _activeHand.ChangeInstrument;
            }

            public void SetInputs()
            {
                ToggleInventory(_playerInputInventory.OnInventoryOpen);
            }

            private void Update()
            {
                _playerUpdateInputInventory.Update();
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
