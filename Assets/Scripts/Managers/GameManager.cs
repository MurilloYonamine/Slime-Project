using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    [Header("Player Components")]
    [SerializeField] private GameObject player;
    [SerializeField] public Transform PlayerOriginalLayer;
    [SerializeField] private int currentCheckpoint;

    [Header("List Components")]
    [SerializeField] private List<GameObject> grapplerList;
    [SerializeField] private List<Image> lifeList;
    [SerializeField] private List<GameObject> checkpointList;
    [SerializeField] private List<CinemachineVirtualCamera> virtualCamerasList;
    [SerializeField] private Vector3 newCheckpointPosition;
    private void Awake() {
        //Cursor.visible = false;
        if (Instance == null) {
            Instance = this;
        }
        else {
            DestroyImmediate(gameObject);
            return;
        }
        //AudioManager.Instance.PlayTrack("Audio/Music/test-song", loop: true);
    }
    private void Start() {
        currentCheckpoint = FindTheHighestPriorityCamera();
        newCheckpointPosition = checkpointList[currentCheckpoint].transform.position;
        player.transform.position = newCheckpointPosition;
    }
    public void ChangeGrapplersDistance(float distance) {
        foreach (GameObject grappler in grapplerList) {
            grappler.GetComponentInChildren<CircleCollider2D>().radius = distance;
        }
    }
    public int GetLifeSize() => lifeList.Count;
    public void ChangeLifeHUD(int currentLife) {
        if (currentLife > lifeList.Count) currentLife = lifeList.Count;
        lifeList[currentLife].GetComponent<CanvasGroup>().alpha = 0.2f;
    }
    public void ChangeCheckpoint(GameObject checkpoint) {
        newCheckpointPosition = checkpoint.transform.position;
    }

    public void RespawnPlayer() {
        virtualCamerasList[currentCheckpoint].Priority = 1;
        newCheckpointPosition = checkpointList[currentCheckpoint].transform.position;
        player.transform.position = newCheckpointPosition;
    }
    public int ChangeCurrentCheckpoint() => currentCheckpoint = FindTheHighestPriorityCamera();
    public int FindTheHighestPriorityCamera() {
        int highestPriority = 0;
        CinemachineVirtualCamera highestPriorityCamera = null;
        foreach (CinemachineVirtualCamera camera in virtualCamerasList) {
            if (camera.Priority > highestPriority) {
                highestPriority = camera.Priority;
                highestPriorityCamera = camera;
            }
        }
        return highestPriorityCamera != null ? virtualCamerasList.IndexOf(highestPriorityCamera) : -1;
    }
}
