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

            if (!CheatManager.Instance.invunerable) currentHealth -= damage;

            if (currentHealth <= 0) GameManager.Instance.RespawnPlayer();
            

            SizeChange();
            HandleKnockBack(hostile);
        }

        public void HandleHealing() {
            currentHealth = maxHealth;
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
            switch (currentHealth) {
                case 0:
                    GameManager.Instance.ChangeLifeHUD(currentHealth);
                    player.transform.localScale = new Vector3(0.8f, 0.8f);
                    player.cursize = PlayerController.CURSIZE.small;
                    player.OnChangeSpeed(10f);
                    break;
                case 1:
                    GameManager.Instance.ChangeLifeHUD(currentHealth);
                    player.transform.localScale = new Vector3(1f, 1f);
                    player.cursize = PlayerController.CURSIZE.small;
                    player.OnChangeSpeed(15f);
                    break;
                case 2:
                    GameManager.Instance.ChangeLifeHUD(currentHealth);
                    player.transform.localScale = new Vector3(1.25f, 1.25f);
                    player.cursize = PlayerController.CURSIZE.normal;
                    player.OnChangeSpeed(10f);
                    break;
                case 3:
                    GameManager.Instance.ChangeLifeHUD(currentHealth);
                    player.transform.localScale = new Vector3(1.5f, 1.5f);
                    player.cursize = PlayerController.CURSIZE.normal;
                    player.OnResetSpeed();
                    break;
            }
        }
    }
}
