using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PLATFORMS {
    public class ExplosivePlatform : Platform {
        private float explosionDelay = 3f;
        [SerializeField] private List<GameObject> countdownIndicators;
        private int elapsedSeconds = 0;

        private IEnumerator CountdownToExplosion() {
            while (elapsedSeconds < explosionDelay) {
                if (elapsedSeconds < countdownIndicators.Count) {
                    countdownIndicators[elapsedSeconds].GetComponent<SpriteRenderer>().color = Color.black;
                }
                elapsedSeconds++;
                yield return new WaitForSeconds(1f);
            }
        }
        private void Explode() {
            Destroy(gameObject);
        }
        protected override void OnCollisionEnter2D(Collision2D collision) {
            base.OnCollisionEnter2D(collision);
            if (collision.gameObject.CompareTag("Player")) {
                StartCoroutine(CountdownToExplosion());
                Invoke("Explode", explosionDelay);
            }
        }
    }
}