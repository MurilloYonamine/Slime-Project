using AUDIO;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace PLAYER {
    [Serializable]
    public class PlayerGrappler {
        private PlayerController player;

        private LineRenderer lineRenderer;
        private DistanceJoint2D distanceJoint2D;
        private Rigidbody2D rigidBody2D;
        private Animator animator;

        private LayerMask grapplerLayer;
        private LayerMask grapplerArea;
        [SerializeField] private float grappleMaxPoint = 5f;
        [HideInInspector] public bool IsGrappleWithinMaxDistance { get; private set; } = true;

        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite inAreaSprite;

        public void Initialize(PlayerController player, LineRenderer lineRenderer, DistanceJoint2D distanceJoint2D, LayerMask grapplerLayer, LayerMask grapplerArea, Rigidbody2D rigidBody2D, Animator animator) {
            this.player = player;
            this.lineRenderer = lineRenderer;
            this.distanceJoint2D = distanceJoint2D;
            this.grapplerLayer = grapplerLayer;
            this.grapplerArea = grapplerArea;
            this.rigidBody2D = rigidBody2D;
            this.animator = animator;

            lineRenderer.enabled = false;
            distanceJoint2D.enabled = false;

            GameManager.Instance.ChangeGrapplersDistance(grappleMaxPoint);
        }

        public void OnUpdate() {
            if (lineRenderer.enabled) lineRenderer.SetPosition(1, player.transform.position);
            if (player.IsClimbing) {
                distanceJoint2D.enabled = false;
                lineRenderer.enabled = false;
                player.IsGrappling = false;
            }
        }

        public void Grapple(InputAction.CallbackContext context) {
            if (context.started) {
                Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

                RaycastHit2D hit = Physics2D.Raycast(player.transform.position, mousePosition - (Vector2)player.transform.position, Mathf.Infinity, grapplerLayer);

                if (hit.collider != null) {
                    float distanceToGrapplePoint = Vector2.Distance(player.transform.position, hit.point);
                    if (distanceToGrapplePoint <= grappleMaxPoint) {
                        player.IsGrappling = true;

                        animator.SetBool("IsJumping", !player.IsGrappling);
                        animator.SetBool("IsWalking", !player.IsGrappling);
                        animator.SetBool("IsGrappling", player.IsGrappling);

                        lineRenderer.SetPosition(0, hit.point);
                        lineRenderer.SetPosition(1, player.transform.position);

                        distanceJoint2D.connectedAnchor = hit.point;

                        distanceJoint2D.enabled = true;
                        lineRenderer.enabled = true;
                        player.IsJumping = false;

                        AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_shot", volume: 1f);
                    }
                }
            }
            else if (context.canceled) {
                distanceJoint2D.enabled = false;
                lineRenderer.enabled = false;
                player.IsGrappling = false;
                animator.SetBool("IsGrappling", player.IsGrappling);

            }

            if (lineRenderer.enabled) lineRenderer.SetPosition(1, player.transform.position);
        }
        public void TriggerEnter2D(Collider2D collider2D) {
            if (((1 << collider2D.gameObject.layer) & grapplerArea) != 0) {
                player.CanGrapple = true;

                if (collider2D.gameObject.GetComponentInParent<SpriteRenderer>() == null) return;

                collider2D.gameObject.GetComponentInParent<SpriteRenderer>().sprite = inAreaSprite;
            }
        }
        public void TriggerExit2D(Collider2D collider2D) {
            if (((1 << collider2D.gameObject.layer) & grapplerArea) != 0) {
                player.CanGrapple = false;

                if (collider2D.gameObject.GetComponentInParent<SpriteRenderer>() == null) return;

                collider2D.gameObject.GetComponentInParent<SpriteRenderer>().sprite = defaultSprite;
            }
        }
    }
}