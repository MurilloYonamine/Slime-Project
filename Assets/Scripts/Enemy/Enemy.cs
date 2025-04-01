using System.Collections;
using AUDIO;
using PLAYER;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ENEMY {
    public class Enemy : MonoBehaviour {

        [SerializeField] private LayerMask groundLayer;

        [Header("Enemy Components")]
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidBody2D;
        private Color originalColor;

        [Header("Enemy Settings")]
        [SerializeField] private float maxHealth = 3f;
        [SerializeField] private float knockbackForce = 25f;
        private float currentHealth;

        [HideInInspector] public GameObject player;

        private void Start() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidBody2D = GetComponent<Rigidbody2D>();

            currentHealth = maxHealth;
            originalColor = spriteRenderer.color;
        }

        public void TakeDamage(float damage) {
            currentHealth -= damage;

            StartCoroutine(FlashWhite());
            AudioManager.Instance.PlaySoundEffect("Audio/SFX/Enemy/hit_damage", volume: 1f);

            if (currentHealth <= 0) Die();

            if (player != null) {
                Vector3 knockbackDirection = (transform.position - player.transform.position).normalized;
                rigidBody2D.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }

        private IEnumerator FlashWhite() {
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = originalColor;
        }

        private void Die() => Destroy(gameObject);

        private void OnCollisionEnter2D(Collision2D collision2D) {
            if (!(groundLayer == (groundLayer | (1 << collision2D.gameObject.layer)))) {
                Debug.Log("XERECA");
            }
        }
    }
}
