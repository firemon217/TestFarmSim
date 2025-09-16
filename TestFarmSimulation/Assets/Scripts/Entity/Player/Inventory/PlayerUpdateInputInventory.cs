using System;
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

            public event Action<bool> OnToggleInventory;

            public PlayerUpdateInputInventory(PlayerInventory inventory)
            {
                _gameInput = new GameInput();
                _gameInput.Enable();

                _inventory = inventory;
            }

            public void Enable()
            {
                _gameInput.GamePlay.Inventory.started += InventoryOpen;
                _gameInput.GamePlay.Inventory.canceled += InventoryClose;
            }

            public void Disable()
            {
                _gameInput.GamePlay.Inventory.started -= InventoryOpen;
                _gameInput.GamePlay.Inventory.canceled += InventoryClose;
            }

            private void InventoryOpen(InputAction.CallbackContext obj) => OnToggleInventory?.Invoke(true);
            private void InventoryClose(InputAction.CallbackContext obj) => OnToggleInventory?.Invoke(false);
        }
    }
}