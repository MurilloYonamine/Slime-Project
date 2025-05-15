using UnityEngine;

namespace PLATFORMS {
    public class Platform : MonoBehaviour {

        [SerializeField] private Transform pointA;
        [SerializeField] private Transform pointB;
        [SerializeField] public float moveSpeed = 2f;

        [SerializeField] private Vector3 NextPosition;

        protected virtual void Start() {
            NextPosition = pointA.position;
        }

        protected virtual void Update() {
            transform.position = Vector2.MoveTowards(transform.position, NextPosition, moveSpeed * Time.deltaTime);

            if (transform.position == NextPosition)
                NextPosition = (NextPosition == pointA.position) ? pointB.position : pointA.position;
        }
        protected virtual void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Player"))
                collision.gameObject.transform.parent = transform;
        }
        protected virtual void OnCollisionExit2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Player"))
                collision.gameObject.transform.parent = GameManager.Instance.PlayerOriginalLayer;
        }

    }
}