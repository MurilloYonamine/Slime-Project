using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using PLAYER;

public class PauseManager : MonoBehaviour {

    [SerializeField] private GameObject player;

    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    [Header("Canvas Groups")]
    [SerializeField] private CanvasGroup pauseCanvasGroup;
    [SerializeField] private CanvasGroup mainCanvasGroup;
    [SerializeField] private CanvasGroup submenuCanvasGroup;

    [Header("Settings")]
    [SerializeField] private Button comeBackButton;

    public bool isPaused = false;
    private void Awake() {
        CloseOpenMenu();
        CloseMainMenu();
        CloseSettingsMenu();
        ButtonListeners();
    }
    private void ButtonListeners() {
        resumeButton.onClick.AddListener(() => CloseOpenMenu());
        settingsButton.onClick.AddListener(() => OpenSettingsMenu());
        comeBackButton.onClick.AddListener(() => CloseSettingsMenu());
    }
    public void ManagePauseMenu(InputAction.CallbackContext context) {
        if (context.started) {
            if (!isPaused) {
                OpenPauseMenu();
            } else {
                CloseOpenMenu();
            }
        }
    }
    private void OpenPauseMenu() {
        pauseCanvasGroup.alpha = 1;
        pauseCanvasGroup.blocksRaycasts = true;
        pauseCanvasGroup.interactable = true;
        isPaused = true;

        Time.timeScale = 0;
        player.GetComponent<PlayerController>().IsPaused = isPaused;
    }
    private void CloseOpenMenu() {
        pauseCanvasGroup.alpha = 0;
        pauseCanvasGroup.blocksRaycasts = false;
        pauseCanvasGroup.interactable = false;
        isPaused = false;
        CloseSettingsMenu();

        Time.timeScale = 1;
        player.GetComponent<PlayerController>().IsPaused = isPaused;
    }
    public void OpenMainMenu() {
        mainCanvasGroup.alpha = 1;
        mainCanvasGroup.blocksRaycasts = true;
        mainCanvasGroup.interactable = true;
    }
    private void CloseMainMenu() {
        mainCanvasGroup.alpha = 0;
        mainCanvasGroup.blocksRaycasts = false;
        mainCanvasGroup.interactable = false;
    }
    public void OpenSettingsMenu() {
        CloseMainMenu();
        submenuCanvasGroup.alpha = 1;
        submenuCanvasGroup.blocksRaycasts = true;
        submenuCanvasGroup.interactable = true;
    }
    public void CloseSettingsMenu() {
        submenuCanvasGroup.alpha = 0;
        submenuCanvasGroup.blocksRaycasts = false;
        submenuCanvasGroup.interactable = false;
        OpenMainMenu();
    }
}
