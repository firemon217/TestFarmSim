using UnityEngine;


namespace Controller
{
    [System.Serializable]
    public class MoveConfig : MonoBehaviour
    {
        [Header("Stable Movement")]
        public float MaxStableMoveSpeed = 10f;
        public float SprintMutlySpeed = 1.5f;
        public float StableMovementSharpness = 15;

        [Header("Air Movement")]
        public float MaxAirMoveSpeed = 10f;
        public float AirAccelerationSpeed = 5f;
        public float Drag = 0.1f;

        [Header("Misc")]
        public Vector3 Gravity = new Vector3(0, -30f, 0);
    }
}