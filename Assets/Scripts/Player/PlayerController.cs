using UnityEngine;
using UnityEngine.InputSystem;
namespace PLAYER {
    public class PlayerController : MonoBehaviour {
        [Header("Components")]
        [SerializeField] private Rigidbody2D rigidBody2D;
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private DistanceJoint2D distanceJoint2D;

        [Header("Layer Settings")]
        [SerializeField] private LayerMask grapplerLayer;
        [SerializeField] private LayerMask climbLayer;
        [SerializeField] private LayerMask groundLayer;

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
        [SerializeField] private PlayerHealth playerHealth;

        [Header("Booleans")]
        public bool IsJumping => playerJump.IsJumping;
        public bool IsClimbing => playerClimb.IsClimbing;
        public bool CanGrapple => playerGrappler.CanGrapple;

        private void Awake() {
            rigidBody2D = GetComponent<Rigidbody2D>();
            trailRenderer = GetComponent<TrailRenderer>();
            lineRenderer = GetComponent<LineRenderer>();
            distanceJoint2D = GetComponent<DistanceJoint2D>();
        }
        private void Start() {
            playerShoot.Initialize(gameObject, playerHealth, bulletPrefab, aimPrefab);
            playerMovement.Initialize(rigidBody2D, trailRenderer);
            playerJump.Initialize(rigidBody2D, groundLayer, IsClimbing);
            playerClimb.Initialize(rigidBody2D, climbLayer, IsJumping);
            playerStats.Initialize(this);
            playerHealth.Initialize();
            playerGrappler.Initialize(gameObject, lineRenderer, distanceJoint2D, grapplerLayer);
        }

        private void Update() {
            playerStats.OnUpdate();
            playerHealth.OnUpdate();
            playerGrappler.OnUpdate();
            playerShoot.OnUpdate();
        }
        private void FixedUpdate() {
            playerMovement.OnFixedUpdate();
            playerClimb.OnFixedUpdate();
        }
        public void OnMove(InputAction.CallbackContext context) => playerMovement.Move(context);
        public void OnClimb(InputAction.CallbackContext context) => playerClimb.Climb(context);
        public void OnJump(InputAction.CallbackContext context) => playerJump.Jump(context);
        public void OnGrapple(InputAction.CallbackContext context) => playerGrappler.Grapple(context);
        public void OnShoot(InputAction.CallbackContext context) => playerShoot.Shoot(context);
        private void OnCollisionEnter2D(Collision2D collision) {
            playerJump.CollisionEnter2D(collision);
            playerClimb.CollissionEnter2D(collision);
        }
        private void OnCollisionExit2D(Collision2D collision) => playerClimb.CollissionExit2D(collision);
        private void OnTriggerEnter2D(Collider2D collision) => playerClimb.TriggerEnter2D(collision);
        private void OnTriggerExit2D(Collider2D collision) => playerClimb.TriggerExit2D(collision);
    }
}