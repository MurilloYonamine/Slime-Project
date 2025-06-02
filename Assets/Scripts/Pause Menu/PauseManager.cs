using AUDIO;
using MENU;
using MENU.SETTINGS;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MENU {
    public class PauseManager : MonoBehaviour {
        [Header("Buttons")]
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button comeBackButton;
        [SerializeField] public Button switchButton;

        [Header("Canvas Groups")]
        [SerializeField] private CanvasGroup pauseCanvasGroup;
        [SerializeField] private CanvasGroup mainCanvasGroup;
        [SerializeField] private CanvasGroup submenuCanvasGroup;

        [Header("Settings")]
        [SerializeField] private GameObject transitionPrefab;

        private Menu menu;

        private void Start() {
            menu = new Menu(transitionPrefab);

            Canvas canvas = pauseCanvasGroup.GetComponentInParent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;

            CloseOpenMenu();
            CloseMainMenu();
            CloseSettingsMenu();
            ButtonListeners();
        }
        private void ButtonListeners() {
            resumeButton.onClick.AddListener(() => CloseOpenMenu());
            restartButton.onClick.AddListener(() => Restart());
            settingsButton.onClick.AddListener(() => OpenSettingsMenu());
            comeBackButton.onClick.AddListener(() => CloseSettingsMenu());
            quitButton.onClick.AddListener(() => QuitGame());

            switchButton.onClick.AddListener(() => ResolutionManager.Instance.ChangeResolutionBySwitch());
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
        private void Restart() {
            CloseOpenMenu();
            GameManager.Instance.RespawnPlayer();
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
        }

        private IEnumerator QuitGameRoutine() {
            CloseMainMenu();
            yield return menu.HandleTransition("Start", moreTime: 0.5f);
            AudioManager.Instance.StopAllTracks();
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        }
    }
}