using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PLAYER {
    [Serializable]
    public class PlayerSpike {
        [HideInInspector] public GameObject player;
        [HideInInspector] public bool IsSpikeActive = false;
        [SerializeField] private float xScale = 4f;
        [SerializeField] private float yScale = 0.5f;

        [SerializeField] private float smoothTime = 0.2f;
        private Vector2 targetScale;
        private Vector2 velocity = Vector2.zero;

        public void Initialize(GameObject player) {
            this.player = player;
            this.targetScale = player.transform.localScale;
        }

        public void Spike(InputAction.CallbackContext context) {
            if (context.performed) {
                IsSpikeActive = !IsSpikeActive;
                targetScale = new Vector2(IsSpikeActive ? xScale : 1, IsSpikeActive ? yScale : 1);
            }
        }

        public void OnUpdate() {
            player.transform.localScale = Vector2.SmoothDamp(player.transform.localScale, targetScale, ref velocity, smoothTime);
        }
    }
}
