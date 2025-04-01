using UnityEngine;

namespace MAGIC_PLANTS.FIG_TREE {
    public class MagicSeed : MonoBehaviour {
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Ground")) {
                return;
            }

            if (collision.gameObject.CompareTag("Player")) {
                GameManager.Instance.magicSeedCount++;
                Destroy(gameObject);
            }
        }
    }
}