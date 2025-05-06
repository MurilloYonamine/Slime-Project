using PLAYER;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
namespace TRAPS {
    public class Spikes : MonoBehaviour {
        private LayerMask playerLayer;
        private void Start() => playerLayer = LayerMask.GetMask("Player");
        
        private void OnCollisionEnter2D(Collision2D collision) {
            if(((1 << collision.gameObject.layer) & playerLayer) != 0) {
                if(collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player)) {
                    player.playerHealth.TakeDamage(1, this);
                }
            }
        }
    }
}