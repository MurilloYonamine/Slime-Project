using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PLAYER {
    [Serializable]
    public class PlayerSpike {
        [HideInInspector] public PlayerController player;
        [HideInInspector] public bool CanScale = true;


        [SerializeField] private float xScale = 4f;
        [SerializeField] private float yScale = 0.5f;
        [SerializeField] private float smoothTime = 0.2f;
        [SerializeField] private Vector2 targetScale;
        private Vector2 velocity = new Vector2(2,2);
        private Vector2 initialScale;
        private float CurTime;

        public void Initialize(PlayerController player) {
            this.player = player;
            this.initialScale = player.transform.localScale;
            this.targetScale = initialScale;
            CurTime = smoothTime+0.8f;
        }
        public void OnUpdate() {
            if (player.cursize == PlayerController.CURSIZE.normal){
                this.initialScale = new Vector2(1f,1f);
                xScale = 4f;
                yScale = 0.5f;
            } else{
                this.initialScale = new Vector2(0.5f,0.5f);
                xScale = 2f;
                yScale = 0.25f;
            }


            if (CanScale){
                CurTime -= Time.deltaTime;
                player.transform.localScale = Vector2.SmoothDamp(player.transform.localScale, targetScale, ref velocity, smoothTime);
                if (CurTime <= 0){
                    CanScale = false;
                    CurTime = smoothTime+0.8f;
                    if (player.curstretch == PlayerController.CURSTRECH.normal){
                        player.curstretch = PlayerController.CURSTRECH.steched;
                    } else if (player.curstretch == PlayerController.CURSTRECH.steched){
                        player.curstretch = PlayerController.CURSTRECH.normal;
                    }
                }
            }
        }
        public void Spike(InputAction.CallbackContext context) {
            if (context.performed && !CanScale) {
                player.IsSpikeActive = !player.IsSpikeActive;
                targetScale = new Vector2(player.IsSpikeActive ? xScale : initialScale.x, player.IsSpikeActive ? yScale : initialScale.y);
                AllowedScalingFor90PercentOff();
            }

        }
        public void DisableSpike() {
            player.IsSpikeActive = false;
            targetScale = initialScale;
        }

        //public void UpdateScaleStatus(Vector2 Scale) => this.targetScale = Scale;
        private void AllowedScalingFor90PercentOff()
        {
            CanScale = true;
        }
    }
}
