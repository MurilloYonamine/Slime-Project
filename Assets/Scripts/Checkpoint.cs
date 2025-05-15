using PLAYER;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    [SerializeField] private bool hasHealed = false;
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