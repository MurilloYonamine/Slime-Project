using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject player;

    [SerializeField] private TextMeshProUGUI magicSeedCountTxt;
    [HideInInspector] public int magicSeedCount = 0;

    [SerializeField] public Transform PlayerOriginalLayer;

    [SerializeField] private List<GameObject> allGrapplers;

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
    public void ChangeGrapplersDistance(float distance) {
        foreach(GameObject grappler in allGrapplers) {
            grappler.GetComponentInChildren<CircleCollider2D>().radius = distance;
        }
    }

}
