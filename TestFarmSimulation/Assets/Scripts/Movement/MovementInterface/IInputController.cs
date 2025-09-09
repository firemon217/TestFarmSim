using UnityEngine;

namespace Controller
{
    public interface IInputController
    {
        public float MoveAxisForward { get; set; }
        public float MoveAxisRight { get; set; }
        public Quaternion CameraRotation { get; set; }
        public bool JumpDown { get; set; }
        public bool CrouchDown { get; set; }
        public bool CrouchUp { get; set; }
        public bool Sprint { get; set; }
    }
}