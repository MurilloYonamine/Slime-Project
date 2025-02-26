using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour {
    [SerializeField] private AudioMixer audioMixer;

    private const string MASTER_VOLUME = "MasterVolume";
    private const string SOUND_FX_VOLUME = "SoundFXVolume";
    private const string MUSIC_VOLUME = "MusicVolume";

    public void SetMasterVolume(float level) {
        audioMixer.SetFloat(MASTER_VOLUME, Mathf.Log10(level) * 20);
    }
    public void SetSoundFXVolume(float level) {
        audioMixer.SetFloat(SOUND_FX_VOLUME, Mathf.Log10(level) * 20);
    }
    public void SetMusicVolume(float level) {
        audioMixer.SetFloat(MUSIC_VOLUME, Mathf.Log10(level) * 20);
    }
}
