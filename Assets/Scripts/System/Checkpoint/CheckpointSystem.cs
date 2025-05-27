using CAMERA;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SYSTEM.CHECKPOINT {
    [Serializable]
    public class CheckpointSystem {
        [SerializeField] private int currentCheckpoint;
        [SerializeField] private List<GameObject> checkpointList;

        // Inicializa o checkpoint atual com o índice da câmera ativa.
        public void Initialize() => currentCheckpoint = CameraManager.Instance.GetActiveCameraIndex();
        

        // Altera o checkpoint atual para o índice do checkpoint fornecido e ativa a câmera correspondente.
        public void ChangeCheckpoint(GameObject checkpoint) {
            int index = FindCheckpointIndex(checkpoint);
            if (index >= 0) {
                currentCheckpoint = index;
                CameraManager.Instance.ActivateCamera(index);
            }
        }

        // Retorna a posição do checkpoint atual, sincronizando com o índice do CameraManager.
        public Vector3 GetCheckpointPosition() {
            currentCheckpoint = CameraManager.Instance.CurrentCheckpointIndex;
            if (currentCheckpoint >= 0 && currentCheckpoint < checkpointList.Count) {
                return checkpointList[currentCheckpoint].transform.position;
            }
            return Vector3.zero;
        }

        // Altera o checkpoint atual para o índice especificado e ativa a câmera correspondente.
        public void ChangeCheckpointByIndex(int index) {
            if (index >= 0 && index < checkpointList.Count) {
                currentCheckpoint = index;
                CameraManager.Instance.ActivateCamera(index);
            }
        }
        
        public int GetCurrentCheckpointIndex() => currentCheckpoint = CameraManager.Instance.CurrentCheckpointIndex; // Retorna e sincroniza o índice do checkpoint atual com o CameraManager.
        public int FindCheckpointIndex(GameObject checkpoint) => checkpointList.IndexOf(checkpoint); // Retorna o índice do checkpoint fornecido na lista.
        public int Count => checkpointList.Count; // Retorna a quantidade de checkpoints na lista.
    }
}
