using UnityEngine;


namespace PlayerController
{
    public abstract class PlayerControllerState : State<PlayerController>
    {
        public virtual void UpdateVelocity(PlayerController player, ref Vector3 currentVelocity, float deltaTime) { }
        public virtual void UpdateRotation(PlayerController player, ref Quaternion currentRotation, float deltaTime) { }
        public virtual void AfterCharacterUpdate(PlayerController player, float deltaTime) { }
    }

}