using AUDIO;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PLAYER
{
    [Serializable]
    public class PlayerJump
    {
        private Rigidbody2D rigidBody2D;
        private Animator animator;
        private PlayerController player;

        [SerializeField] private float jumpPower = 20f;

        private LayerMask groundLayer;

        public void Initialize(PlayerController player, Rigidbody2D rigidBody2D, LayerMask groundLayer, Animator animator)
        {
            this.player = player;
            this.rigidBody2D = rigidBody2D;
            this.groundLayer = groundLayer;
            this.animator = animator;
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (!player.IsJumping) // Normal jump
                {
                    rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_jump");
                    player.IsJumping = true;
                    animator.SetBool("IsJumping", player.IsJumping);
                    player.IsClimbing = false;
                }
            }
            if (context.canceled && rigidBody2D.linearVelocity.y > 0 && !CheatManager.Instance.flying)
            {
                rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocity.x, rigidBody2D.linearVelocity.y * 0.6f);
            }
        }

        public void CollisionEnter2D(Collision2D collision2D)
        {
            player.IsJumping = false;
            animator.SetBool("IsJumping", player.IsJumping);
        }
    }
}