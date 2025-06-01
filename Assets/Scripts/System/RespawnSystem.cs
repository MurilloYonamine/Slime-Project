using CAMERA;
using PLATFORMS;
using PLAYER;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SYSTEM {
    [Serializable]
    public class RespawnSystem {
        private GameObject player;
        [SerializeField] private List<MonoBehaviour> resettablePlatforms;

        public RespawnSystem(GameObject player) {
            this.player = player;
            resettablePlatforms = new List<MonoBehaviour>();
            foreach (var mono in GameObject.FindObjectsOfType<MonoBehaviour>()) {
                if (mono is IResettablePlatform)
                    resettablePlatforms.Add(mono);
            }
        }

        // M�todo que ativa a c�mera do checkpoint e inicia a transi��o de morte para respawnar o jogador.
        public void RespawnPlayer(int checkpointIndex, Vector3 respawnPosition) {
            CameraManager.Instance.ActivateCamera(checkpointIndex);

            if (!TransitionManager.Instance.IsTransitioning) {
                player.GetComponent<PlayerController>().TogglePlayerInput();
                GameManager.Instance.StartCoroutine(
                    TransitionManager.Instance.PlayDeathTransition(() =>
                        GameManager.Instance.StartCoroutine(RespawnSequence(respawnPosition))
                    )
                );
            }
        }

        // Coroutine que executa o respawn ap�s a transi��o esconder a tela
        private IEnumerator RespawnSequence(Vector3 respawnPosition) {
            yield return new WaitForSeconds(0.5f);

            player.GetComponent<PlayerController>().UpdateHealth();

            foreach (var platform in resettablePlatforms) {
                if (platform is IResettablePlatform resettable)
                    resettable.ResetPlatform();
            }

            player.transform.position = respawnPosition;
            player.GetComponent<PlayerController>().TogglePlayerInput();
        }
    }
}