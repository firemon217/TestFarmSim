using Controller;
using KinematicCharacterController;
using UnityEngine;


namespace PlayerController
{
    public class PlayerCharacterInputs : IInputController
    {
        public float MoveAxisForward { get; set; }
        public float MoveAxisRight { get; set; }
        public Quaternion CameraRotation { get; set; }
        public bool JumpDown { get; set; }
        public bool CrouchDown { get; set; }
        public bool CrouchUp { get; set; }
        public bool Sprint { get; set; }
    }

    public class PlayerController : MonoBehaviour, ICharacterController, IEntityController, IPhysicsController
    {
        // Main Kinematic Controller Core
        [SerializeField] private KinematicCharacterMotor _motor;
        public KinematicCharacterMotor Motor { get => _motor; set => _motor = value; }

        // Controller configs
        [SerializeField] public MoveConfig MoveConfig;
        [SerializeField] public RotateConfig RotateConfig;
        [SerializeField] public JumpConfig JumpConfig;
        [SerializeField] public CrouchConfig CrouchConfig;

        // Collision for crouching ???
        [SerializeField] private Collider[] _probedColliders = new Collider[8];
        public Collider[] ProbedColliders { get => _probedColliders; set => _probedColliders = value; }

        // Controlls modules
        public MoveModule _move;
        public JumpingModule _jumping;
        public CrouchingModule _crouching;
        public RotateModule _rotate;
        public InputManager _inputManager;

        // Physics parametrs
        public Vector3 CurrentVelocity { get; set; }
        public Quaternion CurrentRotation { get; set; }
        public float DeltaTime { get; set; }
        public Quaternion CameraPlanarRotation { get; set; }
        public Quaternion CameraRotation { get; set; }

        // Camera for read rotation (Cinemachine in character, not main camera)
        [SerializeField] public GameObject Camera;

        // Object for further management use
        public IInputController CharacterInputs;

        void Awake()
        {
            // Set CharacterController for main Core
            Motor.CharacterController = this as ICharacterController;

            // Initialize main controll modules
            CharacterInputs = new PlayerCharacterInputs();
            _inputManager = new InputManager(this, CharacterInputs, Camera);
            _move = new MoveModule(MoveConfig, this, CharacterInputs);
            _rotate = new RotateModule(RotateConfig, this, CharacterInputs);
            _jumping = new JumpingModule(JumpConfig, this, CharacterInputs);
            _crouching = new CrouchingModule(CrouchConfig, this, CharacterInputs);

        }

        private void CameraPlanarRotationChange(Quaternion camPlanRot)
        {
            CameraPlanarRotation = camPlanRot;
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
            // Move and look inputs
            _rotate.InputRotate();
            _move.InputMove();

            //// Jumping input
            _jumping.InputJump();

            //// Crouching input
            _crouching.InputCrouching();
        }

        public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        {
            DeltaTime = deltaTime;
            _rotate.HandlRotate();
            currentRotation = CurrentRotation;
        }

        public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        {
            // handle moveing
            _move.HandleMoveing();

            currentVelocity = CurrentVelocity;
            // Handle jumping
            _jumping.HandleJumping();
            //// Handle crouching
            _crouching.HandleCrouching();
        }

        public void AfterCharacterUpdate(float deltaTime)
        {
            // Handle jump-related values
            _jumping.AfterUpdateJumping();
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