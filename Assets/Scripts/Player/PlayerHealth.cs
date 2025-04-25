using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PLAYER {
    [Serializable]
    public class PlayerHealth {

        private PlayerController player;
        private Rigidbody2D rigidBody2D;
        public Image healthBar;

        public float Health;
        [HideInInspector] public float MaxHealth;

        [SerializeField] private float verticalKnockbackForce = 5f;
        [SerializeField] private float horizontalKnockbackForce = 2500f;

        public void Initialize(PlayerController player, Rigidbody2D rigidBody2D) {
            this.player = player;
            this.rigidBody2D = rigidBody2D;

            MaxHealth = Health;
        }

        public void OnUpdate() {
            healthBar.fillAmount = Mathf.Clamp(Health / MaxHealth, 0, 1);
        }

        public void TakeDamage(float damage, MonoBehaviour hostile) {
            Health -= damage;
            if (Health <= 0) {
                Health = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            SizeChange();
            HandleKnockBack(hostile);

        }

        public void HEALME(float heal) {
            Health += heal;

            SizeChange();
            if (Health > MaxHealth) Health = MaxHealth;
        }

        public void HandleKnockBack(MonoBehaviour hostile) {
            player.IsTakingDamage = true;

            rigidBody2D.AddForce(Vector2.up * verticalKnockbackForce, ForceMode2D.Impulse);

            if (player.transform.position.x < hostile.transform.position.x) {
                rigidBody2D.AddForce(Vector2.left * horizontalKnockbackForce, ForceMode2D.Force);
            } else {
                rigidBody2D.AddForce(Vector2.right * horizontalKnockbackForce, ForceMode2D.Force);
            }
        }

        public void SizeChange() {
            if (Health > 25 && player.cursize == PlayerController.CURSIZE.small && player.curstretch == PlayerController.CURSTRECH.steched) {
                player.transform.localScale = new Vector2(1f, 1f);
                player.cursize = PlayerController.CURSIZE.normal;
            } else if (Health < 25 && player.cursize == PlayerController.CURSIZE.normal && player.curstretch == PlayerController.CURSTRECH.steched) {
                player.transform.localScale = new Vector2(0.50f, 0.50f);
                player.cursize = PlayerController.CURSIZE.small;
            } else if (Health > 25 && player.cursize == PlayerController.CURSIZE.small && player.curstretch == PlayerController.CURSTRECH.normal) {
                player.transform.localScale = new Vector2(4f, 0.5f);
                player.cursize = PlayerController.CURSIZE.normal;
            } else if (Health < 25 && player.cursize == PlayerController.CURSIZE.normal && player.curstretch == PlayerController.CURSTRECH.normal) {
                player.transform.localScale = new Vector2(2f, 0.25f);
                player.cursize = PlayerController.CURSIZE.small;
            }
        }
    }
}
