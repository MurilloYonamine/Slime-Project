using UnityEngine;
using UnityEngine.InputSystem;

namespace PLAYER
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed = 50f;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private RectTransform aimPrefab;

        private void Update()
        {
            aimPrefab.anchoredPosition = Mouse.current.position.value;

        }

        public void Shoot(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

                Vector3 shootDirection = (mousePosition - transform.position).normalized;

                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(shootDirection.x, shootDirection.y, shootDirection.z) * bulletSpeed;
                Destroy(bullet, 2f);
            }
        }
    }
}
