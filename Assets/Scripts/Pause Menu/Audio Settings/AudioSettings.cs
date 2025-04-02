using AUDIO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour {
    private enum AudioType { Master, Music, SFX }
    private AudioMixerGroup audioMixerGroup;
    private string volumeParameterName;

    [SerializeField] private AudioType audioType;

    [Header("Buttons")]
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button muteButton;

    [SerializeField] private GameObject[] volumeSquares;
    private Color originalSquareColor = new Color(0.42f, 0.60f, 0.44f);

    private int volumeLevel = 0;
    private float minVolume = -20;
    private float maxVolume = 10;

    private float previousVolume = 0f;


    private void Start() {
        SetupAudioType();

        increaseButton.onClick.AddListener(IncreaseVolume);
        decreaseButton.onClick.AddListener(DecreaseVolume);
        muteButton.onClick.AddListener(MuteUnmuteVolume);

        InitializeVolume();
    }

    private void InitializeVolume() {
        if (audioMixerGroup.audioMixer.GetFloat(volumeParameterName, out float currentVolume)) {
            volumeLevel = Mathf.RoundToInt((currentVolume - minVolume) / (maxVolume - minVolume) * (volumeSquares.Length - 1));
            volumeLevel = Mathf.Clamp(volumeLevel, 0, volumeSquares.Length);

            UpdateVolumeVisual();
        }
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

    private void IncreaseVolume() {
        if (volumeLevel >= volumeSquares.Length) return;
        volumeLevel++;
        UpdateVolumeVisual();
        UpdateVolumeMixer();

        if(audioType == AudioType.SFX) AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_shot");
    }

    private void DecreaseVolume() {
        if (volumeLevel <= 0) return;
        volumeLevel--;
        UpdateVolumeVisual();
        UpdateVolumeMixer();

        if (audioType == AudioType.SFX) AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/slime_shot");
    }


    private void MuteUnmuteVolume() {
        float currentVolume;
        audioMixerGroup.audioMixer.GetFloat(volumeParameterName, out currentVolume);

        if (currentVolume == AudioManager.MUTED_VOLUME_LEVEL) {
            audioMixerGroup.audioMixer.SetFloat(volumeParameterName, previousVolume);
            InitializeVolume();
            ToggleMuteButtonVisual(false);
        } else {
            previousVolume = currentVolume;
            audioMixerGroup.audioMixer.SetFloat(volumeParameterName, AudioManager.MUTED_VOLUME_LEVEL);
            volumeLevel = 0;
            UpdateVolumeVisual();
            ToggleMuteButtonVisual(true);
        }
    }
    private void ToggleMuteButtonVisual(bool isMuted) {
        if (isMuted) {
            muteButton.GetComponent<Image>().color = Color.white;
            muteButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        } else {
            muteButton.GetComponent<Image>().color = Color.black;
            muteButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
    }

    private void UpdateVolumeVisual() {
        for (int i = 0; i < volumeSquares.Length; i++) {
            volumeSquares[i].GetComponent<Image>().color = (i < volumeLevel) ?
                new Color(0.14f, 0.31f, 0.33f) : originalSquareColor;
        }
    }

    private void UpdateVolumeMixer() {
        float volume = Mathf.Lerp(minVolume, maxVolume, (float)volumeLevel / volumeSquares.Length);
        audioMixerGroup.audioMixer.SetFloat(volumeParameterName, volume);
    }
}
