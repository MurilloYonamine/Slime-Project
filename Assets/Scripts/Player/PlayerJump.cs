using AUDIO;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PLAYER {
    [Serializable]
    public class PlayerJump {
        private Rigidbody2D rigidBody2D;

        [SerializeField] private float jumpPower = 20f;
        [SerializeField] private float spikeJumpPower = 30f;
        [HideInInspector] public bool IsJumping = false;
        [HideInInspector] public bool IsSpikeActive = false;
        private bool haveSpikeJumped = false;
        private bool haveSpikedAfterJump = false;

        private bool IsClimbing = false;

        private LayerMask groundLayer;

        public void Initialize(Rigidbody2D rigidBody2D, LayerMask groundLayer, bool IsClimbing, bool IsSpikeActive) {
            this.rigidBody2D = rigidBody2D;
            this.groundLayer = groundLayer;
            this.IsSpikeActive = IsSpikeActive;
            this.IsClimbing = IsClimbing;
        }
        public void UpdateSpikeStatus(bool isSpikeActive) {
            if (!this.IsSpikeActive && isSpikeActive && IsJumping) {
                haveSpikedAfterJump = true;
            }
            this.IsSpikeActive = isSpikeActive;
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
                    if (IsSpikeActive) {
                        rigidBody2D.AddForce(Vector2.up * jumpPower * 1.5f, ForceMode2D.Impulse);
                        AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_jump");
                        IsJumping = true;
                        haveSpikeJumped = true;
                    } else {
                        rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                        AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_jump");
                        IsJumping = true;
                    }
                }
            }
            if (context.canceled && rigidBody2D.linearVelocity.y > 0) {
                rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocity.x, rigidBody2D.linearVelocity.y * 0.6f);
            }
        }

        public void CollisionEnter2D(Collision2D collision2D) {
            if (((1 << collision2D.gameObject.layer) & groundLayer) != 0) {
                if (haveSpikeJumped || haveSpikedAfterJump) {
                    rigidBody2D.AddForce(Vector2.up * spikeJumpPower, ForceMode2D.Impulse);
                    AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_jump");
                    haveSpikeJumped = false;
                    haveSpikedAfterJump = false;
                }
            }
            IsJumping = false;
        }
    }
}