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

    [Header("Death Transition Components")]
    [SerializeField] private GameObject deathTransition;
    private CanvasGroup deathTransitionCanvas;
    private Animator deathTransitionAnimator;
    private float deathTransitionTime;

    [Header("Menu Transition Components")]
    [SerializeField] private GameObject menuTransition;
    private CanvasGroup menuTransitionCanvas;
    private Animator menuTransitionAnimator;
    private float menuTransitionTime;


    private void Awake() {
        //Cursor.visible = false;
        deathTransitionAnimator = deathTransition.GetComponent<Animator>();
        deathTransitionCanvas = deathTransition.GetComponentInChildren<CanvasGroup>();
        deathTransitionCanvas.alpha = 0f;
        deathTransitionCanvas.interactable = false;
        deathTransitionCanvas.blocksRaycasts = false;
        deathTransitionTime = deathTransitionAnimator.GetCurrentAnimatorStateInfo(0).length;

        menuTransitionAnimator = menuTransition.GetComponent<Animator>();
        menuTransitionCanvas = menuTransition.GetComponentInChildren<CanvasGroup>();
        menuTransitionCanvas.alpha = 0f;
        menuTransitionCanvas.interactable = false;
        menuTransitionCanvas.blocksRaycasts = false;
        deathTransitionTime = deathTransitionAnimator.GetCurrentAnimatorStateInfo(0).length;

        if (Instance == null) {
            Instance = this;
        }
        else {
            DestroyImmediate(gameObject);
            return;
        }
        StartCoroutine(MenuTransition());
        //AudioManager.Instance.PlayTrack("Audio/Music/test-song", loop: true);
    }
    private void Start() {
        currentCheckpoint = FindTheHighestPriorityCamera();
        newCheckpointPosition = checkpointList[currentCheckpoint].transform.position;
        player.transform.position = newCheckpointPosition;
    }
    private IEnumerator MenuTransition() {
        menuTransitionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(menuTransitionTime);
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
        deathTransitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(deathTransitionTime);
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