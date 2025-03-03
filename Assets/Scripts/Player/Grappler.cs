using AUDIO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PLAYER {
    public class Grappler : MonoBehaviour {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private DistanceJoint2D distanceJoint2D;
        [SerializeField] private LayerMask grappleLayer;
        public bool canGrapple = true;

        private void Start() {
            lineRenderer.enabled = false;
            distanceJoint2D.enabled = false;
        }

        private void Update() {
            if (lineRenderer.enabled) {
                lineRenderer.SetPosition(1, transform.position);
            }
        }

        public void Grapple(InputAction.CallbackContext context) {
            if (context.started) {
                Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePosition -
                    (Vector2)transform.position, Mathf.Infinity, grappleLayer);

                if (hit.collider != null) {
                    lineRenderer.SetPosition(0, hit.point);
                    lineRenderer.SetPosition(1, transform.position);

                    distanceJoint2D.connectedAnchor = hit.point;
                    distanceJoint2D.enabled = true;
                    lineRenderer.enabled = true;
                    AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_shot", volume: 1f);
                }
            } else if (context.canceled) {
                distanceJoint2D.enabled = false;
                lineRenderer.enabled = false;
            }

            if (lineRenderer.enabled) lineRenderer.SetPosition(1, transform.position);
        }

    }
}
