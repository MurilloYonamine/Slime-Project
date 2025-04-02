using PLAYER;
using System.Collections;
using UnityEngine;

public class Wheat : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player)) {
            StartCoroutine(ReleasePlayer(player, 2f));
        }
    }
    private Vector3 PlayerDirection(Vector3 playerPosition) {
        return playerPosition.x > transform.position.x ? new Vector3(-2, 0, 0) : new Vector3(2, 0, 0);
    }
    private IEnumerator ReleasePlayer(PlayerController player, float timer) {
        Vector3 playerOriginalPosition = player.gameObject.transform.position;
        player.gameObject.transform.position = this.transform.position;
        player.StartChangeSpeed(0, timer);
        yield return new WaitForSeconds(timer);
        player.gameObject.transform.position = this.transform.position + PlayerDirection(playerOriginalPosition);
    }
}
