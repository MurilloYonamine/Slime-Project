using AUDIO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour {
    [Header("Buttons")]
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button decreaseButton;

    [SerializeField] private GameObject[] volumeSquares;
    private Color originalSquareColor = new Color(0.42f, 0.60f, 0.44f);

    [SerializeField] private int volumeLevel = 0;

    private void Awake() {
        increaseButton.onClick.AddListener(IncreaseVolume);
        decreaseButton.onClick.AddListener(DecreaseVolume);
    }
    private void IncreaseVolume() {
        if (volumeLevel >= volumeSquares.Length) return;
        volumeSquares[volumeLevel].GetComponent<Image>().color = new Color(0.14f, 0.31f, 0.33f);
        volumeLevel++;
    }
    private void DecreaseVolume() {
        if (volumeLevel <= 0) return;
        volumeLevel--;
        volumeSquares[volumeLevel].GetComponent<Image>().color = originalSquareColor;
    }

}
