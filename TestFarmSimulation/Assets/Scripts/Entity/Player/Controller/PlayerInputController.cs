using Controller;
using UnityEngine;

namespace Player
{
    namespace Controller
    {
        public class PlayerInputController : AInputController
        {
            public override float MoveAxisForward { get; set; }
            public override float MoveAxisRight { get; set; }
            public override Quaternion CameraRotation { get; set; }
            public override bool JumpDown { get; set; }
            public override bool CrouchDown { get; set; }
            public override bool CrouchUp { get; set; }
            public override bool Sprint { get; set; }
        }
    }
}