using CAMERA;
using PLATFORMS;
using PLAYER;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SYSTEM {
    [Serializable]
    public class RespawnSystem {
        private GameObject player;
        [SerializeField] private List<ShootToStopPlatform> shootToStopPlatforms;

        // Construtor que inicializa o sistema de respawn com o jogador fornecido.
        public RespawnSystem(GameObject player) => this.player = player;

        // M�todo que ativa a c�mera do checkpoint e inicia a transi��o de morte para respawnar o jogador.
        public void RespawnPlayer(int checkpointIndex, Vector3 respawnPosition) {
            CameraManager.Instance.ActivateCamera(checkpointIndex);

            if (!TransitionManager.Instance.IsTransitioning) {
                GameManager.Instance.StartCoroutine(
                    TransitionManager.Instance.PlayDeathTransition(() =>
                        ExecuteRespawn(respawnPosition)
                    )
                );
            }
        }

        // M�todo privado que restaura a vida do jogador, reinicia plataformas e move o jogador para a posi��o de respawn.
        private void ExecuteRespawn(Vector3 respawnPosition) {
            player.GetComponent<PlayerController>().UpdateHealth();

            if (shootToStopPlatforms != null && shootToStopPlatforms.Count > 0) {
                foreach (var platform in shootToStopPlatforms) {
                    platform.ison = true;
                    platform.moveSpeed = platform.oldspeed;
                }
            }
            player.transform.position = respawnPosition;
        }
    }
}