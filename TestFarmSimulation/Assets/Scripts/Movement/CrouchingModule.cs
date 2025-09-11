using KinematicCharacterController;
using UnityEngine;

namespace Controller
{
    public class CrouchingModule : IMovementModule
    {
        private bool _shouldBeCrouching = false;
        private bool _isCrouching = false;

        private IPhysicsController _physicsController;
        private CrouchConfig _crouchConfig;
        private AInputController _inputController;

        public CrouchingModule(CrouchConfig crouchConfig, IPhysicsController physicsController, AInputController inputController) 
        {
            _physicsController = physicsController;
            _crouchConfig = crouchConfig;
            _inputController = inputController;
        }

        public void HandleInput()
        {
            if (_inputController.CrouchDown)
            {
                _shouldBeCrouching = true;
            }
            else if (_inputController.CrouchUp)
            {
                _shouldBeCrouching = false;
            }
        }

        public void HandleUpdate()
        {
            if (!_isCrouching && _shouldBeCrouching)
            {
                _isCrouching = true;
                _physicsController.Motor.SetCapsuleDimensions(0.5f, 1f, 0.5f);
            }
            if (_isCrouching && !_shouldBeCrouching)
            {
                // Do an overlap test with the character's standing height to see if there are any obstructions
                _physicsController.Motor.SetCapsuleDimensions(0.5f, 2f, 1f);
                if (_physicsController.Motor.CharacterCollisionsOverlap(
                        _physicsController.Motor.TransientPosition,
                        _physicsController.Motor.TransientRotation,
                        _physicsController.ProbedColliders) > 0)
                {
                    // If obstructions, just stick to crouching dimensions
                    _physicsController.Motor.SetCapsuleDimensions(0.5f, 1f, 0.5f);
                }
                else
                {
                    _isCrouching = false;
                }
            }
        }
    }
}
