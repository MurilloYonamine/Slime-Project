using ENEMY;
using PLAYER;
using UnityEngine;


namespace ENEMYATTACK{
    public class EnemyDamage : MonoBehaviour
    {


        [SerializeField] private Enemy body;
        [SerializeField] private float AttackDuration = 1f;
        private float AttackCurTime;

       private CircleCollider2D circlecollider2D;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            circlecollider2D = GetComponent<CircleCollider2D>(); 
            AttackCurTime = AttackDuration;
            circlecollider2D.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (body.state == ENEMY.State.Chase && 
            !(body.DistanceToTarget.x >= 1.1f || body.DistanceToTarget.x <= -1.1f) && 
            !(body.DistanceToTarget.y >= 1.1f || body.DistanceToTarget.y <= -1.1f)){
                
                body.ChangeState(ENEMY.State.Attack, "chasing enemy");
                
                
            }

            if (body.state == ENEMY.State.Attack && AttackCurTime > 0){
                circlecollider2D.enabled = true;
                AttackCurTime -= Time.deltaTime;
            } else if (body.state == ENEMY.State.Attack && AttackCurTime <= 0){
                circlecollider2D.enabled = false;
                AttackCurTime = AttackDuration;
                body.ChangeState(ENEMY.State.Chase, "chasing enemy");
            }

        }

         private void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<PlayerController>(out PlayerController play)){
                circlecollider2D.enabled = false;
                if(body.state == ENEMY.State.Attack){
                    body.ChangeState(ENEMY.State.Chase, "chasing enemy");
                    play.playerHealth.TakeDamage(body.Damage, this);

                }
            }
         }
    }
}