using KinematicCharacterController;
using KinematicCharacterController.Examples;
using UnityEngine;

namespace PlayerController
{
    public class PlayerCharacterInputs
    {
        public float MoveAxisForward;
        public float MoveAxisRight;
        public Quaternion CameraRotation;
        public bool JumpDown;
        public bool CrouchDown;
        public bool CrouchUp;
        public bool Sprint;
    }

    public class PlayerController : MonoBehaviour, ICharacterController
    {
        [SerializeField] private KinematicCharacterMotor Motor;

        [Header("Stable Movement")]
        [SerializeField] private float MaxStableMoveSpeed = 10f;
        [SerializeField] private float SpeedMultySprint = 1.5f;
        [SerializeField] private float StableMovementSharpness = 15;
        [SerializeField] private float OrientationSharpness = 10;

        [Header("Air Movement")]
        public float MaxAirMoveSpeed = 10f;
        public float AirAccelerationSpeed = 5f;
        public float Drag = 0.1f;

        [Header("Jumping")]
        public float JumpSpeed = 10f;

        [Header("Misc")]
        public Vector3 Gravity = new Vector3(0, -30f, 0);
        public Transform MeshRoot;

        private Collider[] _probedColliders = new Collider[8];

        // Controlls modules
        public MoveModule _move;
        public JumpingModule _jumping;
        public CrouchingModule _crouching;
        public RotateModule _rotate;
        public InputCharacterManager _inputManager;

        [SerializeField] private GameObject _camera;
        private PlayerCharacterInputs _characterInputs;

        public PlayerCharacterInputs CharacterInputs => _characterInputs;

        void Awake()
        {
            Motor.CharacterController = this;

            _characterInputs = new PlayerCharacterInputs();

            _move = new MoveModule(ref Motor, MaxStableMoveSpeed, StableMovementSharpness, MaxAirMoveSpeed, AirAccelerationSpeed, Drag, SpeedMultySprint);
            _jumping = new JumpingModule(ref Motor, JumpSpeed);
            _crouching = new CrouchingModule(ref Motor, ref _probedColliders);
            _rotate = new RotateModule(ref Motor, OrientationSharpness);
            _inputManager = new InputCharacterManager(_camera, this, ref _characterInputs);
        }

        private void OnEnable()
        {
            _inputManager.Enable();
        }

        private void OnDisable()
        {
            _inputManager.Disable();
        }

        private void Update()
        {
            _inputManager.Update();
        }

        public void SetInputs()
        {
            Debug.Log(_characterInputs.MoveAxisForward);
            // Move and look inputs
            _rotate.InputRotate(_characterInputs.CameraRotation);
            _move.InputMove(_characterInputs.MoveAxisForward, _characterInputs.MoveAxisRight, _characterInputs.Sprint, _rotate.CameraPlanarRotation);

            // Jumping input
            _jumping.InputJump(_characterInputs.JumpDown);

            // Crouching input
            _crouching.InputCrouching(_characterInputs.CrouchDown, _characterInputs.CrouchUp);
        }

        public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        {
            _rotate.HandlRotate(ref currentRotation, deltaTime);
        }

        public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        {   
            // handle moveing
            _move.HandleMoveing(ref currentVelocity, deltaTime);
            // Handle jumping
            _jumping.HandleJumping(ref currentVelocity, deltaTime);
            // Handle crouching
            _crouching.HandleCrouching();
        }

        public void AfterCharacterUpdate(float deltaTime)
        {
            // Handle jump-related values
            _jumping.AfterUpdateJumping(deltaTime);
        }

        public void BeforeCharacterUpdate(float deltaTime)
        {
            //throw new System.NotImplementedException();
        }

        public void OnDiscreteCollisionDetected(Collider hitCollider)
        {
            //throw new System.NotImplementedException();
        }

        public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        {
            //throw new System.NotImplementedException();
        }

        public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        {
            //throw new System.NotImplementedException();
        }

        public void PostGroundingUpdate(float deltaTime)
        {
            //throw new System.NotImplementedException();
        }

        public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
        {
            //throw new System.NotImplementedException();
        }

        public bool IsColliderValidForCollisions(Collider coll)
        {
            return true;
        }

    }

}