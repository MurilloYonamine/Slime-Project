using AUDIO;
using ENEMY;
using PLAYER;
using System.Collections;
using UnityEngine;

namespace MAGIC_PLANTS.LETTUCE {
    public class Lettuce : MonoBehaviour {
        public CircleCollider2D explosionArea;
        public CircleCollider2D alfaceCircleCollider2D;
        public GameObject explosionEffect;
        private float explosionDuration;

        private void Start() { }
        private void Update() { }

        private void OnTriggerEnter2D(Collider2D collider2D) {
            if (collider2D.TryGetComponent<PlayerController>(out PlayerController player)) {

                GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                explosionDuration = effect.GetComponent<ParticleSystem>().main.duration;

                Destroy(effect, explosionDuration);

                AudioManager.Instance.PlaySoundEffect("Audio/SFX/Magic Plants/lettuce_explosion");
                CameraManager.Instance.ShakeCamera(15f, 0.1f);
                player.playerHealth.TakeDamage(10f, this);

                SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
                Color originalColor = spriteRenderer.color;
                StartCoroutine(FlashDamage(spriteRenderer, new Color(182f / 255f, 29f / 255f, 136f / 255f, 255f / 255f), originalColor));

                player.playerHealth.HandleKnockBack(this);
            }
        }
        private IEnumerator FlashDamage(SpriteRenderer spriteRenderer, Color damageColor, Color originalColor) {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = originalColor;

            Destroy(gameObject);
        }
        private void OnTriggerExit2D(Collider2D collider2D) {

        }
    }
}