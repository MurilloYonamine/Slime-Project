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
                currentHealth = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            SizeChange();
            HandleKnockBack(hostile);
        }

        public void HEALME(int heal) {
            currentHealth += heal;

            SizeChange();
            if (currentHealth > maxHealth) currentHealth = maxHealth;
        }

        public void HandleKnockBack(GameObject hostile) {
            player.IsTakingDamage = true;

            rigidBody2D.AddForce(Vector2.up * verticalKnockbackForce, ForceMode2D.Impulse);

            if (player.transform.position.x < hostile.transform.position.x) {
                rigidBody2D.AddForce(Vector2.left * horizontalKnockbackForce, ForceMode2D.Force);
            } else {
                rigidBody2D.AddForce(Vector2.right * horizontalKnockbackForce, ForceMode2D.Force);
            }
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
            //if (Health > 25 && player.cursize == PlayerController.CURSIZE.small && player.curstretch == PlayerController.CURSTRECH.steched) {
            //    player.transform.localScale = new Vector2(1f, 1f);
            //    player.cursize = PlayerController.CURSIZE.normal;
            //} else if (Health < 25 && player.cursize == PlayerController.CURSIZE.normal && player.curstretch == PlayerController.CURSTRECH.steched) {
            //    player.transform.localScale = new Vector2(0.50f, 0.50f);
            //    player.cursize = PlayerController.CURSIZE.small;
            //} else if (Health > 25 && player.cursize == PlayerController.CURSIZE.small && player.curstretch == PlayerController.CURSTRECH.normal) {
            //    player.transform.localScale = new Vector2(4f, 0.5f);
            //    player.cursize = PlayerController.CURSIZE.normal;
            //} else if (Health < 25 && player.cursize == PlayerController.CURSIZE.normal && player.curstretch == PlayerController.CURSTRECH.normal) {
            //    player.transform.localScale = new Vector2(2f, 0.25f);
            //    player.cursize = PlayerController.CURSIZE.small;
            //}
            switch (currentHealth) {
                case 0:
                    GameManager.Instance.ChangeLifeHUD(currentHealth);
                    player.transform.localScale = new Vector2(0.25f, 0.25f);
                    player.cursize = PlayerController.CURSIZE.small;
                    player.OnChangeSpeed(1.5f);
                    break;
                case 1:
                    GameManager.Instance.ChangeLifeHUD(currentHealth);
                    player.transform.localScale = new Vector2(0.50f, 0.50f);
                    player.cursize = PlayerController.CURSIZE.small;
                    player.OnChangeSpeed(1.4f);
                    break;
                case 2:
                    GameManager.Instance.ChangeLifeHUD(currentHealth);
                    player.transform.localScale = new Vector2(0.75f, 0.75f);
                    player.cursize = PlayerController.CURSIZE.normal;
                    player.OnChangeSpeed(1.25f);
                    break;
                case 3:
                    GameManager.Instance.ChangeLifeHUD(currentHealth);
                    player.transform.localScale = new Vector2(1f, 1f);
                    player.cursize = PlayerController.CURSIZE.normal;
                    player.OnChangeSpeed(1f);
                    break;
            }
        }
    }
}
