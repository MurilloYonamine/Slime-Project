using System.Collections;
using System.Collections.Generic;
using AUDIO;
using PLAYER;
using UnityEngine;

namespace PLATFORMS {
    public class LaunchPlataform : Platform {
        [SerializeField] private float verticalKnockbackForce = 30f;
        private Rigidbody2D rigidBody2D;
        PlayerController playerController;

        private void HandlePropel(GameObject player) {
            playerController = player.GetComponent<PlayerController>();
            playerController.IsJumping = true;
            rigidBody2D = player.GetComponent<Rigidbody2D>();
            rigidBody2D.AddForce(Vector2.up * verticalKnockbackForce, ForceMode2D.Impulse);
        }
        protected override void OnCollisionEnter2D(Collision2D collision) {
            base.OnCollisionEnter2D(collision);
            HandlePropel(collision.gameObject);
        }
    }
}