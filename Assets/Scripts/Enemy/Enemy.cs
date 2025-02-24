using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ENEMY {
    public class Enemy : MonoBehaviour {

        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidBody2D;

        [SerializeField] private float maxHealth = 3f;
        private float currentHealth;

        private Color originalColor;

        private void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidBody2D = GetComponent<Rigidbody2D>();

            currentHealth = maxHealth;
            originalColor = spriteRenderer.color;
        }

        public void TakeDamage(float damage) {
            currentHealth -= damage;
            StartCoroutine(FlashWhite());
            if (currentHealth <= 0) {
                Die();
            }
        }

        private IEnumerator FlashWhite() {
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = originalColor;
        }

        private void Die() {
            Destroy(gameObject);
        }

    }
}
