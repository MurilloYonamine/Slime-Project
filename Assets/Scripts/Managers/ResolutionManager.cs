using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace MENU.SETTINGS {
    public class ResolutionManager : MonoBehaviour {


        private static ResolutionManager Instance;

        [SerializeField] private TMP_Dropdown resolutionDropdown;

        private List<Resolution> availableResolutions = new List<Resolution>();
        private List<string> options = new List<string>();


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
            DontDestroyOnLoad(gameObject);
        }
        private void SetResolution() {
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
            if (resolutionDropdown == null) {
                resolutionDropdown = FindAnyObjectByType<TMP_Dropdown>();
            }
            SetResolution();
            resolutionDropdown.value = currentLevel;
        }
        private int currentLevel = 0;
        private void OnResolutionChange(int index) {
            Resolution res = availableResolutions[index];

            currentLevel = index;

            Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        }
    }
}