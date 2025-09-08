using KinematicCharacterController;
using UnityEngine;

namespace PlayerController
{
    public class RotateModule
    {
        private Vector3 _lookInputVector;
        private KinematicCharacterMotor _motor;
        private float _orientationSharpness;
        private Quaternion _cameraPlanarRotation;

        public Quaternion CameraPlanarRotation => _cameraPlanarRotation;

        public RotateModule(ref KinematicCharacterMotor motor, float orientationSharpness) 
        { 
            _motor = motor;
            _orientationSharpness = orientationSharpness;
        }

        public void InputRotate(Quaternion cameraRotation)
        {
            // Calculate camera direction and rotation on the character plane
            Vector3 cameraPlanarDirection = Vector3.ProjectOnPlane(cameraRotation * Vector3.forward, _motor.CharacterUp).normalized;
            if (cameraPlanarDirection.sqrMagnitude == 0f)
            {
                cameraPlanarDirection = Vector3.ProjectOnPlane(cameraRotation * Vector3.up, _motor.CharacterUp).normalized;
            }
            _cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, _motor.CharacterUp);
            _lookInputVector = cameraPlanarDirection;
        }

        public void HandlRotate(ref Quaternion currentRotation, float deltaTime)
        {
            if (_lookInputVector != Vector3.zero && _orientationSharpness > 0f)
            {
                // Smoothly interpolate from current to target look direction
                Vector3 smoothedLookInputDirection = Vector3.Slerp(_motor.CharacterForward, _lookInputVector, 1 - Mathf.Exp(-_orientationSharpness * deltaTime)).normalized;

                // Set the current rotation (which will be used by the KinematicCharacterMotor)
                currentRotation = Quaternion.LookRotation(smoothedLookInputDirection, _motor.CharacterUp);
            }
        }

    }

}