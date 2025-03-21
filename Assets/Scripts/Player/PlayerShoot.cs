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

        [SerializeField] private float bulletDamage = 1f;
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private float bulletDestroyTimer = 2f;

        public void Initialize(GameObject player, PlayerHealth playerHealth, GameObject bulletPrefab, RectTransform aimPrefab) {
            this.player = player;
            this.playerHealth = playerHealth;
            this.bulletPrefab = bulletPrefab;
            this.aimPrefab = aimPrefab;

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

                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                mousePosition.z = player.transform.position.z;

                Vector3 shootDirection = (mousePosition - player.transform.position).normalized;

                GameObject bullet = GameObject.Instantiate(bulletPrefab, player.transform.position, Quaternion.identity);

                bullet.AddComponent<Bullet>();
                bullet.GetComponent<Bullet>().bulletDamage = bulletDamage;
                bullet.GetComponent<Bullet>().hitEffect = hitEffect;
                bullet.GetComponent<Bullet>().player = player;

                bullet.GetComponent<Rigidbody2D>().linearVelocity = shootDirection * bulletSpeed;

                AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_shot", volume: 1f, pitch: 1.5f);

                GameObject.Destroy(bullet, bulletDestroyTimer);
            }
        }

    }
}
