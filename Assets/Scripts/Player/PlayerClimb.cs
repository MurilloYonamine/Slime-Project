using AUDIO;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace PLAYER
{
    [Serializable]
    public class PlayerClimb
    {
        private PlayerController player;
        private Rigidbody2D rigidBody2D;

        [SerializeField] private float climbSpeed = 5f;
        [HideInInspector] public bool IsClimbing = false;
        public float climbDirection = 0f;

        [SerializeField] private float gravityScale = 5f;
        [SerializeField] private float fallGravityScale = 10f;

        private LayerMask climbLayer;

        public void Initialize(PlayerController player, Rigidbody2D rigidBody2D, LayerMask climbLayer)
        {
            this.player = player;
            this.rigidBody2D = rigidBody2D;
            this.climbLayer = climbLayer;

            rigidBody2D.gravityScale = gravityScale;
        }

        public void OnFixedUpdate()
        {
            if (IsClimbing)
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
                return;
            }
            AdjustGravity();
        }
        public void Climb(InputAction.CallbackContext context)
        {
            climbDirection = context.ReadValue<Vector2>().y;
            if (!IsClimbing) climbDirection = 0f;
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
        public void CollissionEnter2D(Collision2D collision2D)
        {
            if (((1 << collision2D.gameObject.layer) & climbLayer) != 0)
            {
                player.IsJumping = false;
                IsClimbing = true;

                rigidBody2D.linearVelocity = Vector2.zero;
            }
        }
        public void CollissionExit2D(Collision2D collision2D)
        {
            if (((1 << collision2D.gameObject.layer) & climbLayer) != 0)
            {
                IsClimbing = false;
                climbDirection = 0f;
            }
        }
        public void TriggerEnter2D(Collider2D collider2D)
        {
            if (((1 << collider2D.gameObject.layer) & climbLayer) != 0)
            {
                IsClimbing = true;
                player.IsJumping = false;

                rigidBody2D.linearVelocity = Vector2.zero;
            }
        }
        public void TriggerExit2D(Collider2D collider2D)
        {
            if (((1 << collider2D.gameObject.layer) & climbLayer) != 0)
            {
                IsClimbing = false;
                climbDirection = 0f;
            }
        }
    }
}