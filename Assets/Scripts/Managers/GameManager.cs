using PLAYER;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Text Elements")]
    [SerializeField] private TextMeshProUGUI mousePositionText;
    [SerializeField] private TextMeshProUGUI playerPositionText;
    [SerializeField] private TextMeshProUGUI playerIsJumping;
    [SerializeField] private TextMeshProUGUI playerIsClimbingText;
    [SerializeField] private TextMeshProUGUI playerCanGrappleText;

    [Header("Game Objects")]
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject playerAim;

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
    }

    void Update()
    {
        mousePositionText.text = "Mouse Position: " + Mouse.current.position.value.ToString();
        playerPositionText.text = "Player Position: " + player.transform.position.ToString();
        playerIsJumping.text = "Is player jumping? " + player.GetComponent<PlayerMovement>().isJumping.ToString();
        playerIsClimbingText.text = "Is player climbing? " + player.GetComponent<PlayerMovement>().isClimbing.ToString();
    }

}
