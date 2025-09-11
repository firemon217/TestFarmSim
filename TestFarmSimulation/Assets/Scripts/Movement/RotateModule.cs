using KinematicCharacterController;
using UnityEngine;

namespace Controller
{
    public class RotateModule : IMovementModule
    {
        private Vector3 _lookInputVector;
        private RotateConfig _rotateConfig;
        private IPhysicsController _physicsController;
        private AInputController _inputController;
        

        public Quaternion CameraPlanarRotation;

        public RotateModule(RotateConfig rotateConfig, IPhysicsController physicsController, AInputController inputController) 
        {
            _inputController = inputController;
            _rotateConfig = rotateConfig;
            _physicsController = physicsController;
        }

        public void HandleInput()
        {
            // Calculate camera direction and rotation on the character plane
            Vector3 cameraPlanarDirection = Vector3.ProjectOnPlane(_inputController.CameraRotation * Vector3.forward, _physicsController.Motor.CharacterUp).normalized;
            if (cameraPlanarDirection.sqrMagnitude == 0f)
            {
                cameraPlanarDirection = Vector3.ProjectOnPlane(_inputController.CameraRotation * Vector3.up, _physicsController.Motor.CharacterUp).normalized;
            }
            _physicsController.CameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, _physicsController.Motor.CharacterUp);
            _lookInputVector = cameraPlanarDirection;
        }

        public void HandleUpdate()
        {
            if (_lookInputVector != Vector3.zero && _rotateConfig.OrientationSharpness > 0f)
            {
                // Smoothly interpolate from current to target look direction
                Vector3 smoothedLookInputDirection = Vector3.Slerp(_physicsController.Motor.CharacterForward, _lookInputVector, 1 - Mathf.Exp(-_rotateConfig.OrientationSharpness * _physicsController.DeltaTime)).normalized;

                // Set the current rotation (which will be used by the KinematicCharacterMotor)
                _physicsController.CurrentRotation = Quaternion.LookRotation(smoothedLookInputDirection, _physicsController.Motor.CharacterUp);
            }
        }

    }

}