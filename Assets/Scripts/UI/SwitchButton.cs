using System;
using UnityEngine;
using UnityEngine.UI;

namespace MENU.SETTINGS {
    [Serializable]
    public class SwitchButton {
        [SerializeField] public Button button;

        [SerializeField] private Sprite on;
        [SerializeField] private Sprite off;

        [SerializeField] private Sprite currentSprite;

        public void Initialize() {
            bool isFullscreen = Screen.fullScreen;

            currentSprite = isFullscreen ? on : off;
            button.image.sprite = currentSprite;
        }

        public void SetCurrentSprite() {
            button.image.sprite = currentSprite;
        }

        public void TaskOnClick() {
            if (currentSprite == on) {
                currentSprite = off;
                button.image.sprite = off;
            } else {
                currentSprite = on;
                button.image.sprite = on;
            }
        }

        public void SetFullscreenSprite(bool isFullscreen) {
            currentSprite = isFullscreen ? on : off;
            button.image.sprite = currentSprite;
        }
    }
}
