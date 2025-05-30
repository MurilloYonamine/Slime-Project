using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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


        private void Awake() {
            Screen.SetResolution(640, 360, Screen.fullScreenMode = FullScreenMode.Windowed);

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
            DontDestroyOnLoad(gameObject);
        }
        private void SetResolution() {
            resolutionDropdown.ClearOptions();

            int[] scales = { 40, 60, 80, 100, 120 };
            foreach (int scale in scales) {
                int width = 16 * scale;
                int height = 9 * scale;
                availableResolutions.Add(new Resolution { width = width, height = height });
                options.Add($"{width} x {height}");
            }

            resolutionDropdown.ClearOptions();
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.onValueChanged.AddListener(OnResolutionChange);
        }
        private void OnLevelWasLoaded(int level) {
            if (resolutionDropdown == null) resolutionDropdown = FindAnyObjectByType<TMP_Dropdown>();

            SetResolution();
            resolutionDropdown.value = currentLevel;
        }
       
        private void OnResolutionChange(int index) {
            Resolution res = availableResolutions[index];

            currentLevel = index;

            Screen.SetResolution(res.width, res.height, Screen.fullScreenMode = FullScreenMode.Windowed);
        }
        
        public void ChangeResolutionBySwitch() {
            Resolution res = new Resolution();

            if (Screen.fullScreen) {
                res = availableResolutions[previuslyLevel];

                currentLevel = previuslyLevel;

                resolutionDropdown.value = currentLevel;

                Screen.SetResolution(res.width, res.height, Screen.fullScreenMode = FullScreenMode.Windowed);
                return;
            }

            previuslyLevel = currentLevel;

            res = availableResolutions[availableResolutions.Count - 1];

            currentLevel = availableResolutions.Count;

            resolutionDropdown.value = currentLevel;

            Screen.SetResolution(res.width, res.height, Screen.fullScreenMode = FullScreenMode.FullScreenWindow);
        }
    }
}