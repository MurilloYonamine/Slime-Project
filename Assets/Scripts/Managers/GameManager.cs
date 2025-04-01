using AUDIO;
using PLAYER;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player;

    [SerializeField]private TextMeshProUGUI magicSeedCountTxt;

    [HideInInspector] public int magicSeedCount = 0;

    void Awake()
    {
        //Cursor.visible = false;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }
        //AudioManager.Instance.PlayTrack("Audio/Music/test-song", loop: true);
    }
    private void Update() {
        magicSeedCountTxt.text = $"Magic Seed Count: {magicSeedCount}";
    }
}
