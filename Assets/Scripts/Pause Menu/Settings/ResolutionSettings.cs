using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace MENU.SETTINGS {
    public class ResolutionSettings : MonoBehaviour {
        [SerializeField] private TMP_Dropdown resolutionDropdown;

        private List<Resolution> availableResolutions = new List<Resolution>();
        private List<string> options = new List<string>();

        private void Awake() {
            int[] scales = { 30, 40, 60, 80, 100, 120 }; 
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


        private void OnResolutionChange(int index) {
            Resolution res = availableResolutions[index];

            Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        }
    }
}