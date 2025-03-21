using AUDIO;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace PLAYER {
    [Serializable]
    public class PlayerGrappler {
        private GameObject player;

        private LineRenderer lineRenderer;
        private DistanceJoint2D distanceJoint2D;

        private LayerMask grapplerLayer;
        [SerializeField] private float grappleMaxPoint = 5f;
        [HideInInspector] public bool IsGrappleWithinMaxDistance { get; private set; } = true;
        [HideInInspector] public bool CanGrapple { get; private set; } = false;

        public void Initialize(GameObject player, LineRenderer lineRenderer, DistanceJoint2D distanceJoint2D, LayerMask grapplerLayer) {
            this.player = player;
            this.lineRenderer = lineRenderer;
            this.distanceJoint2D = distanceJoint2D;
            this.grapplerLayer = grapplerLayer;

            this.grappleMaxPoint = 5f;
            this.IsGrappleWithinMaxDistance = true;
            this.CanGrapple = false;

            lineRenderer.enabled = false;
            distanceJoint2D.enabled = false;
        }

        public void OnUpdate() {
            if (lineRenderer.enabled) lineRenderer.SetPosition(1, player.transform.position);
        }

        public void Grapple(InputAction.CallbackContext context) {
            if (context.started) {
                Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

                RaycastHit2D hit = Physics2D.Raycast(player.transform.position, mousePosition - (Vector2)player.transform.position, Mathf.Infinity, grapplerLayer);

                if (hit.collider != null) {
                    float distanceToGrapplePoint = Vector2.Distance(player.transform.position, hit.point);
                    if (distanceToGrapplePoint <= grappleMaxPoint) {
                        CanGrapple = true;
                        lineRenderer.SetPosition(0, hit.point);
                        lineRenderer.SetPosition(1, player.transform.position);

                        distanceJoint2D.connectedAnchor = hit.point;
                        distanceJoint2D.enabled = true;
                        lineRenderer.enabled = true;
                        AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_shot", volume: 1f);
                    } else {
                        CanGrapple = false;
                    }
                }
            } else if (context.canceled) {
                distanceJoint2D.enabled = false;
                lineRenderer.enabled = false;
                CanGrapple = false;
            }

            if (lineRenderer.enabled) lineRenderer.SetPosition(1, player.transform.position);
        }
    }
}