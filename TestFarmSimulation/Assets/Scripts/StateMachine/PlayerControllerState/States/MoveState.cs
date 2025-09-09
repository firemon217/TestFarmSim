using PlayerController;
using UnityEngine;

namespace PlayerController
{
    public class MoveState : IdleState
    {
        Vector3 _currentVelocity;
        public override State<PlayerController> HandleInput(PlayerController owner)
        {
            //if (owner.CharacterInputs.JumpDown)
            //{
            //    return new JumpState();
            //}
            if (owner.CharacterInputs.MoveAxisForward == 0 && owner.CharacterInputs.MoveAxisRight == 0)
            {
                return new IdleState();
            }
            return this;
        }
        public override void Update(PlayerController owner)
        {
            UpdateVelocity(owner, ref _currentVelocity, Time.deltaTime);
        }
        public override void Enter(PlayerController owner) { }
        public override void Exit(PlayerController owner) { }

        public override void UpdateVelocity(PlayerController player, ref Vector3 currentVelocity, float deltaTime)
        {
            //player._move.HandleMoveing(ref currentVelocity, deltaTime);
        }
    }
}