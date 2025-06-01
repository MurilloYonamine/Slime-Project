using PLAYER;
using UnityEngine;

namespace PLATFORMS {
    public class ColorButton : MonoBehaviour, IResettablePlatform {
        [SerializeField] ColorPlatform RedPlatform;
        [SerializeField] ColorPlatform BluePlatform;
        private bool changed = true;
        [SerializeField] private Sprite redass;
        [SerializeField] private Sprite blueass;
        private SpriteRenderer selfspriteshit;

        private void Start() {
            selfspriteshit = GetComponent<SpriteRenderer>();
            ResetPlatform();
        }

        private void OnTriggerEnter2D(Collider2D collider2D) {
            if (collider2D.TryGetComponent<PlayerBullet>(out PlayerBullet bullet)) {
                RedPlatform.CHANGE();
                BluePlatform.CHANGE();
                CHANGE();
            }
        }

        private void CHANGE() {
            changed = !changed;
            if (changed) {
                selfspriteshit.sprite = blueass;
            } else {
                selfspriteshit.sprite = redass;
            }
        }

        public void ResetPlatform() {
            if (selfspriteshit == null)
                selfspriteshit = GetComponent<SpriteRenderer>();

            bool useBlue = BluePlatform != null;

            changed = useBlue;
            selfspriteshit.sprite = useBlue ? blueass : redass;

            if (useBlue) {
                if (BluePlatform != null) {
                    BluePlatform.isactive = true;
                    BluePlatform.moveSpeed = BluePlatform.oldspeed;
                }
                if (RedPlatform != null) {
                    RedPlatform.isactive = false;
                    RedPlatform.moveSpeed = 0;
                }
            } else {
                if (RedPlatform != null) {
                    RedPlatform.isactive = true;
                    RedPlatform.moveSpeed = RedPlatform.oldspeed;
                }
                if (BluePlatform != null) {
                    BluePlatform.isactive = false;
                    BluePlatform.moveSpeed = 0;
                }
            }
        }
    }
}
