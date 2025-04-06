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
        [SerializeField] public Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BoxCollider2D boxCollider2D;

        [Header("Layer Settings")]
        [SerializeField] private LayerMask grapplerLayer;
        [SerializeField] private LayerMask climbLayer;
        [SerializeField] private LayerMask groundLayer;

        [Header("Booleans")]
        public bool IsJumping;
        public bool IsClimbing => playerClimb.IsClimbing;
        public bool CanGrapple => playerGrappler.CanGrapple;
        public bool IsSpikeActive;
        public bool IsPaused;
        public bool takingDamage;
        public bool IsDead;
        public bool IsInsideWheat;
        public bool DisableStats;

        [Header("Prefabs")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private RectTransform aimPrefab;

        [Header("Script Settings")]
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerJump playerJump;
        [SerializeField] private PlayerClimb playerClimb;
        [SerializeField] private PlayerGrappler playerGrappler;
        [SerializeField] private PlayerShoot playerShoot;
        [SerializeField] private PlayerSpike playerSpike;
        [SerializeField] public PlayerHealth playerHealth;

        private void Awake() {
            rigidBody2D = GetComponent<Rigidbody2D>();
            trailRenderer = GetComponent<TrailRenderer>();
            lineRenderer = GetComponent<LineRenderer>();
            distanceJoint2D = GetComponent<DistanceJoint2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider2D = GetComponent<BoxCollider2D>();
        }
        private void Start() {
            playerShoot.Initialize(gameObject, playerHealth, bulletPrefab, aimPrefab,this);
            playerMovement.Initialize(rigidBody2D, trailRenderer, spriteRenderer, animator);
            playerJump.Initialize(this, rigidBody2D, groundLayer, IsClimbing, IsSpikeActive);
            playerClimb.Initialize(rigidBody2D, climbLayer, IsJumping);
            playerHealth.Initialize(rigidBody2D, this);
            playerGrappler.Initialize(gameObject, lineRenderer, distanceJoint2D, grapplerLayer);
            playerSpike.Initialize(this);

            playerStats.Initialize(this);
        }

        private void Update() {
            playerStats.OnUpdate();
            playerHealth.OnUpdate();
            playerGrappler.OnUpdate();
            playerShoot.OnUpdate();
            playerSpike.OnUpdate();

            playerJump.UpdateSpikeStatus(IsSpikeActive);
            playerSpike.UpdateWheatStatus(IsInsideWheat);
            playerSpike.UpdateScaleStatus(transform.localScale);
            playerClimb.UpdateJumpStatus(IsJumping);
        }
        private void FixedUpdate() {
            playerMovement.OnFixedUpdate();
            playerClimb.OnFixedUpdate();
        }
        public void OnMove(InputAction.CallbackContext context) { if (!IsPaused) playerMovement.Move(context); }
        public void OnClimb(InputAction.CallbackContext context) { if (!IsPaused) playerClimb.Climb(context); }
        public void OnJump(InputAction.CallbackContext context) { if (!IsPaused) playerJump.Jump(context); }
        public void OnGrapple(InputAction.CallbackContext context) { if (!IsPaused) playerGrappler.Grapple(context); }
        public void OnShoot(InputAction.CallbackContext context) { if (!IsPaused) playerShoot.Shoot(context); }
        public void OnSpike(InputAction.CallbackContext context) { if (!IsPaused) playerSpike.Spike(context); }

        public void StartChangeSpeed(float speed, float timeToNormalize) => StartCoroutine(playerMovement.ChangeSpeed(speed, timeToNormalize));
        public void DisableSpike() => playerSpike.DisableSpike();

        private void OnCollisionEnter2D(Collision2D collision) {
            playerJump.CollisionEnter2D(collision);
            playerClimb.CollissionEnter2D(collision);
        }
        private void OnCollisionExit2D(Collision2D collision) {
            playerClimb.CollissionExit2D(collision);
        }
        private void OnTriggerEnter2D(Collider2D collision) => playerClimb.TriggerEnter2D(collision);
        private void OnTriggerExit2D(Collider2D collision) => playerClimb.TriggerExit2D(collision);
    }
}