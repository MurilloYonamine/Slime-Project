using AUDIO;
using UnityEngine;
using UnityEngine.InputSystem;
namespace PLAYER
{
    public class PlayerShoot : MonoBehaviour
    {
        public PlayerHealth pHealth;
        
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed = 50f;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private RectTransform aimPrefab;

        private void Update() {
            aimPrefab.anchoredPosition = Mouse.current.position.value;
        }

        public void Shoot(InputAction.CallbackContext context)
        {
            
            if (context.started && pHealth.Health > 1)
            {
                pHealth.Health -= 1;

                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                mousePosition.z = transform.position.z;

                Vector3 shootDirection = (mousePosition - transform.position).normalized;

                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

                bullet.GetComponent<Rigidbody2D>().linearVelocity = shootDirection * bulletSpeed;

                AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_shot", volume: 1f, pitch: 1.5f);

                Destroy(bullet, 2f);
            }
        }

    }
}
