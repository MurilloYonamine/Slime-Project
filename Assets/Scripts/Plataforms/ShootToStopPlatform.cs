using PLATFORMS;
using PLAYER;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace PLATFORMS {
    public class ShootToStopPlatform : Platform, IResettablePlatform {

        public bool ison = true;
        [HideInInspector] public float oldspeed;

        protected override void Start() {
            base.Start();
            oldspeed = moveSpeed;
            ResettablePlatformRegistry.All.Add(this);
        }

        private void OnDestroy() {
            ResettablePlatformRegistry.All.Remove(this);
        }

        public void ResetPlatform() {
            ison = true;
            moveSpeed = oldspeed;
            if (pointA != null) {
                transform.position = pointA.position;
                NextPosition = pointB.position;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider2D) {

            if (collider2D.TryGetComponent<PlayerBullet>(out PlayerBullet bullet)) {
                CHANGE();
            }
        }

        private void CHANGE() {
            ison = !ison;
            moveSpeed = ison ? oldspeed : 0;
        }

        public static void ResetAllPlatforms() {
            foreach (var platform in ResettablePlatformRegistry.All.ToList()) {
                if (platform != null && (platform as MonoBehaviour) != null)
                    platform.ResetPlatform();
            }
        }
    }
}