using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using MENU;
using System.Collections;

namespace MENU {
    public class PauseManager : MonoBehaviour {
        [Header("Buttons")]
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button comeBackButton;

        [Header("Canvas Groups")]
        [SerializeField] private CanvasGroup pauseCanvasGroup;
        [SerializeField] private CanvasGroup mainCanvasGroup;
        [SerializeField] private CanvasGroup submenuCanvasGroup;

        [Header("Settings")]
        [SerializeField] private GameObject transitionPrefab;

        private Menu menu;

        private void Start() {
            menu = new Menu(transitionPrefab);


            CloseOpenMenu();
            CloseMainMenu();
            CloseSettingsMenu();
            ButtonListeners();
        }
        private void ButtonListeners() {
            resumeButton.onClick.AddListener(() => CloseOpenMenu());
            settingsButton.onClick.AddListener(() => OpenSettingsMenu());
            comeBackButton.onClick.AddListener(() => CloseSettingsMenu());
            quitButton.onClick.AddListener(() => QuitGame());
        }
        public void ManagePauseMenu(InputAction.CallbackContext context) {
            if (context.started) {
                if (!GameManager.Instance.isPaused) {
                    OpenPauseMenu();
                    return;
                }
                CloseOpenMenu();
            }
        }
        private void OpenPauseMenu() {
            menu.SetVisibility(pauseCanvasGroup, isVisible: true);
            OpenMainMenu();

            Time.timeScale = 0;
            GameManager.Instance.isPaused = true;
            Cursor.visible = true;
        }
        private void CloseOpenMenu() {
            menu.SetVisibility(pauseCanvasGroup, isVisible: false);
            CloseSettingsMenu();

            Time.timeScale = 1;
            GameManager.Instance.isPaused = false;
            Cursor.visible = false;
        }
        public void OpenMainMenu() => menu.SetVisibility(mainCanvasGroup, isVisible: true);
        public void CloseMainMenu() => menu.SetVisibility(mainCanvasGroup, isVisible: false);
        public void OpenSettingsMenu() {
            CloseMainMenu();
            menu.SetVisibility(submenuCanvasGroup, isVisible: true);
        }
        public void CloseSettingsMenu() {
            menu.SetVisibility(submenuCanvasGroup, isVisible: false);
            OpenMainMenu();
        }
        private void QuitGame() {
            Time.timeScale = 1;
            Cursor.visible = true;
            StartCoroutine(QuitGameRoutine());
            CloseMainMenu();
        }

        private IEnumerator QuitGameRoutine() {
            yield return menu.HandleTransition("Start", moreTime: 0.5f);
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        }
    }
}