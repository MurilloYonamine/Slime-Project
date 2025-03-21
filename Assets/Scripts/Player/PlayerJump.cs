using AUDIO;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PLAYER {
    [Serializable]
    public class PlayerJump {
        private Rigidbody2D rigidBody2D;

        [SerializeField] private float jumpPower = 20f;
        [HideInInspector] public bool IsJumping = false;

        private bool IsClimbing = false;

        private LayerMask groundLayer;

        public void Initialize(Rigidbody2D rigidBody2D, LayerMask groundLayer, bool IsClimbing) {
            this.rigidBody2D = rigidBody2D;
            this.groundLayer = groundLayer;
            this.jumpPower = 20f;
            this.IsClimbing = IsClimbing;
            this.IsJumping = false;
        }

        public void Jump(InputAction.CallbackContext context) {
            if (context.started) {
                if (IsClimbing) // Wall jump
                {
                    rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    IsClimbing = false;
                    IsJumping = true;
                } else if (!IsJumping) // Normal jump
                  {
                    rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_jump");
                    IsJumping = true;
                }
            }
            if (context.canceled && rigidBody2D.linearVelocity.y > 0) {
                rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocity.x, rigidBody2D.linearVelocity.y * 0.6f);
            }
        }
        public void CollisionEnter2D(Collision2D collision2D) {
            if (((1 << collision2D.gameObject.layer) & groundLayer) != 0) {
                IsJumping = false;
            }
        }
    }
}