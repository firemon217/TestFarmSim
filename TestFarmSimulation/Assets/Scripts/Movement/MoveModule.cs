using KinematicCharacterController;
using UnityEngine;


namespace Controller
{
    public class MoveModule
    {
        private MoveConfig _moveConfig;
        private IPhysicsController _physicsController;
        private IInputController _inputController;

        private bool _sprint = false;
        private Vector3 _moveInputVector;


        public MoveModule(MoveConfig moveConf, IPhysicsController physicsController, IInputController input) 
        {
            _moveConfig = moveConf;
            _physicsController = physicsController;
            _inputController = input;
        }

        public void InputMove()
        {
            Vector3 moveInputVector = Vector3.ClampMagnitude(new Vector3(_inputController.MoveAxisRight, 0f, _inputController.MoveAxisForward), 1f);
            _moveInputVector = _physicsController.CameraPlanarRotation * moveInputVector;
            _sprint = _inputController.Sprint;
        }

        public void HandleMoveing()
        {
            Vector3 targetMovementVelocity = Vector3.zero;
            if (_physicsController.Motor.GroundingStatus.IsStableOnGround)
            {
                // Reorient velocity on slope
                _physicsController.CurrentVelocity = _physicsController.Motor.GetDirectionTangentToSurface(_physicsController.CurrentVelocity, _physicsController.Motor.GroundingStatus.GroundNormal) *  _physicsController.CurrentVelocity.magnitude;

                // Calculate target velocity
                Vector3 inputRight = Vector3.Cross(_moveInputVector, _physicsController.Motor.CharacterUp);
                Vector3 reorientedInput = Vector3.Cross(_physicsController.Motor.GroundingStatus.GroundNormal, inputRight).normalized * _moveInputVector.magnitude;
                targetMovementVelocity = reorientedInput * (_sprint ? _moveConfig.MaxStableMoveSpeed * _moveConfig.SprintMutlySpeed : _moveConfig.MaxStableMoveSpeed);

                // Smooth movement Velocity
                _physicsController.CurrentVelocity = Vector3.Lerp(_physicsController.CurrentVelocity, targetMovementVelocity, 1 - Mathf.Exp(-_moveConfig.StableMovementSharpness * _physicsController.DeltaTime));
            }
            else
            {
                // Add move input
                if (_moveInputVector.sqrMagnitude > 0f)
                {
                    targetMovementVelocity = _moveInputVector * _moveConfig.MaxAirMoveSpeed;

                    // Prevent climbing on un-stable slopes with air movement
                    if (_physicsController.Motor.GroundingStatus.FoundAnyGround)
                    {
                        Vector3 perpenticularObstructionNormal = Vector3.Cross(Vector3.Cross(_physicsController.Motor.CharacterUp, _physicsController.Motor.GroundingStatus.GroundNormal), _physicsController.Motor.CharacterUp).normalized;
                        targetMovementVelocity = Vector3.ProjectOnPlane(targetMovementVelocity, perpenticularObstructionNormal);
                    }

                    Vector3 velocityDiff = Vector3.ProjectOnPlane(targetMovementVelocity - _physicsController.CurrentVelocity, _moveConfig.Gravity);
                    _physicsController.CurrentVelocity += velocityDiff * _moveConfig.AirAccelerationSpeed * _physicsController.DeltaTime;
                }

                // Gravity
                _physicsController.CurrentVelocity += _moveConfig.Gravity * _physicsController.DeltaTime;

                // Drag
                _physicsController.CurrentVelocity *= (1f / (1f + (_moveConfig.Drag * _physicsController.DeltaTime)));
            }
        }
    }

}