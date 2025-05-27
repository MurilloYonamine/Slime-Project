using PLAYER;
using UnityEngine;

namespace SYSTEM.CHECKPOINT {
    public class Checkpoint : MonoBehaviour {
        [SerializeField] private bool hasHealed = false;

        // Este método detecta quando o jogador entra no checkpoint, atualiza o checkpoint ativo e cura o jogador uma vez.
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                GameManager.Instance.ChangeCheckpoint(this.gameObject);
                if (!hasHealed) {
                    other.GetComponent<PlayerController>().UpdateHealth();
                    hasHealed = true;
                }
            }
        }
    }
}