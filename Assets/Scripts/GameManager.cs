using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Text Elements")]
    [SerializeField] private TextMeshProUGUI mousePositionText;
    [SerializeField] private TextMeshProUGUI playerPositionText;

    [Header("Game Objects")]
    [SerializeField] private GameObject player;

    void Update()
    {
        mousePositionText.text = "Mouse Position: " + Mouse.current.position.value.ToString();
        playerPositionText.text = "Player Position: " + player.transform.position.ToString();
    }
}
