using AUDIO;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PLAYER {
    [Serializable]
    public class PlayerMovement {
        private Rigidbody2D rigidBody2D;
        private TrailRenderer trailRenderer;

        [SerializeField] private float originalSpeed;
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private Vector2 moveDirection;

        public void Initialize(Rigidbody2D rigidBody2D, TrailRenderer trailRenderer) {
            this.rigidBody2D = rigidBody2D;
            this.trailRenderer = trailRenderer;

            this.moveSpeed = 10f;
            this.originalSpeed = this.moveSpeed;
            this.moveDirection = Vector2.zero;
        }

        public IEnumerator ChangeSpeed(float speed, float timeToNormalize) {
            moveSpeed = speed;
            yield return new WaitForSeconds(timeToNormalize);
            moveSpeed = originalSpeed;
        }

        public void OnFixedUpdate() {
            rigidBody2D.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rigidBody2D.linearVelocity.y);
        }

        public void Move(InputAction.CallbackContext context) {
            moveDirection = context.ReadValue<Vector2>();
            trailRenderer.emitting = moveDirection.x != 0;
        }
    }
}

