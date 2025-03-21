using AUDIO;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace PLAYER
{
    [Serializable]
    public class PlayerShoot
    {
        public PlayerHealth playerHealth;
        
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed = 50f;
        [SerializeField] private Camera mainCamera;
        public void Shoot(PlayerController player)
        {
            
            if (playerHealth.Health > 1)
            {
                playerHealth.Health -= 1;

                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                mousePosition.z = player.transform.position.z;

                Vector3 shootDirection = (mousePosition - player.transform.position).normalized;

                GameObject bullet = GameObject.Instantiate(bulletPrefab, player.transform.position, Quaternion.identity);

                bullet.GetComponent<Rigidbody2D>().linearVelocity = shootDirection * bulletSpeed;

                AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_shot", volume: 1f, pitch: 1.5f);
            }
        }

    }
}
