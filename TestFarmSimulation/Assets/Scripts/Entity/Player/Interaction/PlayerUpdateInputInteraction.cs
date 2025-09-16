using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Player
{
    namespace Interaction
    {
        // Module input interact controller
        public class PlayerUpdateInputInteraction
        {
            // Game Input Instance
            private GameInput _gameInput;

            // Event input
            public event Action onInteract;
            public event Action onThrow;

            public PlayerUpdateInputInteraction()
            {
                _gameInput = new GameInput();
                _gameInput.Enable();
            }

            public void Enable()
            {
                _gameInput.GamePlay.Interaction.started += InteractStart;
                _gameInput.GamePlay.Throw.started += ThrowStart;
            }

            public void Disable()
            {
                _gameInput.GamePlay.Interaction.started -= InteractStart;
                _gameInput.GamePlay.Throw.started -= ThrowStart;
            }

            private void InteractStart(InputAction.CallbackContext obj) { onInteract?.Invoke(); }

            private void ThrowStart(InputAction.CallbackContext obj) { onThrow?.Invoke(); }
        }
    }
}