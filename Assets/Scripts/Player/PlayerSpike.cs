using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PLAYER {
    [Serializable]
    public class PlayerSpike {
        [HideInInspector] public PlayerController player;
        [HideInInspector] public bool IsInsideWeed = false;

        [SerializeField] private float xScale = 4f;
        [SerializeField] private float yScale = 0.5f;
        [SerializeField] private float smoothTime = 0.2f;
        private Vector2 targetScale;
        private Vector2 velocity = Vector2.zero;

        public void Initialize(PlayerController player) {
            this.player = player;
            this.targetScale = player.transform.localScale;
        }
        public void OnUpdate() {
            player.transform.localScale = Vector2.SmoothDamp(player.transform.localScale, targetScale, ref velocity, smoothTime);
        }
        public void Spike(InputAction.CallbackContext context) {
            if (context.performed && !IsInsideWeed) {
                player.IsSpikeActive = !player.IsSpikeActive;
                targetScale = new Vector2(player.IsSpikeActive ? xScale : 1, player.IsSpikeActive ? yScale : 1);
            }
        }
        public void DisableSpike() {
            player.IsSpikeActive = false;
            targetScale = new Vector2(1, 1);
        }
        public void UpdateWheatStatus(bool IsInsideWeed) => this.IsInsideWeed = IsInsideWeed;
    }
}
