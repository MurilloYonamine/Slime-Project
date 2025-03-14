using System.Collections;
using AUDIO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PLAYER
{
    public class PlayerMovement : MonoBehaviour
    {

        [Header("Components")]
        [SerializeField] private Rigidbody2D rigidBody2D;
        [SerializeField] private TrailRenderer trailRenderer;
        public PlayerHealth playerHealth;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 10f;
        private Vector2 moveDirection;

        [Header("Jump Settings")]
        [SerializeField] private float jumpPower = 20f;
        [field: SerializeField] public bool IsJumping { get; private set; } = false;

        [Header("Wall Climb Settings")]
        [SerializeField] private float climbSpeed = 5f;
        [field: SerializeField] public bool IsClimbing { get; private set; } = false;
        private float climbDirection;

        [Header("Gravity Settings")]
        [SerializeField] private float gravityScale = 5f;
        [SerializeField] private float fallGravityScale = 10f;

        [Header("Layer Settings")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask climbableWallLayer;

        [Header("Go Up Settings")]
        [field: SerializeField] public bool CanGoUp { get; private set; } = true;
        [SerializeField] private float goUpHealth = 0.02f;


        private void Start()
        {
            rigidBody2D = GetComponent<Rigidbody2D>();
            rigidBody2D.gravityScale = gravityScale;

            trailRenderer = GetComponent<TrailRenderer>();
            playerHealth = GetComponent<PlayerHealth>();
        }

        private void FixedUpdate()
        {
            rigidBody2D.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rigidBody2D.linearVelocity.y);

            if (IsClimbing)
            {
                ClimbMovement();
            }
            else if (CanGoUp && playerHealth.Health > 1)
            {
                rigidBody2D.linearVelocityY = 10f;
                playerHealth.Health -= goUpHealth;
            }
            else
            {
                AdjustGravity();
            }
        }
        public void SwapActionGrappler(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_swap");
            }
        }

        public void Move(InputAction.CallbackContext context)
        {
            moveDirection = context.ReadValue<Vector2>();
            trailRenderer.emitting = moveDirection.x != 0;

            // Climb
            climbDirection = context.ReadValue<Vector2>().y;
            if (!IsClimbing) climbDirection = 0f;
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (IsClimbing && !CanGoUp) // Wall jump
                {
                    rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    IsClimbing = false;
                    IsJumping = true;
                }
                else if (!IsJumping && !CanGoUp) // Normal jump
                {
                    rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_jump");
                    IsJumping = true;
                }
            }
            if (context.canceled && rigidBody2D.linearVelocity.y > 0)
            {
                rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocity.x, rigidBody2D.linearVelocity.y * 0.6f);
            }
        }

        private void ClimbMovement()
        {
            if (Mathf.Abs(climbDirection) >= 0.1f)
            {
                rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocity.x, climbDirection * climbSpeed);
                rigidBody2D.gravityScale = 0f;
            }
            else
            {
                rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocity.x, -1f);
            }
        }

        private void AdjustGravity()
        {
            if (rigidBody2D.linearVelocity.y > 0)
            {
                rigidBody2D.gravityScale = gravityScale;
            }
            else
            {
                rigidBody2D.gravityScale = fallGravityScale;
            }
        }

        public void GoUp(InputAction.CallbackContext context)
        {
            if (context.performed) CanGoUp = true;
            if (context.canceled) CanGoUp = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (((1 << collision.gameObject.layer) & groundLayer) != 0)
            {
                IsJumping = false;
            }
            if (((1 << collision.gameObject.layer) & climbableWallLayer) != 0)
            {
                IsClimbing = true;
                IsJumping = false;

                rigidBody2D.linearVelocity = Vector2.zero;
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (((1 << collision.gameObject.layer) & climbableWallLayer) != 0)
            {
                IsClimbing = false;
                climbDirection = 0f;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (((1 << collision.gameObject.layer) & climbableWallLayer) != 0)
            {
                IsClimbing = true;
                IsJumping = false;

                rigidBody2D.linearVelocity = Vector2.zero;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (((1 << collision.gameObject.layer) & climbableWallLayer) != 0)
            {
                IsClimbing = false;
                climbDirection = 0f;
            }
        }
    }
}

