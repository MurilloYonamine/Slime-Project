using AUDIO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PLAYER {
    public class PlayerMovement : MonoBehaviour {

        [Header("Components")]
        [SerializeField] private Rigidbody2D rigidBody2D;
        [SerializeField] private TrailRenderer trailRenderer;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 10f;
        private Vector2 moveDirection;

        [Header("Jump Settings")]
        [SerializeField] private float jumpPower = 20f;
        [SerializeField] public bool isJumping { get; private set; } = false;

        [Header("Wall Climb Settings")]
        [SerializeField] private float climbSpeed = 5f;
        public bool isClimbing { get; private set; } = false;
        private float climbDirection;

        [Header("Gravity Settings")]
        [SerializeField] private float gravityScale = 5f;
        [SerializeField] private float fallGravityScale = 10f;

        [Header("Layer Settings")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask climbableWallLayer;

        private void Start() {
            rigidBody2D = GetComponent<Rigidbody2D>();
            rigidBody2D.gravityScale = gravityScale;

            trailRenderer = GetComponent<TrailRenderer>();
        }

        private void FixedUpdate() {
            rigidBody2D.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rigidBody2D.linearVelocity.y);

            if (isClimbing) {
                ClimbMovement();
            } else {
                AdjustGravity();
            }
        }

        public void Move(InputAction.CallbackContext context) {
            moveDirection = context.ReadValue<Vector2>();
            trailRenderer.emitting = moveDirection.x != 0;
        }

        public void Jump(InputAction.CallbackContext context) {
            if (context.started) {
                if (isClimbing) {
                    WallJump();
                } else if (!isJumping) {
                    rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_jump");
                    isJumping = true;
                }
            }
            if (context.canceled && rigidBody2D.linearVelocity.y > 0) {
                rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocity.x, rigidBody2D.linearVelocity.y * 0.6f);
            }
        }

        public void Climb(InputAction.CallbackContext context) {
            climbDirection = context.ReadValue<Vector2>().y;

            if (!isClimbing) climbDirection = 0f;
        }

        private void ClimbMovement() {
            if (Mathf.Abs(climbDirection) > 0.1f) {
                rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocity.x, climbDirection * climbSpeed);
                rigidBody2D.gravityScale = 0f;
            } else {
                rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocity.x, -1f);
            }
        }
        private void WallJump() {
            Vector2 jumpDirection = new Vector2(moveDirection.x * 5f, 1f).normalized;
            rigidBody2D.linearVelocity = Vector2.zero;
            rigidBody2D.AddForce(jumpDirection * jumpPower, ForceMode2D.Impulse);

            isClimbing = false;
        }
        private void AdjustGravity() {
            if (rigidBody2D.linearVelocity.y > 0) {
                rigidBody2D.gravityScale = gravityScale;
            } else {
                rigidBody2D.gravityScale = fallGravityScale;
            }
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (((1 << collision.gameObject.layer) & groundLayer) != 0) {
                isJumping = false;
            }
            if (((1 << collision.gameObject.layer) & climbableWallLayer) != 0) {
                isClimbing = true;
                isJumping = false;

                rigidBody2D.linearVelocity = Vector2.zero;
            }
        }
        private void OnCollisionExit2D(Collision2D collision) {
            if (((1 << collision.gameObject.layer) & climbableWallLayer) != 0) {
                isClimbing = false;
                climbDirection = 0f;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision) {
            if (((1 << collision.gameObject.layer) & climbableWallLayer) != 0) {
                isClimbing = true;
                isJumping = false;

                rigidBody2D.linearVelocity = Vector2.zero;
            }
        }
        private void OnTriggerExit2D(Collider2D collision) {
            if (((1 << collision.gameObject.layer) & climbableWallLayer) != 0) {
                isClimbing = false;
                climbDirection = 0f;
            }
        } 
    }
}

