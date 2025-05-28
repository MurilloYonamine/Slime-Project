using CAMERA;
using System.Collections;
using UnityEngine;
using SYSTEM;
using SYSTEM.CHECKPOINT;
using PLAYER;


public class CheatManager : MonoBehaviour {
    public static CheatManager Instance { get; private set; }

    [SerializeField] private GameObject CheatMenu;

    public bool invunerable = false;
    public bool flying = false;
    public bool canMouseControl = true;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            DestroyImmediate(gameObject);
            return;
        }
    }
    public void cheatercheaterpoopooeater() {
        Debug.Log("piss");
        if (CheatMenu.activeInHierarchy == true) {
            CheatMenu.SetActive(false);
            canMouseControl = true;
            Cursor.visible = false;
        }
        else {
            CheatMenu.SetActive(true);
            canMouseControl = false;
            Cursor.visible = true;
        }
    }

    public void Teleportation(int target) => GameManager.Instance.TeleportToCheckpoint(target);
    public void OnInvChange() => invunerable = !invunerable;
    public void OnFlyChange() => flying = !flying;
}
