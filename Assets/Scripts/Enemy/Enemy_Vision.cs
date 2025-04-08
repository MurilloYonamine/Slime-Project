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
            if (body.state == ENEMY.State.Chase & (body.DistanceToTarget.x >= 18 || body.DistanceToTarget.x <= -18)){
                body.ChangeState(ENEMY.State.Return, "chasing enemy");
            }

            if(body.state == ENEMY.State.Patrol){
                circlecollider2D.enabled = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<PlayerController>(out PlayerController play) && body.state == ENEMY.State.Patrol){
                body.ChangeState(ENEMY.State.Chase, "chasing enemy");
                circlecollider2D.enabled = false;
            }
        }



    }
}