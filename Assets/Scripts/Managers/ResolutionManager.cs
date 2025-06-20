using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MENU.SETTINGS {
    public class ResolutionManager : MonoBehaviour {
        public static ResolutionManager Instance { get; private set; }

        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private SwitchButton switchButton;

        private List<Resolution> availableResolutions = new List<Resolution>();
        private List<string> options = new List<string>();

        private void Awake() {
            if (Instance == null) {
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
                Instance = this;

                SceneManager.sceneLoaded += OnSceneLoaded;
                InitializeResolutions();
            } else {
                DestroyImmediate(gameObject);
            }
        }

        private void Start() {
            TryFindComponents();
            switchButton.Initialize();
            ApplySavedResolution();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            TryFindComponents();
            switchButton.Initialize();
            ApplySavedResolution();
        }

        private void TryFindComponents() {
            if (resolutionDropdown == null) resolutionDropdown = FindAnyObjectByType<TMP_Dropdown>();
            if (switchButton.button == null)
                switchButton.button = GameObject.FindWithTag("SwitchButton")?.GetComponent<Button>();
        }

        private void InitializeResolutions() {
            availableResolutions.Clear();
            options.Clear();

            int[] scales = { 40, 80, 120 }; // 640x360, 1280x720, 1920x1080
            foreach (int scale in scales) {
                int width = 16 * scale;
                int height = 9 * scale;
                availableResolutions.Add(new Resolution { width = width, height = height });
                options.Add($"{width}x{height}");
            }

            if (resolutionDropdown == null) return;

            resolutionDropdown.ClearOptions();
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.onValueChanged.RemoveAllListeners();
            resolutionDropdown.onValueChanged.AddListener(OnResolutionChange);
        }

        private void ApplySavedResolution() {
            InitializeResolutions();

            int savedIndex = PlayerPrefs.GetInt("ResolutionIndex", 1); // default = 1280x720 (80*16x80*9)
            bool isFullscreen = PlayerPrefs.GetInt("IsFullscreen", 0) == 1;

            savedIndex = Mathf.Clamp(savedIndex, 0, availableResolutions.Count - 1);

            resolutionDropdown.value = savedIndex;

            Resolution res = availableResolutions[savedIndex];
            Screen.SetResolution(res.width, res.height, isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);

            switchButton.SetFullscreenSprite(isFullscreen);
        }

        private void OnResolutionChange(int index) {
            Resolution res = availableResolutions[index];
            bool isFullscreen = PlayerPrefs.GetInt("IsFullscreen", 0) == 1;

            Screen.SetResolution(res.width, res.height,
                isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);

            PlayerPrefs.SetInt("ResolutionIndex", index);
            PlayerPrefs.Save();
        }

        public void ChangeResolutionBySwitch() {
            bool isCurrentlyFullscreen = Screen.fullScreen;
            bool newIsFullscreen = !isCurrentlyFullscreen;

            int savedIndex = PlayerPrefs.GetInt("ResolutionIndex", 1);
            savedIndex = Mathf.Clamp(savedIndex, 0, availableResolutions.Count - 1);
            Resolution res = availableResolutions[savedIndex];

            Screen.SetResolution(res.width, res.height,
                newIsFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);

            switchButton.SetFullscreenSprite(newIsFullscreen);

            PlayerPrefs.SetInt("IsFullscreen", newIsFullscreen ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}
