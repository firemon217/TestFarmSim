using KinematicCharacterController;
using UnityEngine;

namespace PlayerController
{
    public class CrouchingModule
    {
        private bool _shouldBeCrouching = false;
        private bool _isCrouching = false;
        private Collider[] _probedColliders;

        private KinematicCharacterMotor _motor;

        public CrouchingModule(ref KinematicCharacterMotor motor, ref Collider[] probedColliders) 
        {
            _motor = motor;
            _probedColliders = probedColliders;
        }

        public void InputCrouching(bool crouchDown, bool crouchUp)
        {
            if (crouchDown)
            {
                _shouldBeCrouching = true;
            }
            else if (crouchUp)
            {
                _shouldBeCrouching = false;
            }
        }

        public void HandleCrouching()
        {
            if (!_isCrouching && _shouldBeCrouching)
            {
                _isCrouching = true;
                _motor.SetCapsuleDimensions(0.5f, 1f, 0.5f);
            }
            if (_isCrouching && !_shouldBeCrouching)
            {
                // Do an overlap test with the character's standing height to see if there are any obstructions
                _motor.SetCapsuleDimensions(0.5f, 2f, 1f);
                if (_motor.CharacterCollisionsOverlap(
                        _motor.TransientPosition,
                        _motor.TransientRotation,
                        _probedColliders) > 0)
                {
                    // If obstructions, just stick to crouching dimensions
                    _motor.SetCapsuleDimensions(0.5f, 1f, 0.5f);
                }
                else
                {
                    _isCrouching = false;
                }
            }
        }
    }
}
