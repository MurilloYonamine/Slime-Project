using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace PLAYER {
    public class PlayerController : MonoBehaviour {
        [Header("Components")]
        [SerializeField] private Rigidbody2D rigidBody2D;
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private DistanceJoint2D distanceJoint2D;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private PlayerInput playerInput;

        [Header("Layer Settings")]
        [SerializeField] private LayerMask grapplerLayer;
        [SerializeField] private LayerMask grapplerArea;
        [SerializeField] private LayerMask climbLayer;
        [SerializeField] private LayerMask groundLayer;

        [Header("Booleans")]
        public bool IsJumping = false;
        public bool IsClimbing = false;
        public bool IsGrappling = false;
        public bool CanGrapple = false;
        public bool IsSpikeActive = false;
        public bool IsPaused = false;
        public bool IsTakingDamage = false;
        public bool IsDead = false;
        public bool IsInsideWheat = false;
        public bool DisableStats = false;

        [Header("Prefabs")]

        [Header("Script Settings")]
        //[SerializeField] private PlayerStats playerStats;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerJump playerJump;
        [SerializeField] private PlayerClimb playerClimb;
        [SerializeField] private PlayerGrappler playerGrappler;
        [SerializeField] private PlayerShoot playerShoot;
        //[SerializeField] private PlayerSpike playerSpike;
        public PlayerHealth playerHealth;

        [Header("Enum Settings")]
        [HideInInspector] public CURSTRECH curstretch = CURSTRECH.normal;
        [HideInInspector] public CURSIZE cursize = CURSIZE.normal;
        public enum CURSTRECH { steched, normal }
        public enum CURSIZE { normal, small }

        private void Awake() {
            rigidBody2D = GetComponent<Rigidbody2D>();
            trailRenderer = GetComponent<TrailRenderer>();
            lineRenderer = GetComponent<LineRenderer>();
            distanceJoint2D = GetComponent<DistanceJoint2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            playerInput = GetComponent<PlayerInput>();
        }
        private void Start() {
            playerHealth.Initialize(this, rigidBody2D);
            playerShoot.Initialize(this, playerHealth);
            playerMovement.Initialize(this, rigidBody2D, trailRenderer, spriteRenderer, animator);
            playerJump.Initialize(this, rigidBody2D, groundLayer, animator);
            playerClimb.Initialize(this, rigidBody2D, climbLayer);
            playerGrappler.Initialize(this, lineRenderer, distanceJoint2D, grapplerLayer, grapplerArea, rigidBody2D, animator);

            //playerStats.Initialize(this);
        }

        private void Update() {
            playerGrappler.OnUpdate();
            playerShoot.OnUpdate();
            //playerStats.OnUpdate();
        }
        private void FixedUpdate() {
            playerMovement.OnFixedUpdate();
            playerClimb.OnFixedUpdate();
        }
        public void OnMove(InputAction.CallbackContext context) { if (!GameManager.Instance.isPaused) playerMovement.Move(context); }
        public void OnClimb(InputAction.CallbackContext context) { if (!GameManager.Instance.isPaused) playerClimb.Climb(context); }
        public void OnJump(InputAction.CallbackContext context) { if (!GameManager.Instance.isPaused) playerJump.Jump(context); }
        public void OnGrapple(InputAction.CallbackContext context) { if (!GameManager.Instance.isPaused) playerGrappler.Grapple(context); }
        public void OnShoot(InputAction.CallbackContext context) { if (!GameManager.Instance.isPaused) playerShoot.Shoot(context); }
        //public void OnSpike(InputAction.CallbackContext context) { if (!IsPaused) playerSpike.Spike(context); }
        public void OnChangeSpeed(float speed) => playerMovement.ChangeSpeed(speed);
        public void OnResetSpeed() => playerMovement.ResetSpeed();
        //public void DisableSpike() => playerSpike.DisableSpike();
        public void UpdateHealth() => playerHealth.HandleHealing();
        public void TogglePlayerInput() => playerInput.enabled = !playerInput.enabled;

        private void OnCollisionEnter2D(Collision2D collision2D) {
            playerJump.CollisionEnter2D(collision2D);
            playerClimb.CollissionEnter2D(collision2D);
        }
        private void OnCollisionExit2D(Collision2D collision2D) {
            playerClimb.CollissionExit2D(collision2D);
        }
        private void OnTriggerEnter2D(Collider2D collision2D) {
            playerClimb.TriggerEnter2D(collision2D);
            playerGrappler.TriggerEnter2D(collision2D);
        }
        private void OnTriggerExit2D(Collider2D collision2D) {
            playerClimb.TriggerExit2D(collision2D);
            playerGrappler.TriggerExit2D(collision2D);
        }
    }
}