using PLAYER;
using Unity.VisualScripting;
using UnityEngine;


namespace PLATFORMS {
    public class ColorButton : MonoBehaviour {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] ColorPlatform RedPlatform;
        [SerializeField] ColorPlatform BluePlatform;
        private void OnTriggerEnter2D(Collider2D collider2D) {

            if (collider2D.TryGetComponent<PlayerBullet>(out PlayerBullet bullet)) {
                RedPlatform.CHANGE();
                BluePlatform.CHANGE();
    
            }
        }



    }
}