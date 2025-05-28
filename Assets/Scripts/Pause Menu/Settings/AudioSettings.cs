using AUDIO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace MENU.SETTINGS {
    public class AudioSettings : MonoBehaviour {
        private enum AudioType { Master, Music, SFX }
        private AudioMixerGroup audioMixerGroup;
        private string volumeParameterName;

        [SerializeField] private AudioType audioType;

        [Header("Slider")]
        [SerializeField] private Slider volumeSlider;

        private float minVolume = -20;
        private float maxVolume = 10;

        private bool isInitializing = false;

        private void Start() {
            SetupAudioType();

            volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);

            InitializeVolume();
        }

        private void InitializeVolume() {
            isInitializing = true;

            if (audioMixerGroup.audioMixer.GetFloat(volumeParameterName, out float currentVolume)) {
                float normalized = Mathf.InverseLerp(minVolume, maxVolume, currentVolume);
                volumeSlider.value = normalized;
            }

            isInitializing = false;
        }

        private void SetupAudioType() {
            switch (audioType) {
                case AudioType.Master:
                    audioMixerGroup = AudioManager.Instance.masterMixer;
                    volumeParameterName = AudioManager.MASTER_VOLUME_PARAMETER_NAME;
                    break;
                case AudioType.Music:
                    audioMixerGroup = AudioManager.Instance.musicMixer;
                    volumeParameterName = AudioManager.MUSIC_VOLUME_PARAMETER_NAME;
                    break;
                case AudioType.SFX:
                    audioMixerGroup = AudioManager.Instance.sfxMixer;
                    volumeParameterName = AudioManager.SFX_VOLUME_PARAMETER_NAME;
                    break;
                default:
                    break;
            }
        }

        private void OnSliderValueChanged(float value) {
            if (isInitializing) return;
            float volume = Mathf.Lerp(minVolume, maxVolume, value);
            audioMixerGroup.audioMixer.SetFloat(volumeParameterName, volume);

            if (audioType == AudioType.SFX) {
                AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_shot");
            }
        }
    }
}