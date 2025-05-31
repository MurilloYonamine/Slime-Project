using System;
using UnityEngine;
using UnityEngine.UI;

namespace MENU.SETTINGS {
    [Serializable]
    public class SwitchButton {
        [SerializeField] public Button button;
        [SerializeField] private Sprite on;
        [SerializeField] private Sprite off;

        public void Initialize() {
            if (button == null) return;

            bool isFullscreen = Screen.fullScreen;
            button.image.sprite = isFullscreen ? on : off;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick() {
            ResolutionManager.Instance.ChangeResolutionBySwitch();
        }

        public void SetFullscreenSprite(bool isFullscreen) {
            if (button == null) return;
            button.image.sprite = isFullscreen ? on : off;
        }
    }
}
