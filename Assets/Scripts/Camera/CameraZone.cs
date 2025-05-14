using Cinemachine;
using UnityEngine;

namespace CAMERA {
    [RequireComponent(typeof(Collider2D))]
    public class CameraZone : MonoBehaviour {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private BoxCollider2D boxCollider2D;

        private void Awake() {
            if (virtualCamera == null) virtualCamera = GetComponent<CinemachineVirtualCamera>();
            if (boxCollider2D == null) boxCollider2D = GetComponent<BoxCollider2D>();

            boxCollider2D.isTrigger = true;
            boxCollider2D.size = CalculateCameraBounds();
        }
        private Vector2 CalculateCameraBounds() {
            float orthoSize = virtualCamera.m_Lens.OrthographicSize;
            float aspect = Camera.main.aspect;

            float height = orthoSize * 2f;
            float width = height * aspect;

            return new Vector2(width, height);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                virtualCamera.Priority = 1;

                GameManager.Instance.SetNewCheckpoint();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                virtualCamera.Priority = 0;

            }
        }
    }
}