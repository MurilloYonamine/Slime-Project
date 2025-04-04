using ENEMY;
using PLAYER;
using Unity.VisualScripting;
using UnityEngine;

namespace ENEMYVISION{
    public class Enemy_Vision : MonoBehaviour
    {
        [SerializeField] Enemy body;

       private CircleCollider2D circlecollider2D;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            circlecollider2D = GetComponent<CircleCollider2D>();
            circlecollider2D.enabled = true;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<PlayerController>(out PlayerController play)){
                body.ChangeState(ENEMY.State.Chase, "chasing enemy");
                circlecollider2D.radius = 8;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerController>(out PlayerController play) && body.state == ENEMY.State.Chase){
                body.ChangeState(ENEMY.State.Return, "chasing enemy");
                circlecollider2D.radius = 4;
            }
        }


    }
}