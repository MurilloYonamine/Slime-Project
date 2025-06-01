using UnityEngine;

namespace PLATFORMS {
    public class SpikePlatform : Platform, IResettablePlatform {
        [SerializeField] private GameObject[] spikes;

        public void ResetPlatform() {
            if (pointA != null) {
                transform.position = pointA.position;
                NextPosition = pointB.position;
            }
        }
    }
}