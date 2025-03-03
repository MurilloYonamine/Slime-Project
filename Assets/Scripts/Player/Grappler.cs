using AUDIO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace PLAYER {
    public class Grappler : MonoBehaviour {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private DistanceJoint2D distanceJoint2D;
        [SerializeField] private LayerMask grappleLayer;
        public bool canGrapple = true;

        [SerializeField] private Image playerAimImage;
        private Color originalColor;

        private void Start() {
            playerAimImage = GameManager.Instance.playerAim.GetComponent<Image>();
            originalColor = playerAimImage.color;

            lineRenderer.enabled = false;
            distanceJoint2D.enabled = false;
        }

        private void Update() {
            if (lineRenderer.enabled) lineRenderer.SetPosition(1, transform.position);

            DetectHover();
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
        private void DetectHover() {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0f, grappleLayer);

            if (hit.collider != null) {
               playerAimImage.color = Color.red;
            } else {
                ResetHoverEffect();
            }
        }

        private void ResetHoverEffect() {
            playerAimImage.color = originalColor;
        }
    }
}
