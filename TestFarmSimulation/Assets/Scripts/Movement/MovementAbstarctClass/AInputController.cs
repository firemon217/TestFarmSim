using UnityEngine;

namespace Controller
{
    public abstract class AInputController
    {
        public virtual float MoveAxisForward { get; set; }
        public virtual float MoveAxisRight { get; set; }
        public virtual Quaternion CameraRotation { get; set; }
        public virtual bool JumpDown { get; set; }
        public virtual bool CrouchDown { get; set; }
        public virtual bool CrouchUp { get; set; }
        public virtual bool Sprint { get; set; }
    }
}