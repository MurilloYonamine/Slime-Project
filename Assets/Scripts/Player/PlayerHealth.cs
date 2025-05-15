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

        public int currentHealth;
        public int maxHealth;

        [SerializeField] private float verticalKnockbackForce = 5f;
        [SerializeField] private float horizontalKnockbackForce = 2500f;

        public void Initialize(PlayerController player, Rigidbody2D rigidBody2D) {
            this.player = player;
            this.rigidBody2D = rigidBody2D;

            maxHealth = GameManager.Instance.GetLifeSize();
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage, MonoBehaviour hostile) {
            currentHealth -= damage;
            if (currentHealth <= 0) {
                GameManager.Instance.RespawnPlayer();
            }

            SizeChange();
            HandleKnockBack(hostile);
        }

        public void HandleHealing() {
            currentHealth = maxHealth;
            Debug.Log("Player healed to max health: " + currentHealth);
            SizeChange();
        }

        public void HandleKnockBack(GameObject hostile) {
            player.IsTakingDamage = true;

            rigidBody2D.AddForce(Vector2.up * verticalKnockbackForce, ForceMode2D.Impulse);

            if (player.transform.position.x < hostile.transform.position.x)
                rigidBody2D.AddForce(Vector2.left * horizontalKnockbackForce, ForceMode2D.Force);
            else
                rigidBody2D.AddForce(Vector2.right * horizontalKnockbackForce, ForceMode2D.Force);

        }

        public void HandleKnockBack(MonoBehaviour hostile) {
            player.IsTakingDamage = true;

            rigidBody2D.AddForce(Vector2.up * verticalKnockbackForce, ForceMode2D.Impulse);

            if (player.transform.position.x < hostile.transform.position.x)
                rigidBody2D.AddForce(Vector2.left * horizontalKnockbackForce, ForceMode2D.Force);
            else
                rigidBody2D.AddForce(Vector2.right * horizontalKnockbackForce, ForceMode2D.Force);

        }

        public void SizeChange() {
            Debug.Log($"Current Health: {currentHealth}, Max Health: {maxHealth}");

            switch (currentHealth) {
                case 0:
                    GameManager.Instance.ChangeLifeHUD(currentHealth);
                    player.transform.localScale = new Vector2(0.5f, 0.5f);
                    player.cursize = PlayerController.CURSIZE.small;
                    player.OnChangeSpeed(25f);
                    break;
                case 1:
                    GameManager.Instance.ChangeLifeHUD(currentHealth);
                    player.transform.localScale = new Vector2(0.60f, 0.60f);
                    player.cursize = PlayerController.CURSIZE.small;
                    player.OnChangeSpeed(20f);
                    break;
                case 2:
                    GameManager.Instance.ChangeLifeHUD(currentHealth);
                    player.transform.localScale = new Vector2(0.75f, 0.75f);
                    player.cursize = PlayerController.CURSIZE.normal;
                    player.OnChangeSpeed(15f);
                    break;
                case 3:
                    GameManager.Instance.ChangeLifeHUD(currentHealth);
                    player.transform.localScale = new Vector2(1f, 1f);
                    player.cursize = PlayerController.CURSIZE.normal;
                    player.OnResetSpeed();
                    break;
            }
        }
    }
}
