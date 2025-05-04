using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
namespace PLAYER
{
    [Serializable]
    public class PlayerStats
    {
        [SerializeField] private TextMeshProUGUI mousePositionText;
        [SerializeField] private TextMeshProUGUI playerPositionText;
        [SerializeField] private TextMeshProUGUI playerIsJumpingText;
        [SerializeField] private TextMeshProUGUI playerIsClimbingText;
        [SerializeField] private TextMeshProUGUI playerIsGrapplingText;
        [SerializeField] private TextMeshProUGUI playerCanGrappleText;
        [SerializeField] private TextMeshProUGUI playerIsSpikeActiveText;

        private PlayerController player;

        public void Initialize(PlayerController player)
        {
            this.player = player;

            if (player.DisableStats)
            {
                mousePositionText.gameObject.SetActive(false);
                playerPositionText.gameObject.SetActive(false);
                playerIsJumpingText.gameObject.SetActive(false);
                playerIsClimbingText.gameObject.SetActive(false);
                playerIsGrapplingText.gameObject.SetActive(false);
                playerCanGrappleText.gameObject.SetActive(false);
                playerIsSpikeActiveText.gameObject.SetActive(false);
            }
            else
            {
                mousePositionText.gameObject.SetActive(true);
                playerPositionText.gameObject.SetActive(true);
                playerIsJumpingText.gameObject.SetActive(true);
                playerIsClimbingText.gameObject.SetActive(true);
                playerIsClimbingText.gameObject.SetActive(true);
                playerCanGrappleText.gameObject.SetActive(true);
                playerIsSpikeActiveText.gameObject.SetActive(true);
            }
        }

        public void OnUpdate()
        {

            mousePositionText.text = "Mouse Position: " + Mouse.current.position.value.ToString();
            playerPositionText.text = "Player Position: " + player.transform.position.ToString();
            playerIsJumpingText.text = "Is player jumping? " + player.IsJumping.ToString();
            playerIsGrapplingText.text = "Is player grappling? " + player.IsGrappling.ToString();
            playerIsClimbingText.text = "Is player climbing? " + player.IsClimbing.ToString();
            playerCanGrappleText.text = "Can player grapple? " + player.CanGrapple.ToString();
            playerIsSpikeActiveText.text = "Is player spike active? " + player.IsSpikeActive.ToString();
        }
    }
}
