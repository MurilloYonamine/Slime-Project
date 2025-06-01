using PLAYER;
using UnityEngine;

namespace PLATFORMS {
    public class ColorPlatform : Platform, IResettablePlatform {

        [SerializeField] public bool isactive = false;
        public float oldspeed;

        private void Awake() {
            if (!isactive) {
                oldspeed = moveSpeed;
                moveSpeed = 0;
            } else {
                oldspeed = moveSpeed;
            }
        }

        protected override void Start() {
            base.Start();
            ResettablePlatformRegistry.All.Add(this);
        }


        private void OnDestroy() {
            ResettablePlatformRegistry.All.Remove(this);
        }

        public void ResetPlatform() {
            isactive = true;
            moveSpeed = oldspeed;
            transform.position = initialPosition;
            NextPosition = pointA != null ? pointA.position : initialPosition;
        }

        public void CHANGE() {
            isactive = !isactive;
            if (!isactive) {
                moveSpeed = 0;
            } else {
                moveSpeed = oldspeed;
            }
        }
    }
}