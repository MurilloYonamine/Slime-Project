using ENEMY;
using UnityEngine;

namespace PLAYER {
    public class PlayerBullet : MonoBehaviour {
        [HideInInspector] public float bulletDamage = 1f;
        [HideInInspector] public GameObject hitEffect;
        [HideInInspector] public GameObject player;

        private void OnTriggerEnter2D(Collider2D collider2D) {
            if (collider2D.TryGetComponent<Alface>(out Alface alface)) {
                Debug.Log("Tiro entrou na área de explosão");
                Physics2D.IgnoreCollision(collider2D, alface.explosionArea);

                CreateImpactEffect();
            }

            if (collider2D.TryGetComponent<Enemy>(out Enemy enemy)) {
                if (enemy.player == null) enemy.player = player;
                enemy.TakeDamage(bulletDamage);
                CameraManager.Instance.ShakeCamera(5f, 0.1f);

                Destroy(gameObject);
                return;
            }

            if (!collider2D.CompareTag("Player") && hitEffect != null) {
                CreateImpactEffect();
            }
        }
        private void CreateImpactEffect() {
            GameObject impact = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(impact, 1f);
            Destroy(gameObject);
        }
    }
}
