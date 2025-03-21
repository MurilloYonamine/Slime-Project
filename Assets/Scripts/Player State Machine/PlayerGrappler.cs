using AUDIO;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
namespace PLAYER {
    [Serializable]
    public class PlayerGrappler {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private DistanceJoint2D distanceJoint2D;
        [SerializeField] private LayerMask grappleLayer;
        [SerializeField] private float grappleMaxPoint = 5f;
        [field: SerializeField] public bool IsGrappleWithinMaxDistance { get; private set; } = true;
        [field: SerializeField] public bool CanGrapple { get; private set; } = false;

        private Color originalColor;

        public void Initialize() {
            lineRenderer.enabled = false;
            distanceJoint2D.enabled = false;
        }

        public void Grapple(GameObject gameObject) {
            Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, mousePosition -
                (Vector2)gameObject.transform.position, Mathf.Infinity, grappleLayer);

            if (Vector3.Distance(hit.point, gameObject.transform.position) > grappleMaxPoint) return;

            if (hit.collider != null) {
                lineRenderer.SetPosition(0, hit.point);
                lineRenderer.SetPosition(1, gameObject.transform.position);

                distanceJoint2D.connectedAnchor = hit.point;
                distanceJoint2D.enabled = true;
                lineRenderer.enabled = true;
                AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_shot", volume: 1f);
            }
            if (lineRenderer.enabled) lineRenderer.SetPosition(1, gameObject.transform.position);

        }
    }
}
