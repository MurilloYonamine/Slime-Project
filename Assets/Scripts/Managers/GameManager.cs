using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject player;


    [SerializeField] public Transform PlayerOriginalLayer;

    [SerializeField] private List<GameObject> allGrapplers;
    [SerializeField] private List<Image> allLifes;


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
    public void Die() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void ChangeGrapplersDistance(float distance) {
        foreach(GameObject grappler in allGrapplers) {
            grappler.GetComponentInChildren<CircleCollider2D>().radius = distance;
        }
    }
    public int GetLifeSize() => allLifes.Count;
    public void ChangeLifeHUD(int currentLife) {
        Debug.Log(currentLife);
        if(currentLife > allLifes.Count) currentLife = allLifes.Count;
        allLifes[currentLife].GetComponent<CanvasGroup>().alpha = 0.2f;
    }
}
