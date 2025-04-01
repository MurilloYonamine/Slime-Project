using UnityEngine;

public class MagicSeed : MonoBehaviour {
    private bool isFloating = false;
    private float floatSpeed = 2.0f;
    private float floatHeight = 0.5f;
    private Vector3 startPosition;

    void Start() {
        startPosition = transform.position;
    }

    void Update() {
        //if (isFloating) {
        //    float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        //    transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isFloating = true;
            return;
        }

        if (collision.gameObject.CompareTag("Player")) {
            GameManager.Instance.magicSeedCount++;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            GameManager.Instance.magicSeedCount++;
            Destroy(gameObject);
        }
    }
}

