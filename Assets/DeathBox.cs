using PLAYER;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DEATHBOX{
    public class DeathBox : MonoBehaviour
    {
        private BoxCollider2D boxcollider2D;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            boxcollider2D = GetComponent<BoxCollider2D>();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerController>(out PlayerController play)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}