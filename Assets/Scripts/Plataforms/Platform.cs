using UnityEngine;

namespace PLATFORMS {
    public class Platform : MonoBehaviour {

        [SerializeField] protected Transform pointA;
        [SerializeField] protected Transform pointB;
        [SerializeField] public float moveSpeed = 2f;

        [SerializeField] protected Vector3 NextPosition;

        protected Vector3 initialPosition;

        protected virtual void Start() {
            initialPosition = transform.position;
            NextPosition = pointA != null ? pointA.position : transform.position;
        }

        protected virtual void Update() {
            transform.position = Vector2.MoveTowards(transform.position, NextPosition, moveSpeed * Time.deltaTime);

            if (transform.position == NextPosition)
                NextPosition = (NextPosition == pointA.position) ? pointB.position : pointA.position;
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Player")) {
                Transform playerTransform = collision.gameObject.transform;

                playerTransform.SetParent(transform, true);

                Debug.Log("Player entrou na plataforma. Scale atual: " + playerTransform.localScale);
            }
        }

        protected virtual void OnCollisionExit2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Player")) {
                Transform playerTransform = collision.gameObject.transform;

                playerTransform.SetParent(GameManager.Instance.PlayerOriginalLayer, true);

                Debug.Log("Player saiu da plataforma. Scale atual: " + playerTransform.localScale);
            }
        }

        public virtual void ResetToInitialPosition() {
            transform.position = initialPosition;
        }
    }
}
