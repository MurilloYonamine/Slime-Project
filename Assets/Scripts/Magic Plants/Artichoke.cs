using System.Collections;
using UnityEngine;

namespace MagicPlants {
    public class Artichoke : MonoBehaviour {
        [SerializeField] private Transform[] spikes;
        private Vector3[] originalScales;

        [SerializeField] private float expansionSize = 2f;
        [SerializeField] private float expansionSpeed = 1f;

        [SerializeField] private float damage = 10f;

        private void Awake() {
            originalScales = new Vector3[spikes.Length];
            for (int i = 0; i < spikes.Length; i++) {
                originalScales[i] = spikes[i].localScale;
                var spikeCollision = spikes[i].gameObject.AddComponent<SpikeCollision>();
                spikeCollision.Initialize(this, damage);
            }
        }

        public void ExpandSpikes() {
            StopAllCoroutines();
            StartCoroutine(ExpandSpikesCoroutine());
        }

        public void ResetSpikes() {
            StopAllCoroutines();
            StartCoroutine(ResetSpikesCoroutine());
        }

        private IEnumerator ExpandSpikesCoroutine() {
            float elapsedTime = 0f;
            while (elapsedTime < expansionSpeed) {
                for (int i = 0; i < spikes.Length; i++) {
                    spikes[i].localScale = Vector3.Lerp(originalScales[i], new Vector3(originalScales[i].x, expansionSize, originalScales[i].z), elapsedTime / expansionSpeed);
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            for (int i = 0; i < spikes.Length; i++) {
                spikes[i].localScale = new Vector3(originalScales[i].x, expansionSize, originalScales[i].z);
            }
        }

        private IEnumerator ResetSpikesCoroutine() {
            float elapsedTime = 0f;
            while (elapsedTime < expansionSpeed) {
                for (int i = 0; i < spikes.Length; i++) {
                    spikes[i].localScale = Vector3.Lerp(new Vector3(originalScales[i].x, expansionSize, originalScales[i].z), originalScales[i], elapsedTime / expansionSpeed);
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            for (int i = 0; i < spikes.Length; i++) {
                spikes[i].localScale = originalScales[i];
            }
        }
    }
}