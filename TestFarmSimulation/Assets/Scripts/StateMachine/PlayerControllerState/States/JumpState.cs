using PlayerController;
using UnityEngine;

namespace PlayerController
{
    public class JumpState : IdleState
    {
        public override State<PlayerController> HandleInput(PlayerController owner)
        {
            if (!owner.CharacterInputs.JumpDown)
            {
                return new MoveState();
            }
            return this;
        }
    }
}