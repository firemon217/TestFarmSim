using UnityEngine;
using UnityEngine.InputSystem;


namespace Player
{
    namespace Inventory
    {
        public class PlayerUpdateInputInventory
        {
            private GameInput _gameInput;
            private PlayerInventory _inventory;
            private PlayerInputInventory _playerInputInventory;

            public PlayerUpdateInputInventory(PlayerInventory inventory, PlayerInputInventory playerInputInventory)
            {
                _gameInput = new GameInput();
                _gameInput.Enable();

                _inventory = inventory;
                _playerInputInventory = playerInputInventory;
            }

            public void Update()
            {
                _inventory.SetInputs();
            }

            public void Enable()
            {
                _gameInput.GamePlay.Inventory.started += InventoryToggle;
            }

            public void Disable()
            {
                _gameInput.GamePlay.Inventory.started -= InventoryToggle;
            }

            private void InventoryToggle(InputAction.CallbackContext obj) => _playerInputInventory.OnInventoryOpen = !_playerInputInventory.OnInventoryOpen;
        }
    }
}