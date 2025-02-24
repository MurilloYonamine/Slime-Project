using ENEMY;
using UnityEngine;

namespace PLAYER {
    public class Bullet : MonoBehaviour {
        [SerializeField] private float bulletDamage = 1f;
        
        private void OnTriggerEnter2D(Collider2D other) {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy) {
                other.GetComponent<Enemy>().TakeDamage(bulletDamage);
                Destroy(gameObject);
            }
        }
    }
}