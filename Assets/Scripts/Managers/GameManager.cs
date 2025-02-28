using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Text Elements")]
    [SerializeField] private TextMeshProUGUI mousePositionText;
    [SerializeField] private TextMeshProUGUI playerPositionText;

    [Header("Game Objects")]
    [SerializeField] public GameObject player;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }
    }

    void Update()
    {
        mousePositionText.text = "Mouse Position: " + Mouse.current.position.value.ToString();
        playerPositionText.text = "Player Position: " + player.transform.position.ToString();
    }

}
