using System.Collections;
using System.Collections.Generic;
using AUDIO;
using PLAYER;
using UnityEngine;

namespace PLATFORMS
{
    public class ExplosivePlatform : Platform
    {
        [SerializeField] private float verticalKnockbackForce;
        private Rigidbody2D rigidBody2D;
        private void HandlePropel(GameObject player)
        {
            rigidBody2D = player.GetComponent<Rigidbody2D>();
            rigidBody2D.AddForce(Vector2.up * verticalKnockbackForce, ForceMode2D.Impulse);
        }
        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            HandlePropel(collision.gameObject);
        }
    }
}