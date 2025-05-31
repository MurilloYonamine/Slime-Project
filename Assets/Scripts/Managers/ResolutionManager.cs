using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MENU.SETTINGS {
    public class ResolutionManager : MonoBehaviour {
        public static ResolutionManager Instance { get; private set; }

        [SerializeField] private TMP_Dropdown resolutionDropdown;
        private List<Resolution> availableResolutions = new List<Resolution>();
        private List<string> options = new List<string>();

        private int currentLevel = 0;
        private int previuslyLevel = 0;

        [SerializeField] private SwitchButton switchButton;

        private PauseManager pauseManager;

        private void Awake() {
            if (Instance == null) {
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
                Instance = this;
            } else {
                DestroyImmediate(gameObject);
                return;
            }

            SetResolution();
        }

        private void Start() {
            if (resolutionDropdown == null) resolutionDropdown = FindAnyObjectByType<TMP_Dropdown>();

            switchButton.Initialize();

            int savedIndex = PlayerPrefs.GetInt("ResolutionIndex", 3);
            int isFullscreen = PlayerPrefs.GetInt("IsFullscreen", 0);

            resolutionDropdown.value = savedIndex;
            currentLevel = savedIndex;

            Resolution res = availableResolutions[savedIndex];
            Screen.SetResolution(res.width, res.height,
                isFullscreen == 1 ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);

            switchButton.SetFullscreenSprite(isFullscreen == 1);
        }

        private void SetResolution() {
            availableResolutions.Clear();
            options.Clear();

            int[] scales = { 40, 60, 80, 100, 120 };
            foreach (int scale in scales) {
                int width = 16 * scale;
                int height = 9 * scale;
                availableResolutions.Add(new Resolution { width = width, height = height });
                options.Add($"{width}x{height}");
            }

            resolutionDropdown.ClearOptions();
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.onValueChanged.RemoveAllListeners();
            resolutionDropdown.onValueChanged.AddListener(OnResolutionChange);
        }

        private void OnLevelWasLoaded(int level) {
            if (resolutionDropdown == null) resolutionDropdown = FindAnyObjectByType<TMP_Dropdown>();
            if (pauseManager == null) pauseManager = FindAnyObjectByType<PauseManager>();
            if (switchButton.button == null) switchButton.button = pauseManager.switchButton;

            SetResolution();

            int savedIndex = PlayerPrefs.GetInt("ResolutionIndex", 3);
            int isFullscreen = PlayerPrefs.GetInt("IsFullscreen", 0);

            resolutionDropdown.value = savedIndex;
            currentLevel = savedIndex;

            Resolution res = availableResolutions[savedIndex];
            Screen.SetResolution(res.width, res.height, isFullscreen == 1 ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);

            switchButton.SetFullscreenSprite(isFullscreen == 1);
        }


        private void OnResolutionChange(int index) {
            Resolution res = availableResolutions[index];
            currentLevel = index;

            Screen.SetResolution(res.width, res.height, FullScreenMode.Windowed);

            switchButton.SetFullscreenSprite(false);

            PlayerPrefs.SetInt("ResolutionIndex", index);
            PlayerPrefs.SetInt("IsFullscreen", 0);
            PlayerPrefs.Save();
        }


        public void ChangeResolutionBySwitch() {
            Resolution res;

            if (Screen.fullScreen) {
                res = availableResolutions[previuslyLevel];
                currentLevel = previuslyLevel;
                resolutionDropdown.value = currentLevel;

                Screen.SetResolution(res.width, res.height, FullScreenMode.Windowed);
                switchButton.TaskOnClick();

                PlayerPrefs.SetInt("ResolutionIndex", currentLevel);
                PlayerPrefs.SetInt("IsFullscreen", 0);
            } else {
                previuslyLevel = currentLevel;
                res = availableResolutions[availableResolutions.Count - 1];
                currentLevel = availableResolutions.Count - 1;
                resolutionDropdown.value = currentLevel;

                Screen.SetResolution(res.width, res.height, FullScreenMode.FullScreenWindow);
                switchButton.TaskOnClick();

                PlayerPrefs.SetInt("ResolutionIndex", currentLevel);
                PlayerPrefs.SetInt("IsFullscreen", 1);
            }

            PlayerPrefs.Save();
        }
    }
}
