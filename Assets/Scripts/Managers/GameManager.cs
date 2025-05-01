using System.Collections;
using System.Collections.Generic;
using AUDIO;
using CAMERA;
using Cinemachine;
using PLAYER;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject player;

    [SerializeField] private TextMeshProUGUI magicSeedCountTxt;
    [HideInInspector] public int magicSeedCount = 0;

    [SerializeField] private int currentVirtualCamera = 0;
    [SerializeField] public List<CinemachineVirtualCamera> virtualCameras;
    [SerializeField] public Transform PlayerOriginalLayer;
    private void Awake()
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
    private void Update() => magicSeedCountTxt.text = $"Magic Seed Count: {magicSeedCount}";
    public void Die() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void ChangeCurrentCamera(bool playerHasPassed)
    {
        if (!playerHasPassed)
        {
            virtualCameras[currentVirtualCamera].Priority--;
            currentVirtualCamera++;
            virtualCameras[currentVirtualCamera].Priority++;
            return;
        }
        virtualCameras[currentVirtualCamera].Priority--;
        currentVirtualCamera--;
        virtualCameras[currentVirtualCamera].Priority++;
    }
}
