using PLAYER;
using Unity.VisualScripting;
using UnityEngine;


namespace PLATFORMS {
    public class ColorButton : MonoBehaviour {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] ColorPlatform RedPlatform;
        [SerializeField] ColorPlatform BluePlatform;
        [SerializeField] private Sprite Bluelook;
        [SerializeField] private Sprite Redlook;
        private SpriteRenderer butt;
        private bool isactive = true;

        private void Start() {
            butt = GetComponent<SpriteRenderer>();
        }
        private void OnTriggerEnter2D(Collider2D collider2D) {

            if (collider2D.TryGetComponent<PlayerBullet>(out PlayerBullet bullet)) {
                RedPlatform.CHANGE();
                BluePlatform.CHANGE();
                CHANGE();
            }


        }

        private void CHANGE() {
            isactive = !isactive;
            if (!isactive) {
                butt.sprite = Redlook;
            }
            else {
                butt.sprite = Bluelook;
            }
        }


    }
}