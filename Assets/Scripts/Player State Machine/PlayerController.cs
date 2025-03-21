using AUDIO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace PLAYER {
    public class PlayerController : MonoBehaviour {
        [Header("Layer Settings")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask climbableWallLayer;

        [Header("Player Components")]
        [SerializeField] private Rigidbody2D rigidBody2D;
        [SerializeField] private TrailRenderer trailRenderer;
        private PlayerInputActions playerInputActions;
        [SerializeField] private PlayerShoot playerShoot;

        [Header("Prefabs")]
        [SerializeField] private RectTransform aimPrefab;

        [Header("Jump Settings")]
        [SerializeField] private float jumpPower = 20f;
        [field: SerializeField] public bool isJumping = false;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 10f;

        [Header("Gravity Settings")]
        [SerializeField] private float gravityScale = 5f;
        [SerializeField] private float fallGravityScale = 10f;

        [Header("Grappler Settings")]
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private DistanceJoint2D distanceJoint2D;
        [SerializeField] private LayerMask grappleLayer;
        [SerializeField] private float grappleMaxPoint = 5f;
        [field: SerializeField] public bool IsGrappleWithinMaxDistance { get; private set; } = true;
        [field: SerializeField] public bool CanGrapple { get; private set; } = false;

        [Header("Player States")]
        public IPlayerState currentState;
        public PlayerIdleState playerIdleState;
        public PlayerMoveState playerMoveState;
        public PlayerJumpState playerJumpState;
        public PlayerGrapplerState playerGrapplerState;

        private void Awake() {
            rigidBody2D = GetComponent<Rigidbody2D>();
            trailRenderer = GetComponent<TrailRenderer>();
            playerInputActions = new PlayerInputActions();

            playerInputActions.Player.Attack.Enable();
        }

        private void Start() {
            lineRenderer.enabled = false;
            distanceJoint2D.enabled = false;

            playerMoveState = new PlayerMoveState(moveSpeed, rigidBody2D, trailRenderer, playerInputActions);
            playerIdleState = new PlayerIdleState(rigidBody2D, playerInputActions);
            playerJumpState = new PlayerJumpState(rigidBody2D, playerInputActions, jumpPower, gravityScale, fallGravityScale);
            playerGrapplerState = new PlayerGrapplerState(lineRenderer, distanceJoint2D, grappleLayer, grappleMaxPoint, playerInputActions);

            ChangeState(playerIdleState);
        }

        void Update() {
            currentState.OnUpdate(this);

            aimPrefab.anchoredPosition = Mouse.current.position.value;

            if (playerInputActions.Player.Attack.triggered) playerShoot.Shoot(this);
        }

        public void ChangeState(IPlayerState newState) {
            currentState?.OnExit(this);
            currentState = newState;
            currentState.OnEnter(this);
        }

        private void FixedUpdate() {
            currentState.OnFixedUpdate(this);
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0) {
                isJumping = false;
                if (currentState is PlayerJumpState) {
                    ChangeState(playerIdleState);
                }
            }
        }
        private void OnCollisionExit2D(Collision2D collision) {
            if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0) {
                isJumping = true;
            }
        }
    }
}