using System.Collections;
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

    [SerializeField] private GameObject transitionPrefab;
    [SerializeField] private CanvasGroup transitionCanvas;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private float transitionTime;
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
        transitionCanvas = transitionPrefab.GetComponentInChildren<CanvasGroup>();
        transitionAnimator = transitionPrefab.GetComponent<Animator>();
        transitionTime = transitionAnimator.GetCurrentAnimatorStateInfo(0).length;

        transitionCanvas.alpha = 0f;
        transitionCanvas.interactable = false;
        transitionCanvas.blocksRaycasts = false;

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
        if (currentLife == lifeList.Count) {
            for (int i = 0; i < lifeList.Count; i++) {
                lifeList[i].GetComponent<CanvasGroup>().alpha = 1f;
            }
            return;
        }

        lifeList[currentLife].GetComponent<CanvasGroup>().alpha = 0.2f;
    }
    public void ChangeCheckpoint(GameObject checkpoint) {
        newCheckpointPosition = checkpoint.transform.position;
    }

    public void RespawnPlayer() {
        virtualCamerasList[currentCheckpoint].Priority = 1;
        StartCoroutine(TransitionToRespawn());
    }
    private IEnumerator TransitionToRespawn() {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        newCheckpointPosition = checkpointList[currentCheckpoint].transform.position;
        player.transform.position = newCheckpointPosition;
    }
    public int ChangeCurrentCheckpoint() => currentCheckpoint = FindTheHighestPriorityCamera();
    public int FindTheHighestPriorityCamera() {
        for (int i = 0; i < virtualCamerasList.Count; i++) {
            if (virtualCamerasList[i].Priority == 1) return i;
        }
        return currentCheckpoint;
    }
}