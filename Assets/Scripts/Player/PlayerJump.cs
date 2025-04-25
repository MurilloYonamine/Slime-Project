using AUDIO;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PLAYER {
    [Serializable]
    public class PlayerJump {
        private Rigidbody2D rigidBody2D;
        private PlayerController player;

        [SerializeField] private float jumpPower = 20f;
        [SerializeField] private float spikeJumpPower = 30f;
        [HideInInspector] public bool IsSpikeActive = false;
        private bool haveSpikeJumped = false;
        private bool haveSpikedAfterJump = false;

        private bool IsClimbing = false;

        private LayerMask groundLayer;

        public void Initialize(PlayerController player, Rigidbody2D rigidBody2D, LayerMask groundLayer, bool IsClimbing, bool IsSpikeActive) {
            this.player = player;
            this.rigidBody2D = rigidBody2D;
            this.groundLayer = groundLayer;
            this.IsSpikeActive = IsSpikeActive;
            this.IsClimbing = IsClimbing;
        }
        public void UpdateSpikeStatus(bool isSpikeActive) {
            if (!this.IsSpikeActive && isSpikeActive && player.IsJumping) haveSpikedAfterJump = true;
            
            this.IsSpikeActive = isSpikeActive;
        }

        public void Jump(InputAction.CallbackContext context) {
            if (context.started) {
                if (IsClimbing) // Wall jump
                {
                    rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    IsClimbing = false;
                    player.IsJumping = true;
                } else if (!player.IsJumping) // Normal jump
                {
                    if (IsSpikeActive) {
                        rigidBody2D.AddForce(Vector2.up * jumpPower * 1.5f, ForceMode2D.Impulse);
                        AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_jump");
                        player.IsJumping = true;
                        haveSpikeJumped = true;
                    } else {
                        rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                        AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_jump");
                        player.IsJumping = true;
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
            player.IsJumping = false;
        }
    }
}