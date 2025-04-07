using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PLAYER {
    [Serializable]



    public class PlayerHealth {

        public float Health;
        
        [HideInInspector] public float MaxHealth;

        private Rigidbody2D rigidBody2D;
        public Image healthBar;
        private PlayerController playerController;

        [SerializeField] private float verticalKnockbackForce = 5f;
        [SerializeField] private float horizontalKnockbackForce = 2500f;

        public void Initialize(Rigidbody2D rigidBody2D, PlayerController playerController) {
            MaxHealth = Health;

            this.rigidBody2D = rigidBody2D;
            this.playerController = playerController;
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

        public void HEALME(float heal){
            Health += heal;

            SizeChange();
            if (Health > MaxHealth){
                Health = MaxHealth;
            }
        }

        public void HandleKnockBack(MonoBehaviour hostile) {
            playerController.takingDamage = true;

            rigidBody2D.AddForce(Vector2.up * verticalKnockbackForce, ForceMode2D.Impulse);

            if (playerController.transform.position.x < hostile.transform.position.x) {
                rigidBody2D.AddForce(Vector2.left * horizontalKnockbackForce, ForceMode2D.Force);
            } else {
                rigidBody2D.AddForce(Vector2.right * horizontalKnockbackForce, ForceMode2D.Force);
            }
        }

        public void SizeChange(){
             if (Health > 25  && playerController.cursize == CURSIZE.small && playerController.curstretch == CURSTRECH.steched){
                playerController.transform.localScale = new Vector2(1f,1f);
                playerController.cursize = CURSIZE.normal;
                Debug.Log("was it me?");
            }
            else if (Health < 25 && playerController.cursize == CURSIZE.normal && playerController.curstretch == CURSTRECH.steched){
                playerController.transform.localScale = new Vector2(0.50f,0.50f);
                playerController.cursize = CURSIZE.small;
                Debug.Log("or me?");
            } 
            else if (Health > 25  && playerController.cursize == CURSIZE.small && playerController.curstretch == CURSTRECH.normal){
                playerController.transform.localScale = new Vector2(4f,0.5f);
                playerController.cursize = CURSIZE.normal;
                Debug.Log("maybe me?");
            } 
            else if(Health < 25 && playerController.cursize == CURSIZE.normal && playerController.curstretch == CURSTRECH.normal){
                playerController.transform.localScale = new Vector2(2f,0.25f);
                playerController.cursize = CURSIZE.small;
                Debug.Log("OPTION NUMBER 4");
            }
        }

    }
}
