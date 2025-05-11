using System.Collections;
using UnityEngine;

namespace PLATFORMS {
    public class UnstablePlatform : Platform {
        [SerializeField] private float timeToDestroy = 2f;
        [SerializeField] private float timeToReset = 3f;

        private BoxCollider2D boxCollider;
        private PlatformEffector2D platformEffector2D;
        private SpriteRenderer spriteRenderer;

        protected override void Start() {
            base.Start();
            boxCollider = GetComponent<BoxCollider2D>();
            platformEffector2D = GetComponent<PlatformEffector2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        protected override void OnCollisionEnter2D(Collision2D collision) {
            base.OnCollisionEnter2D(collision);
            if (collision.gameObject.CompareTag("Player"))
                StartCoroutine(DisablePlatform());

        }
        protected override void OnCollisionExit2D(Collision2D collision) {
            base.OnCollisionExit2D(collision);
            //StopAllCoroutines();
            StartCoroutine(ResetPlatform());
        }
        private IEnumerator DisablePlatform() {
            StartCoroutine(FadeEffect(2f, true));
            yield return new WaitForSeconds(timeToDestroy);
            SetActiveComponents(false);
        }
        private IEnumerator ResetPlatform() {
            StartCoroutine(FadeEffect(2f, false));
            yield return new WaitForSeconds(timeToReset);
            SetActiveComponents(true);
        }
        private void SetActiveComponents(bool isActive) {
            boxCollider.enabled = isActive;
            platformEffector2D.enabled = isActive;
            spriteRenderer.enabled = isActive;
        }
        private IEnumerator FadeEffect(float time, bool fade) {
            float fadeDuration = time;
            float elapsedTime = 0f;
            float a = fade ? 1f : 0f;
            float b = fade ? 0f : 1f;

            Color originalColor = spriteRenderer.color;
            while (elapsedTime < fadeDuration) {
                spriteRenderer.enabled = true;
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(a, b, elapsedTime / fadeDuration);
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }
        }
    }
}