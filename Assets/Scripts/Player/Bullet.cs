using ENEMY;
using Unity.VisualScripting;
using UnityEngine;

namespace PLAYER {
    public class Bullet : MonoBehaviour {
        [HideInInspector] public float bulletDamage = 1f;
        [HideInInspector] public GameObject hitEffect;
        [HideInInspector] public GameObject player;

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<Enemy>(out Enemy enemy)) {
                if (enemy.player == null) enemy.player = player;
                enemy.TakeDamage(bulletDamage);
                CameraManager.Instance.ShakeCamera(5f, 0.1f);

                Destroy(gameObject);
            }

            if (!other.CompareTag("Player") && hitEffect != null) {
                GameObject impact = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(impact, 1f);
                Destroy(gameObject);
            }
        }
    }
}
