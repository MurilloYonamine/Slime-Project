using AUDIO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace PLAYER {
    public class PlayerGrapplerState : IPlayerState {
        private readonly LineRenderer lineRenderer;
        private readonly DistanceJoint2D distanceJoint2D;
        private readonly LayerMask grappleLayer;
        private readonly float grappleMaxPoint;
        private readonly PlayerInputActions playerInputActions;

        public PlayerGrapplerState(LineRenderer lineRenderer, DistanceJoint2D distanceJoint2D, LayerMask grappleLayer, float grappleMaxPoint, PlayerInputActions playerInputActions) {
            this.lineRenderer = lineRenderer;
            this.distanceJoint2D = distanceJoint2D;
            this.grappleLayer = grappleLayer;
            this.grappleMaxPoint = grappleMaxPoint;
            this.playerInputActions = playerInputActions;
        }

        public void OnEnter(PlayerController player) {
            playerInputActions.Player.Grappler.Enable();
        }

        public void OnUpdate(PlayerController player) {
            if (playerInputActions.Player.Grappler.triggered) {
                HandleGrapple(player);
            } else if (!distanceJoint2D.enabled) {
                player.ChangeState(player.playerIdleState);
            }
        }

        public void OnFixedUpdate(PlayerController player) { }

        public void OnExit(PlayerController player) {
            playerInputActions.Player.Grappler.Disable();
            distanceJoint2D.enabled = false;
            lineRenderer.enabled = false;
        }

        private void HandleGrapple(PlayerController player) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, mousePosition - (Vector2)player.transform.position, Mathf.Infinity, grappleLayer);

            if (hit.collider != null && Vector3.Distance(hit.point, player.transform.position) <= grappleMaxPoint) {
                lineRenderer.SetPosition(0, hit.point);
                lineRenderer.SetPosition(1, player.transform.position);

                distanceJoint2D.connectedAnchor = hit.point;
                distanceJoint2D.enabled = true;
                lineRenderer.enabled = true;

                AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_shot", volume: 1f);
            } else {
                distanceJoint2D.enabled = false;
                lineRenderer.enabled = false;
            }
        }
    }
}