using UnityEngine;
namespace PLAYER {

    public class PlayerIdleState : IPlayerState {
        private readonly Rigidbody2D rigidBody2D;
        private readonly PlayerInputActions playerInputActions;

        public PlayerIdleState(Rigidbody2D rigidBody2D, PlayerInputActions playerInputActions) {
            this.rigidBody2D = rigidBody2D;
            this.playerInputActions = playerInputActions;
        }
        public void OnEnter(PlayerController player) {
            playerInputActions.Player.Move.Enable();
            playerInputActions.Player.Jump.Enable();
            playerInputActions.Player.Grappler.Enable();
            rigidBody2D.linearVelocity = new Vector2(Mathf.Lerp(rigidBody2D.linearVelocity.x, 0, 0.2f), rigidBody2D.linearVelocity.y);
        }
        public void OnUpdate(PlayerController player) {
            if (playerInputActions.Player.Move.ReadValue<Vector2>() != Vector2.zero) {
                player.ChangeState(player.playerMoveState);
            } else if (playerInputActions.Player.Jump.triggered) {
                player.ChangeState(player.playerJumpState);
            } 
        }
        public void OnFixedUpdate(PlayerController player) {
            rigidBody2D.linearVelocity = Vector2.zero;
        }
        public void OnExit(PlayerController player) { }
    }
}