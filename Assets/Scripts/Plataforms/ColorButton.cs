using PLAYER;
using Unity.VisualScripting;
using UnityEngine;


namespace PLATFORMS {
    public class ColorButton : MonoBehaviour {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] ColorPlatform RedPlatform;
        [SerializeField] ColorPlatform BluePlatform;
        private bool changed = true;
        [SerializeField] private Sprite redass;
        [SerializeField] private Sprite blueass;
        private SpriteRenderer selfspriteshit;


        private void Start() {
            selfspriteshit = GetComponent<SpriteRenderer>();
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
            }
            else {
                selfspriteshit.sprite = redass;
            }

        }

    }
}