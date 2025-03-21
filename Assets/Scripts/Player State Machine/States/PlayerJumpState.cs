using UnityEngine;
using UnityEngine.InputSystem;
namespace PLAYER {

    public class PlayerJumpState : IPlayerState {
        private readonly Rigidbody2D rigidBody2D;
        private readonly PlayerInputActions playerInputActions;
        private readonly float jumpPower;

        [Header("Gravity Settings")]
        private readonly float gravityScale;
        private readonly float fallGravityScale;

        public PlayerJumpState(Rigidbody2D rigidBody2D, PlayerInputActions playerInputActions, float jumpPower, float gravityScale, float fallGravityScale) {
            this.rigidBody2D = rigidBody2D;
            this.playerInputActions = playerInputActions;
            this.jumpPower = jumpPower;
            this.gravityScale = gravityScale;
            this.fallGravityScale = fallGravityScale;
        }

        public void OnEnter(PlayerController player) {
            rigidBody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            player.isJumping = true;
        }

        public void OnUpdate(PlayerController player) {
            if (playerInputActions.Player.Move.triggered) {
                player.ChangeState(player.playerMoveState);
            } else if (rigidBody2D.linearVelocity.y <= 0 && !player.isJumping) {
                player.ChangeState(player.playerIdleState);
            } 
        }

        public void OnFixedUpdate(PlayerController player) {
            if (rigidBody2D.linearVelocity.y > 0 && !playerInputActions.Player.Jump.IsPressed()) {
                // Se o jogador soltar o botão de pulo, aumenta a gravidade para cortar o pulo
                rigidBody2D.gravityScale = fallGravityScale * 0.8f;
            } else if (rigidBody2D.linearVelocity.y > 0) {
                rigidBody2D.gravityScale = gravityScale; // reduzido enquanto sobe
            } else {
                // Gravidade aumentada para quedas mais rápidas
                rigidBody2D.gravityScale = fallGravityScale; // quedas mais rapida
            }
        }
        public void OnExit(PlayerController player) { }
    }
}