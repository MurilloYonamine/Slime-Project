using System.Collections;
using PLAYER;
using UnityEngine;

namespace PLATFORMS {
    public class UnstablePlatform : Platform, IResettablePlatform {
        [SerializeField] private float timeToDestroy = 2f;
        [SerializeField] private float timeToReset = 3f;
        [SerializeField] private Animator _animator;
        [SerializeField] private bool disappearOnlyOnce = false;

        private BoxCollider2D boxCollider;
        private PlatformEffector2D platformEffector2D;
        private SpriteRenderer spriteRenderer;

        private bool animating = false;
        private bool alreadyUsed = false;

        protected override void Start() {
            base.Start();
            boxCollider = GetComponent<BoxCollider2D>();
            platformEffector2D = GetComponent<PlatformEffector2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void OnCollisionEnter2D(Collision2D collision) {
            base.OnCollisionEnter2D(collision);
            if (collision.gameObject.CompareTag("Player")) {
                if (!animating && (!disappearOnlyOnce || !alreadyUsed)) {
                    StartCoroutine(DisablePlatform());
                }
            }
        }

        public void ResetPlatform() {
            StopAllCoroutines();

            SetActiveComponents(true);

            if (disappearOnlyOnce && alreadyUsed) {
                animating = false;
                if (!boxCollider.enabled) boxCollider.enabled = true;
                if (_animator != null)
                    _animator.SetBool("Ondeath", false);

                transform.position = initialPosition;
                NextPosition = pointA != null ? pointA.position : initialPosition;

                if (spriteRenderer != null) {
                    var color = spriteRenderer.color;
                    color.a = 1f;
                    spriteRenderer.color = color;
                }
                return;
            }

            alreadyUsed = false;

            if (_animator != null)
                _animator.SetBool("Ondeath", false);

            animating = false;

            transform.position = initialPosition;
            NextPosition = pointA != null ? pointA.position : initialPosition;

            if (spriteRenderer != null) {
                var color = spriteRenderer.color;
                color.a = 1f;
                spriteRenderer.color = color;
            }
        }

        private IEnumerator DisablePlatform() {
            animating = true;

            if (disappearOnlyOnce) {
                alreadyUsed = true; 
            }

            if (_animator != null)
                _animator.SetBool("Ondeath", true);

            yield return new WaitForSeconds(0.5f);

            yield return StartCoroutine(FadeEffect(0.5f, true));

            SetActiveComponents(false);

            yield return new WaitForSeconds(timeToReset);

            ResetPlatform();

            yield return StartCoroutine(FadeEffect(0.5f, false));

            animating = false;
        }

        private void SetActiveComponents(bool isActive) {
            if (boxCollider != null) boxCollider.enabled = isActive;
            if (platformEffector2D != null) platformEffector2D.enabled = isActive;
            if (spriteRenderer != null) spriteRenderer.enabled = isActive;
        }

        private IEnumerator FadeEffect(float time, bool fade) {
            float elapsedTime = 0f;
            float a = fade ? 1f : 0f;
            float b = fade ? 0f : 1f;

            Color originalColor = spriteRenderer.color;
            while (elapsedTime < time) {
                spriteRenderer.enabled = true;
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(a, b, elapsedTime / time);
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }
        }
    }
}
