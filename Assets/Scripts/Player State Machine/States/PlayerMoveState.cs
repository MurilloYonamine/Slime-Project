using UnityEngine;
using UnityEngine.InputSystem;
namespace PLAYER {

    public class PlayerMoveState : IPlayerState {
        private readonly Rigidbody2D rigidBody2D;
        private readonly TrailRenderer trailRenderer;
        private readonly PlayerInputActions playerInputActions;

        private readonly float moveSpeed;

        public PlayerMoveState(float moveSpeed, Rigidbody2D rigidBody2D, TrailRenderer trailRenderer, PlayerInputActions playerInputActions) {
            this.moveSpeed = moveSpeed;
            this.rigidBody2D = rigidBody2D;
            this.trailRenderer = trailRenderer;
            this.playerInputActions = playerInputActions;
        }
        public void OnEnter(PlayerController player) {
            playerInputActions.Player.Move.Enable();
        }
        public void OnUpdate(PlayerController player) {
            if (playerInputActions.Player.Move.ReadValue<Vector2>() == Vector2.zero && !player.isJumping) {
                rigidBody2D.linearVelocity = Vector2.zero;
                player.ChangeState(player.playerIdleState);
            } else if (playerInputActions.Player.Jump.triggered) {
                player.ChangeState(player.playerJumpState);
            } 
        }
        public void OnFixedUpdate(PlayerController player) {
            Vector2 moveInput = playerInputActions.Player.Move.ReadValue<Vector2>();
            float targetVelocityX = moveInput.x * moveSpeed;
            float smoothedVelocityX = Mathf.Lerp(rigidBody2D.linearVelocity.x, targetVelocityX, 0.1f);
            rigidBody2D.linearVelocity = new Vector2(smoothedVelocityX, rigidBody2D.linearVelocity.y);
            trailRenderer.emitting = moveInput.x != 0;
        }
        public void OnExit(PlayerController player) {
            playerInputActions.Player.Move.Disable();
        }
    }
}