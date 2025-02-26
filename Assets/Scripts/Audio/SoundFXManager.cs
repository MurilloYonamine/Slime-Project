using UnityEngine;
using UnityEngine.Audio;

namespace AUDIO {
    public class SoundFXManager : MonoBehaviour {
        public static SoundFXManager Instance;
        [SerializeField] private AudioMixerGroup soundFXMixerGroup;     

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume) {
            // spawn
            GameObject gameObject = new GameObject();
            gameObject.name = $"SoundFX_{audioClip.name}";

            AudioSource audioSource = Instantiate(gameObject.AddComponent<AudioSource>(),
                                                  spawnTransform.position, Quaternion.identity);

            audioSource.outputAudioMixerGroup = soundFXMixerGroup;

            audioSource.clip = audioClip;
            audioSource.volume = volume;

            audioSource.Play();

            float clipLength = audioClip.length;

            Destroy(audioSource.gameObject, clipLength);
        }
    }
}