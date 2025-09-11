using Controller;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    namespace Controller
    {
        public class PlayerUpdateInputController
        {
            private GameObject _camera;
            private PlayerController _playerInput;
            private AInputController _characterInput;

            private GameInput _gameInput;

            public PlayerUpdateInputController(PlayerController controller, AInputController input, GameObject camera)
            {
                _gameInput = new GameInput();
                _gameInput.Enable();
                _playerInput = controller;
                _characterInput = input;
                _camera = camera;
            }

            public void Update()
            {
                ReadMovement();
                _playerInput.SetInputs();
            }

            public void Enable()
            {
                _gameInput.GamePlay.Jump.started += JumpStarted;
                _gameInput.GamePlay.Jump.canceled += JumpCanceled;
                _gameInput.GamePlay.Crouch.performed += CrouchPerfomed;
                _gameInput.GamePlay.Crouch.canceled += CrouchCanceled;
                _gameInput.GamePlay.Sprint.performed += SprintPerfomed;
                _gameInput.GamePlay.Sprint.canceled += SprintCanceled;
            }

            public void Disable()
            {
                _gameInput.GamePlay.Jump.started -= JumpStarted;
                _gameInput.GamePlay.Jump.canceled -= JumpCanceled;
                _gameInput.GamePlay.Crouch.performed -= CrouchPerfomed;
                _gameInput.GamePlay.Crouch.canceled -= CrouchCanceled;
                _gameInput.GamePlay.Sprint.performed -= SprintPerfomed;
                _gameInput.GamePlay.Sprint.canceled -= SprintCanceled;
            }

            private void JumpStarted(InputAction.CallbackContext obj) => _characterInput.JumpDown = true;
            private void JumpCanceled(InputAction.CallbackContext obj) => _characterInput.JumpDown = false;
            private void CrouchPerfomed(InputAction.CallbackContext obj) { _characterInput.CrouchDown = true; _characterInput.CrouchUp = false; }
            private void CrouchCanceled(InputAction.CallbackContext obj) { _characterInput.CrouchDown = false; _characterInput.CrouchUp = true; }
            private void SprintPerfomed(InputAction.CallbackContext obj) => _characterInput.Sprint = true;
            private void SprintCanceled(InputAction.CallbackContext obj) => _characterInput.Sprint = false;

            private void ReadMovement()
            {
                Vector2 inputDirection = _gameInput.GamePlay.Movement.ReadValue<Vector2>();

                _characterInput.MoveAxisForward = inputDirection.y;
                _characterInput.MoveAxisRight = inputDirection.x;

                // Используем сохранённый поворот
                _characterInput.CameraRotation = _camera.transform.rotation;
            }

        }
    }
}