using Controller;
using KinematicCharacterController;
using UnityEngine;


namespace Player
{
    namespace Controller
    {
        public class PlayerController : MonoBehaviour, ICharacterController, IPhysicsController
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
            public PlayerUpdateInputController _inputManager;

            // Physics parametrs
            public Vector3 CurrentVelocity { get; set; }
            public Quaternion CurrentRotation { get; set; }
            public float DeltaTime { get; set; }
            public Quaternion CameraPlanarRotation { get; set; }
            public Quaternion CameraRotation { get; set; }

            // Camera for read rotation (Cinemachine in character, not main camera)
            [SerializeField] public GameObject Camera;

            // Object for further management use
            public AInputController PlayerInputs;

            void Awake()
            {
                // Set CharacterController for main Core
                Motor.CharacterController = this as ICharacterController;

                // Initialize main controll modules
                PlayerInputs = new PlayerInputController();
                _inputManager = new PlayerUpdateInputController(this, PlayerInputs, Camera);
                _move = new MoveModule(MoveConfig, this, PlayerInputs);
                _rotate = new RotateModule(RotateConfig, this, PlayerInputs);
                _jumping = new JumpingModule(JumpConfig, this, PlayerInputs);
                _crouching = new CrouchingModule(CrouchConfig, this, PlayerInputs);

            }

            private void Update()
            {
                _inputManager.Update();
            }

            public void SetInputs()
            {
                // Move and look inputs
                _rotate.HandleInput();
                _move.HandleInput();

                //// Jumping input
                _jumping.HandleInput();

                //// Crouching input
                _crouching.HandleInput();
            }

            public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
            {
                DeltaTime = deltaTime;
                _rotate.HandleUpdate();
                currentRotation = CurrentRotation;
            }

            public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
            {
                // handle moveing
                _move.HandleUpdate();

                currentVelocity = CurrentVelocity;
                // Handle jumping
                _jumping.HandleUpdate();
                //// Handle crouching
                _crouching.HandleUpdate();
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

            private void OnEnable()
            {
                _inputManager.Enable();
            }

            private void OnDisable()
            {
                _inputManager.Disable();
            }

        }
    }
}