using UnityEngine;

namespace PLATFORMS {
    public class SpikePlatform : Platform, IResettablePlatform {
        [SerializeField] private GameObject[] spikes;

        public void ResetPlatform() {
            transform.position = initialPosition;
            NextPosition = pointA != null ? pointA.position : initialPosition;
        }
    }
}