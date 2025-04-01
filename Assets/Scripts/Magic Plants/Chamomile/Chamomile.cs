using PLAYER;
using UnityEngine;

public class Chamomile : MonoBehaviour {
    [SerializeField] private float playerSpeedAfterCollision = 0f;
    [SerializeField] private float timeToNormalizePlayerSpeed = 5f;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player)) {
            player.StartChangeSpeed(playerSpeedAfterCollision, timeToNormalizePlayerSpeed);
            Destroy(gameObject);
        }
    }
}
