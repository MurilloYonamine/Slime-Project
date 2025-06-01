using PLAYER;
using UnityEngine;

namespace PLATFORMS {
    public class ColorPlatform : Platform, IResettablePlatform {
        [SerializeField] public bool isactive = false;
        public float oldspeed;
        private void Awake() {
            oldspeed = moveSpeed;
            if (!isactive) {
                moveSpeed = 0;
            } else {
                moveSpeed = oldspeed;
            }
        }
        protected override void Start() {
            base.Start();
            initialPosition = transform.position; 
            NextPosition = pointA != null ? pointA.position : transform.position;
            ResettablePlatformRegistry.All.Add(this);
        }

        private void OnDestroy() {
            ResettablePlatformRegistry.All.Remove(this);
        }

        public void ResetPlatform() {
            transform.position = initialPosition;
            NextPosition = pointA != null ? pointA.position : initialPosition;
        }


        public void CHANGE() {
            isactive = !isactive;
            moveSpeed = isactive ? oldspeed : 0;
        }
    }
}