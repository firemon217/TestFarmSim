using KinematicCharacterController;
using UnityEngine;

namespace Controller
{
    public interface IPhysicsController
    {
        public KinematicCharacterMotor Motor { get; set; }
        public Quaternion CurrentRotation { get; set; }
        public Quaternion CameraPlanarRotation { get; set; }

        public Vector3 CurrentVelocity { get; set; }

        public float DeltaTime { get; set; }

        public Collider[] ProbedColliders { get; set; }
    }
}