using AUDIO;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace PLAYER {
    [Serializable]
    public class PlayerMovement {
        private Rigidbody2D rigidBody2D;
        private TrailRenderer trailRenderer;
        private SpriteRenderer spriteRenderer;
        private Animator animator;

        [SerializeField] private float originalSpeed;
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private Vector2 moveDirection;
        bool faceDirection => moveDirection.x < 0; // true if moving right, false if moving left

        public void Initialize(Rigidbody2D rigidBody2D, TrailRenderer trailRenderer, SpriteRenderer spriteRenderer, Animator animator) {
            this.rigidBody2D = rigidBody2D;
            this.trailRenderer = trailRenderer;
            this.spriteRenderer = spriteRenderer;
            this.animator = animator;

            this.moveSpeed = 10f;
            this.originalSpeed = this.moveSpeed;
            this.moveDirection = Vector2.zero;
        }

        public void OnFixedUpdate() {
            // Debug.Log($"Animator Bool: {animator.GetBool("IsWalking")}");
            // Debug.Log($"Face Direction: {faceDirection}");

            rigidBody2D.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rigidBody2D.linearVelocity.y);
        }

        public void Move(InputAction.CallbackContext context) {
            spriteRenderer.flipX = faceDirection;

            if (!animator.GetBool("IsWalking")) spriteRenderer.flipX = faceDirection;

            moveDirection = context.ReadValue<Vector2>();

            animator.SetBool("IsWalking", moveDirection.x != 0);

            trailRenderer.emitting = moveDirection.x != 0;
        }
        public IEnumerator ChangeSpeed(float speed, float timeToNormalize) {
            moveSpeed = speed;
            yield return new WaitForSeconds(timeToNormalize);
            moveSpeed = originalSpeed;
        }
    }
}

