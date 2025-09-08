using UnityEngine;

namespace PlayerController
{
    public class IdleState : PlayerControllerState
    {
        public override State<PlayerController> HandleInput(PlayerController owner) {
            if (owner.CharacterInputs.MoveAxisForward != 0 || owner.CharacterInputs.MoveAxisRight != 0)
            {
                return new MoveState();
            }
            return this;
        }
        public override void Update(PlayerController owner) { }
        public override void Enter(PlayerController owner) { }
        public override void Exit(PlayerController owner) { }
    }
}