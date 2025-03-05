using ENEMY;
using UnityEngine;

namespace PLAYER {
    public class Bullet : MonoBehaviour {
        [SerializeField] private float bulletDamage = 1f;
        [SerializeField] private GameObject hitEffect;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<Enemy>(out Enemy enemy)) {
                enemy.TakeDamage(bulletDamage);
                CameraManager.Instance.ShakeCamera(5f, 0.1f);

                Destroy(gameObject);
            }

            if (!other.CompareTag("Player") && hitEffect != null) {
                GameObject impact = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(impact, 1f);
            }
        }
    }
}
