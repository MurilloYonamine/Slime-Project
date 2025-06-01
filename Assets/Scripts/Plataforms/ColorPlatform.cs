using PLAYER;
using UnityEngine;

namespace PLATFORMS {
    public class ColorPlatform : Platform, IResettablePlatform {

        [SerializeField] public bool isactive = false;
        public float oldspeed;

        protected override void Start() {
            base.Start();
            oldspeed = moveSpeed;
            ResettablePlatformRegistry.All.Add(this);

            if (!isactive) {
                moveSpeed = 0;
            } else {
                moveSpeed = oldspeed;
            }
        }

        private void OnDestroy() {
            ResettablePlatformRegistry.All.Remove(this);
        }

        public void ResetPlatform() {
            isactive = true;
            moveSpeed = oldspeed;
            if (pointA != null) {
                transform.position = pointA.position;
                NextPosition = pointB.position;
            }
        }

        public void CHANGE() {
            isactive = !isactive;
            moveSpeed = isactive ? oldspeed : 0;
        }
    }
}