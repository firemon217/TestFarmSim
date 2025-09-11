using KinematicCharacterController;
using UnityEngine;

namespace Controller
{
    public class JumpingModule : IMovementModule
    {

        private bool _jumpRequested = false;
        private bool _jumpConsumed = false;
        private bool _jumpedThisFrame = false;
        private float _timeSinceJumpRequested = Mathf.Infinity;
        private float _timeSinceLastAbleToJump = 0f;
        private float _jumpPreGroundingGraceTime = 0f;
        private float _jumpPostGroundingGraceTime = 0f;

        private IPhysicsController _physicsController;
        private JumpConfig _jumpConfig;
        private AInputController _inputController;


        public JumpingModule(JumpConfig jumpConfig, IPhysicsController physicsController, AInputController inputController)
        {
            _physicsController = physicsController;
            _jumpConfig = jumpConfig;
            _inputController = inputController;
        }

        public void HandleInput()
        {
            if (_inputController.JumpDown)
            {
                _timeSinceJumpRequested = 0f;
                _jumpRequested = true;
            }
        }

        public void HandleUpdate()
        {
            _jumpedThisFrame = false;
            _timeSinceJumpRequested += _physicsController.DeltaTime;
            if (_jumpRequested)
            {
                // See if we actually are allowed to jump
                if (!_jumpConsumed && (_physicsController.Motor.GroundingStatus.IsStableOnGround) || _timeSinceLastAbleToJump <= _jumpPostGroundingGraceTime)
                {
                    // Calculate jump direction before ungrounding
                    Vector3 jumpDirection = _physicsController.Motor.CharacterUp;
                    if (_physicsController.Motor.GroundingStatus.FoundAnyGround && !_physicsController.Motor.GroundingStatus.IsStableOnGround)
                    {
                        jumpDirection = _physicsController.Motor.GroundingStatus.GroundNormal;
                    }

                    // Makes the character skip ground probing/snapping on its next update. 
                    // If this line weren't here, the character would remain snapped to the ground when trying to jump. Try commenting this line out and see.
                    _physicsController.Motor.ForceUnground(0.1f);

                    // Add to the return velocity and reset jump state
                    _physicsController.CurrentVelocity += (jumpDirection * _jumpConfig.JumpSpeed) - Vector3.Project(_physicsController.CurrentVelocity, _physicsController.Motor.CharacterUp);
                    _jumpRequested = false;
                    _jumpConsumed = true;
                    _jumpedThisFrame = true;
                }
            }
        }

        public void AfterUpdateJumping()
        {
            // Handle jumping pre-ground grace period
            if (_jumpRequested && _timeSinceJumpRequested > _jumpPreGroundingGraceTime)
            {
                _jumpRequested = false;
            }

            // Handle jumping while sliding
            if (_physicsController.Motor.GroundingStatus.IsStableOnGround)
            {
                // If we're on a ground surface, reset jumping values
                if (!_jumpedThisFrame)
                {
                    _jumpConsumed = false;
                }
                _timeSinceLastAbleToJump = 0f;
            }
            else
            {
                // Keep track of time since we were last able to jump (for grace period)
                _timeSinceLastAbleToJump += _physicsController.DeltaTime;
            }
        }

    }
}