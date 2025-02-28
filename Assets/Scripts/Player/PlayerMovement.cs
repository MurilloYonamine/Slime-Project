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
        [SerializeField] private float jumpPower = 10f;
        [SerializeField] private bool isJumping = false;

        [Header("Gravity Settings")]
        [SerializeField] private float gravityScale = 5f;
        [SerializeField] private float fallGravityScale = 15f;


        private void Start() {
            rigidBody2D = GetComponent<Rigidbody2D>();
            rigidBody2D.gravityScale = gravityScale;

            trailRenderer = GetComponent<TrailRenderer>();
        }
        private void FixedUpdate() {
            rigidBody2D.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rigidBody2D.linearVelocity.y);
            AdjustGravity();
        }

        public void Jump(InputAction.CallbackContext context) {
            if (context.started && !isJumping) {
                rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_jump");
                isJumping = true;
            }
            if (context.canceled && rigidBody2D.linearVelocity.y > 0) {
                rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocity.x, rigidBody2D.linearVelocity.y * 0.6f);
            }
        }

        private void AdjustGravity() {
            if (rigidBody2D.linearVelocity.y > 0) {
                rigidBody2D.gravityScale = gravityScale;
            } else {
                rigidBody2D.gravityScale = fallGravityScale;
            }
        }

        public void Move(InputAction.CallbackContext context) {
            moveDirection = context.ReadValue<Vector2>();
            trailRenderer.emitting = moveDirection.x != 0;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Ground")) {
                Debug.Log("Está no Chão!");
                isJumping = false;
            }
        }
    }
}