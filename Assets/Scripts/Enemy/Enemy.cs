using System.Collections;
using PLAYER;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ENEMY
{
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Components")]
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidBody2D;
        private Color originalColor;

        [Header("Enemy Settings")]
        [SerializeField] private float maxHealth = 3f;
        [SerializeField] private float knockbackForce = 25f;
        private float currentHealth;

        [Header("Camera Shake Settings")]
        [SerializeField] private CameraShake cameraShake;
        [SerializeField] private float shakeDuration = 0.1f;
        [SerializeField] private float shakeMagnitude = 0.1f;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidBody2D = GetComponent<Rigidbody2D>();

            cameraShake = FindObjectOfType<CameraShake>();

            currentHealth = maxHealth;
            originalColor = spriteRenderer.color;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;

            StartCoroutine(FlashWhite());

            if (currentHealth <= 0) Die();

            Vector3 knockbackDirection = (transform.position - GameManager.Instance.player.transform.position).normalized;
            rigidBody2D.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Force);
            StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
        }

        private IEnumerator FlashWhite()
        {
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = originalColor;
        }

        private void Die() => Destroy(gameObject);
    }
}
