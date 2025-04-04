using AUDIO;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace PLAYER
{
    [Serializable]
    public class PlayerShoot
    {
        private PlayerHealth playerHealth;
        private GameObject player;

        private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed = 50f;

        private Camera mainCamera;
        private RectTransform aimPrefab;
        private PlayerController playerController;

        [SerializeField] private float bulletDamage = 1f;
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private float bulletDestroyTimer = 2f;

        public void Initialize(GameObject player, PlayerHealth playerHealth, GameObject bulletPrefab, RectTransform aimPrefab, PlayerController playerController) {
            this.player = player;
            this.playerHealth = playerHealth;
            this.bulletPrefab = bulletPrefab;
            this.aimPrefab = aimPrefab;
            this.playerController = playerController;

            mainCamera = Camera.main;
        }

        public void OnUpdate() {
            aimPrefab.anchoredPosition = Mouse.current.position.value;
        }

        public void Shoot(InputAction.CallbackContext context)
        {
            
            if (context.started && playerHealth.Health > 1)
            {
                playerHealth.Health -= 1;
                 if (playerHealth.Health > 25){
                    playerController.transform.localScale = new Vector2(1f,1f);
                }
                else if (playerHealth.Health < 25){
                    playerController.transform.localScale = new Vector2(0.50f,0.50f);
                }
                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                mousePosition.z = player.transform.position.z;

                Vector3 shootDirection = (mousePosition - player.transform.position).normalized;

                GameObject bullet = GameObject.Instantiate(bulletPrefab, player.transform.position, Quaternion.identity);

                bullet.AddComponent<PlayerBullet>();
                bullet.GetComponent<PlayerBullet>().bulletDamage = bulletDamage;
                bullet.GetComponent<PlayerBullet>().hitEffect = hitEffect;
                bullet.GetComponent<PlayerBullet>().player = player;

                bullet.GetComponent<Rigidbody2D>().linearVelocity = shootDirection * bulletSpeed;

                AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_shot", volume: 1f, pitch: 1.5f);

                GameObject.Destroy(bullet, bulletDestroyTimer);
            }
        }

    }
}

