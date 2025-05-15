using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using PLAYER;
using UnityEngine.SceneManagement;
using System.Collections;

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
    [SerializeField] private GameObject transitionPrefab;
    private CanvasGroup transitionCanvas;
    private Animator transitionAnimator;
    private float transitionTime;
    public bool isPaused = false;
    private void Awake() {
        CloseOpenMenu();
        CloseMainMenu();
        CloseSettingsMenu();
        ButtonListeners();

        transitionCanvas = transitionPrefab.GetComponentInChildren<CanvasGroup>();
        transitionAnimator = transitionPrefab.GetComponent<Animator>();
        transitionTime = transitionAnimator.GetCurrentAnimatorStateInfo(0).length;

        transitionCanvas.alpha = 0f;
        transitionCanvas.interactable = false;
        transitionCanvas.blocksRaycasts = false;
    }
    private void ButtonListeners() {
        resumeButton.onClick.AddListener(() => CloseOpenMenu());
        settingsButton.onClick.AddListener(() => OpenSettingsMenu());
        comeBackButton.onClick.AddListener(() => CloseSettingsMenu());
        quitButton.onClick.AddListener(() => QuitGame());
    }
    public void ManagePauseMenu(InputAction.CallbackContext context) {
        if (context.started) {
            if (!isPaused) {
                OpenPauseMenu();
            }
            else {
                CloseOpenMenu();
            }
        }
    }
    private void OpenPauseMenu() {
        pauseCanvasGroup.alpha = 1;
        pauseCanvasGroup.blocksRaycasts = true;
        pauseCanvasGroup.interactable = true;
        isPaused = true;
        pauseCanvasGroup.gameObject.SetActive(true);

        Time.timeScale = 0;
        player.GetComponent<PlayerController>().IsPaused = isPaused;
    }
    private void CloseOpenMenu() {
        pauseCanvasGroup.alpha = 0;
        pauseCanvasGroup.blocksRaycasts = false;
        pauseCanvasGroup.interactable = false;
        isPaused = false;
        CloseSettingsMenu();
        pauseCanvasGroup.gameObject.SetActive(false);

        Time.timeScale = 1;
        player.GetComponent<PlayerController>().IsPaused = isPaused;
    }
    public void OpenMainMenu() {
        mainCanvasGroup.alpha = 1;
        mainCanvasGroup.blocksRaycasts = true;
        mainCanvasGroup.interactable = true;
        mainCanvasGroup.gameObject.SetActive(true);
    }
    private void CloseMainMenu() {
        mainCanvasGroup.alpha = 0;
        mainCanvasGroup.blocksRaycasts = false;
        mainCanvasGroup.interactable = false;
        mainCanvasGroup.gameObject.SetActive(false);
    }
    public void OpenSettingsMenu() {
        CloseMainMenu();
        submenuCanvasGroup.alpha = 1;
        submenuCanvasGroup.blocksRaycasts = true;
        submenuCanvasGroup.interactable = true;
        submenuCanvasGroup.gameObject.SetActive(true);
    }
    public void CloseSettingsMenu() {
        submenuCanvasGroup.alpha = 0;
        submenuCanvasGroup.blocksRaycasts = false;
        submenuCanvasGroup.interactable = false;
        submenuCanvasGroup.gameObject.SetActive(false);
        OpenMainMenu();
    }
    private void QuitGame() {
        Time.timeScale = 1;
        StartCoroutine(MenuStartTransition());
    }
    private IEnumerator MenuStartTransition() {
        transitionAnimator.SetTrigger("Start");
        CloseMainMenu();

        yield return new WaitForSeconds(transitionTime + 0.5f);
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }
}
