using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace CAMERA {
    public class CameraManager : MonoBehaviour {
        public static CameraManager Instance { get; private set; }

        [SerializeField] private List<CinemachineVirtualCamera> virtualCamerasList;
        [field: SerializeField] public int CurrentCheckpointIndex { get; private set; }

        private void Awake() {
            if (Instance == null) Instance = this;
            else DestroyImmediate(gameObject);
        }

        private void Start() => CurrentCheckpointIndex = GetActiveCameraIndex();

        /// Ativa a c�mera virtual no �ndice especificado, definindo sua prioridade para 1 e as demais para 0. Atualiza o �ndice do checkpoint atual.
        public void ActivateCamera(int index) {
            for (int i = 0; i < virtualCamerasList.Count; i++) virtualCamerasList[i].Priority = (i == index) ? 1 : 0;
            
            CurrentCheckpointIndex = index;
        }

        // Retorna o �ndice da c�mera virtual atualmente ativa (com prioridade 1). Se nenhuma estiver ativa, retorna 0.
        public int GetActiveCameraIndex() {
            for (int i = 0; i < virtualCamerasList.Count; i++) {
                if (virtualCamerasList[i].Priority == 1) return i;
            }
            return 0;
        }
        // Retorna o �ndice de uma c�mera virtual espec�fica na lista.
        public int GetVirtualCameraIndex(CinemachineVirtualCamera cam) => virtualCamerasList.IndexOf(cam);

        // Retorna o transform do checkpoint correspondente ao �ndice do checkpoint atual.
        public Transform ChangeCheckpoint(List<GameObject> checkpoints) => checkpoints[CurrentCheckpointIndex].transform;

    }
}