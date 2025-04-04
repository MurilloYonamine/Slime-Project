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
        [SerializeField] private Vector2 targetScale;
        private Vector2 velocity = Vector2.zero;
        private Vector2 initialScale;

        public void Initialize(PlayerController player) {
            this.player = player;
            this.initialScale = player.transform.localScale;
            this.targetScale = initialScale;
        }
        public void OnUpdate() {
            player.transform.localScale = Vector2.SmoothDamp(player.transform.localScale, targetScale, ref velocity, smoothTime);
        }
        public void Spike(InputAction.CallbackContext context) {
            if (context.performed && !IsInsideWeed) {
                player.IsSpikeActive = !player.IsSpikeActive;
                targetScale = new Vector2(player.IsSpikeActive ? xScale : initialScale.x, player.IsSpikeActive ? yScale : initialScale.y);
            }
        }
        public void DisableSpike() {
            player.IsSpikeActive = false;
            targetScale = initialScale;
        }
        public void UpdateWheatStatus(bool IsInsideWeed) => this.IsInsideWeed = IsInsideWeed;

        public void UpdateScaleStatus(Vector2 Scale) => this.targetScale = Scale;
    }
}
