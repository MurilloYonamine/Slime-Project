using System.Collections;
using PLAYER;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ENEMY
{
    public class Enemy : MonoBehaviour
    {

        private GameObject player;

        [Header("Enemy Components")]
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidBody2D;
        private Color originalColor;


        [Header("Enemy Settings")]
        [SerializeField] private float maxHealth = 3f;
        [SerializeField] private float knockbackForce = 25f;
        private float currentHealth;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidBody2D = GetComponent<Rigidbody2D>();

            player = GameObject.FindGameObjectWithTag("Player");

            currentHealth = maxHealth;
            originalColor = spriteRenderer.color;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;

            StartCoroutine(FlashWhite());

            if (currentHealth <= 0) Die();

            Vector3 knockbackDirection = (transform.position - player.transform.position).normalized;
            rigidBody2D.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Force);
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
