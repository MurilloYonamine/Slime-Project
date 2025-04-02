using PLAYER;
using System.Collections;
using UnityEngine;

namespace MAGIC_PLANTS.ARTICHOKE {
    public class SpikeCollision : MonoBehaviour {
        private Artichoke artichoke;
        private float damage;
        private bool hasCollided = false;

        public void Initialize(Artichoke artichoke, float damage) {
            this.artichoke = artichoke;
            this.damage = damage;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (hasCollided) return;

            if (collision.TryGetComponent<PlayerController>(out PlayerController player)) {
                hasCollided = true;
                artichoke.ExpandSpikes();
                player.playerHealth.TakeDamage(damage, this);

                SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
                Color originalColor = spriteRenderer.color;
                StartCoroutine(FlashDamage(spriteRenderer, new Color(182f / 255f, 29f / 255f, 136f / 255f, 255f / 255f), originalColor));

                player.playerHealth.HandleKnockBack(this);
                player.DisableSpike();
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag("Player")) {
                hasCollided = false;
                artichoke.ResetSpikes();
            }
        }

        private IEnumerator FlashDamage(SpriteRenderer spriteRenderer, Color damageColor, Color originalColor) {
            if (hasCollided) yield return null;

            spriteRenderer.color = damageColor;
            yield return new WaitForSecondsRealtime(0.4f);
            spriteRenderer.color = originalColor;
        }
    }
}