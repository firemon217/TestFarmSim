using KinematicCharacterController;
using UnityEngine;


namespace PlayerController
{
    public class MoveModule
    {
        private KinematicCharacterMotor _motor;
        private Vector3 _moveInputVector;

        [Header("Stable Movement")]
        private float _maxStableMoveSpeed = 10f;
        private float _sprintMutlySpeed = 1.5f;
        private float _stableMovementSharpness = 15;

        [Header("Air Movement")]
        private float _maxAirMoveSpeed = 10f;
        private float _airAccelerationSpeed = 5f;
        private float _drag = 0.1f;

        [Header("Misc")]
        private Vector3 _gravity = new Vector3(0, -30f, 0);

        private bool _sprint = false;

        public MoveModule(ref KinematicCharacterMotor motor, float maxStableMoveSpeed, float stableMovementSharpness, float maxAirMoveSpeed, float airAccelerationSpeed, float drag, float sprintMultySpeed) 
        { 
            _motor = motor;
            _maxStableMoveSpeed = maxStableMoveSpeed;
            _stableMovementSharpness = stableMovementSharpness;
            _airAccelerationSpeed = airAccelerationSpeed;
            _drag = drag;
            _maxAirMoveSpeed = maxAirMoveSpeed;
            _sprintMutlySpeed = sprintMultySpeed;
        }

        public void InputMove(float moveAxisForward, float moveAxisRight, bool sprint, Quaternion cameraPlanarRotation)
        {
            Vector3 moveInputVector = Vector3.ClampMagnitude(new Vector3(moveAxisRight, 0f, moveAxisForward), 1f);
            _moveInputVector = cameraPlanarRotation * moveInputVector;
            _sprint = sprint;
        }

        public void HandleMoveing(ref Vector3 currentVelocity, float deltaTime)
        {
            Vector3 targetMovementVelocity = Vector3.zero;
            if (_motor.GroundingStatus.IsStableOnGround)
            {
                // Reorient velocity on slope
                currentVelocity = _motor.GetDirectionTangentToSurface(currentVelocity, _motor.GroundingStatus.GroundNormal) * currentVelocity.magnitude;

                // Calculate target velocity
                Vector3 inputRight = Vector3.Cross(_moveInputVector, _motor.CharacterUp);
                Vector3 reorientedInput = Vector3.Cross(_motor.GroundingStatus.GroundNormal, inputRight).normalized * _moveInputVector.magnitude;
                targetMovementVelocity = reorientedInput * (_sprint ? _maxStableMoveSpeed * _sprintMutlySpeed : _maxStableMoveSpeed);

                // Smooth movement Velocity
                currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity, 1 - Mathf.Exp(-_stableMovementSharpness * deltaTime));
            }
            else
            {
                // Add move input
                if (_moveInputVector.sqrMagnitude > 0f)
                {
                    targetMovementVelocity = _moveInputVector * _maxAirMoveSpeed;

                    // Prevent climbing on un-stable slopes with air movement
                    if (_motor.GroundingStatus.FoundAnyGround)
                    {
                        Vector3 perpenticularObstructionNormal = Vector3.Cross(Vector3.Cross(_motor.CharacterUp, _motor.GroundingStatus.GroundNormal), _motor.CharacterUp).normalized;
                        targetMovementVelocity = Vector3.ProjectOnPlane(targetMovementVelocity, perpenticularObstructionNormal);
                    }

                    Vector3 velocityDiff = Vector3.ProjectOnPlane(targetMovementVelocity - currentVelocity, _gravity);
                    currentVelocity += velocityDiff * _airAccelerationSpeed * deltaTime;
                }

                // Gravity
                currentVelocity += _gravity * deltaTime;

                // Drag
                currentVelocity *= (1f / (1f + (_drag * deltaTime)));
            }
        }
    }

}