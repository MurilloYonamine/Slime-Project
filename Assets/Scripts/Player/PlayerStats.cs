using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
namespace PLAYER {
	[Serializable]
	public class PlayerStats {
        [SerializeField] private TextMeshProUGUI mousePositionText;
        [SerializeField] private TextMeshProUGUI playerPositionText;
        [SerializeField] private TextMeshProUGUI playerIsJumping;
        [SerializeField] private TextMeshProUGUI playerIsClimbingText;
        [SerializeField] private TextMeshProUGUI playerCanGrappleText;
        [SerializeField] private TextMeshProUGUI playerIsSpikeActiveText;

        private PlayerController player;

        public void Initialize(PlayerController player) {
            this.player = player;
        }

        public void OnUpdate() {
            mousePositionText.text = "Mouse Position: " + Mouse.current.position.value.ToString();
            playerPositionText.text = "Player Position: " + player.transform.position.ToString();
            playerIsJumping.text = "Is player jumping? " + player.IsJumping.ToString();
            playerIsClimbingText.text = "Is player climbing? " + player.IsClimbing.ToString();
            playerCanGrappleText.text = "Can player grapple? " + player.CanGrapple.ToString();
            playerIsSpikeActiveText.text = "Is player spike active? " + player.IsSpikeActive.ToString();
        }
    }
}
