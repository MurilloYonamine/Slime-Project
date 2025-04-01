using ENEMY;
using MagicPlants;
using UnityEngine;

namespace PLAYER {
    public class PlayerBullet : MonoBehaviour {
        [HideInInspector] public float bulletDamage = 1f;
        [HideInInspector] public GameObject hitEffect;
        [HideInInspector] public GameObject player;

        private void OnTriggerEnter2D(Collider2D collider2D) {
            if (collider2D.TryGetComponent<Lettuce>(out Lettuce lettuce)) {
                Debug.Log("Tiro entrou na área de explosão");
                Physics2D.IgnoreCollision(collider2D, lettuce.explosionArea);

                CreateImpactEffect();
            }

            if (collider2D.TryGetComponent<Enemy>(out Enemy enemy)) {
                if (enemy.player == null) enemy.player = player;
                enemy.TakeDamage(bulletDamage);
                CameraManager.Instance.ShakeCamera(5f, 0.1f);

                Destroy(gameObject);
                return;
            }

            if(collider2D.TryGetComponent<FigTree>(out FigTree figTree)) {
                figTree.DropMagicSeed();
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
