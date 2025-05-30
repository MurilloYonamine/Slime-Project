using MENU.SETTINGS;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour {
    [SerializeField] private Button button;

    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;

    [SerializeField] private Sprite currentSprite;
    private void Start() {
        button = GetComponent<Button>();

        if(!Screen.fullScreen) {
            button.image.sprite = off;
            return;
        }

        button.image.sprite = on;

        currentSprite = button.image.sprite;
    }

    public void TaskOnClick() {
        button.image.sprite = currentSprite == on ? off : on;
        currentSprite = button.image.sprite;

        ChangeResolution();
    }
    private void ChangeResolution() => ResolutionManager.Instance.ChangeResolutionBySwitch();
    
}
