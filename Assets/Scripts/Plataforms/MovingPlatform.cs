using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace platshenanigans {
    public class MovingPlatform : MonoBehaviour {

        private enum PlatformType {
            ReleasingWall,    // parede que solta
            Disappearing,     // que some
            Toggleable,       // on/off
            Explosive         // explode
        };

        private PlatformType currentPlatformType;

        [SerializeField] private Transform pointA;
        [SerializeField] private Transform pointB;
        [SerializeField] private float moveSpeed = 2f;

        [SerializeField] private Vector3 NextPosition;

        private void Start() {
            NextPosition = pointA.position;
        }

        private void Update() {
            transform.position = Vector2.MoveTowards(transform.position, NextPosition, moveSpeed * Time.deltaTime);

            if (transform.position == NextPosition) {
                NextPosition = (NextPosition == pointA.position) ? pointB.position : pointA.position;
            }
        }

        private void HandlePlatformBehavior() {
            switch (currentPlatformType) {
                case PlatformType.ReleasingWall: ReleaseWall(); break;
                case PlatformType.Disappearing: Disappearing(); break;
                case PlatformType.Toggleable: Toggleable(); break;
                case PlatformType.Explosive: Explosive(); break;
                default: Debug.LogError("Unknown platform type"); break;
            }
        }
        private void ReleaseWall() {

        }
        private void Disappearing() {

        }

        private void Toggleable() {
        }
        private void Explosive() {
        }


        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Player")) {
                collision.gameObject.transform.parent = transform;
            }
        }
        private void OnCollisionExit2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Player")) {
                collision.gameObject.transform.parent = GameManager.Instance.PlayerOriginalLayer;
            }
        }

    }
}