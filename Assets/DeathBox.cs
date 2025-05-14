using PLAYER;
using UnityEngine;

namespace DEATHBOX {
    public class DeathBox : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<PlayerController>(out PlayerController player)) {

            }
        }
    }
}