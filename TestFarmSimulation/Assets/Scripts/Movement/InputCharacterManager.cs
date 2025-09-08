using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerController
{
    public class InputCharacterManager
    {
        private GameObject _camera;
        private PlayerController _character;
        private PlayerCharacterInputs _characterInputs;

        private GameInput _gameInput;

        public InputCharacterManager(GameObject camera, PlayerController character, ref PlayerCharacterInputs characterInputs)
        {
            _gameInput = new GameInput();
            _gameInput.Enable();
            _character = character;
            _characterInputs = characterInputs;
            _camera = camera;
        }

        public void Update()
        {
            Cursor.lockState = CursorLockMode.Locked;
            ReadMovement();
            _character.SetInputs();
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

        private void JumpStarted(InputAction.CallbackContext obj) => _characterInputs.JumpDown = true;
        private void JumpCanceled(InputAction.CallbackContext obj) => _characterInputs.JumpDown = false;
        private void CrouchPerfomed(InputAction.CallbackContext obj) { _characterInputs.CrouchDown = true; _characterInputs.CrouchUp = false; }
        private void CrouchCanceled(InputAction.CallbackContext obj) { _characterInputs.CrouchDown = false; _characterInputs.CrouchUp = true; }
        private void SprintPerfomed(InputAction.CallbackContext obj) => _characterInputs.Sprint = true;
        private void SprintCanceled(InputAction.CallbackContext obj) => _characterInputs.Sprint = false;

        private void ReadMovement()
        {
            Vector2 inputDirection = _gameInput.GamePlay.Movement.ReadValue<Vector2>();

            _characterInputs.MoveAxisForward = inputDirection.y;
            _characterInputs.MoveAxisRight = inputDirection.x;

            // Используем сохранённый поворот
            _characterInputs.CameraRotation = _camera.transform.rotation;
        }

    }


}