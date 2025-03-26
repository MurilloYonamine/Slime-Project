using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace PLAYER {
    [Serializable]
    public class PlayerHealth {

        public float Health;
        [HideInInspector] public float MaxHealth;
        private Rigidbody2D rigidBody2D;

        public Image healthBar;
        public void Initialize(Rigidbody2D rigidBody2D) {
            MaxHealth = Health;
            this.rigidBody2D = rigidBody2D;
        }
        public void OnUpdate() {
            healthBar.fillAmount = Mathf.Clamp(Health / MaxHealth, 0, 1);
        }
        public void TakeDamage(float damage) {
            Health -= damage;
            if (Health <= 0) {
                Health = 0;
                Debug.Log("Player morreu");
            }
        }
        public void PlayerKnockback(float directionX, float directionY, float force) {
            Debug.Log("Player foi arremessado diagonalmente");
            Vector2 direction = new Vector2(directionX, directionY).normalized;
            rigidBody2D.AddForce(direction * force);
        }
    }
}