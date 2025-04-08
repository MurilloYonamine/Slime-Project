using System;
using System.Collections;
using AUDIO;
using ENEMYVISION;
using PLAYER;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ENEMY {

    public enum State{
        Patrol,
        Chase,
        Return,
        Dead,
        Attack
    }

    public class Enemy : MonoBehaviour {

        [SerializeField] private LayerMask groundLayer;

        [Header("Enemy Components")]
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidBody2D;
        private Color originalColor;
        private CircleCollider2D circlecollider2D;
        private Vector3 Startpoint;
        [HideInInspector]public Transform target;
        [SerializeField] private CircleCollider2D vision;
        [HideInInspector] public Vector2 DistanceToTarget;

        public State state = State.Patrol;

        [Header("Enemy Settings")]
        [SerializeField] private float maxHealth = 3f;
        [SerializeField] private float knockbackForce = 25f;
        [SerializeField] private float MoveSpeed = 5f;
        [SerializeField] private int NutritionalValue = 10;
        [SerializeField] public int Damage = 10;
        private float currentHealth;

        [HideInInspector] public GameObject player;

        private void Start() {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            circlecollider2D = GetComponent<CircleCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidBody2D = GetComponent<Rigidbody2D>();

            currentHealth = maxHealth;
            originalColor = spriteRenderer.color;
            Startpoint = transform.localPosition;
            circlecollider2D.enabled = false;
        }

        private void Update(){
            if (state == State.Patrol){
                if(IsFacingRight()){
                    rigidBody2D.linearVelocity = new Vector2(MoveSpeed,0f);
                }else{
                    rigidBody2D.linearVelocity = new Vector2(-MoveSpeed,0f);
                }
            }
            else if(state == State.Chase){
                DistanceToTarget = transform.position - target.position;
                if(DistanceToTarget.x > 0){
                    transform.localScale = new Vector2(-1, transform.localScale.y);
                } else{
                    transform.localScale = new Vector2(1, transform.localScale.y);
                }
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x,transform.position.y), new Vector2(target.position.x,transform.position.y), MoveSpeed * Time.deltaTime);
                
            }else if (state == State.Return){
                DistanceToTarget = transform.position - Startpoint;
                if(DistanceToTarget.x > 0){
                    transform.localScale = new Vector2(-1, transform.localScale.y);
                } else{
                    transform.localScale = new Vector2(1, transform.localScale.y);
                }
                transform.position = Vector2.MoveTowards(transform.position, Startpoint, MoveSpeed * Time.deltaTime);
                if (transform.position.x == Startpoint.x){
                    ChangeState(State.Patrol, "Default");
                    
                }
            }
            else if (state == State.Dead){
                spriteRenderer.color = Color.blue;
                circlecollider2D.enabled = true;
                vision.enabled = false;
                
            }


        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent<PlayerController>(out PlayerController play) && state == State.Dead) {
                PlayerHealth fuck = play.playerHealth;
                fuck.HEALME(NutritionalValue);
                AudioManager.Instance.PlaySoundEffect("Audio/SFX/Slime/yummy_snack", volume: 0.1f);
                Destroy(gameObject);
            }
        
        }

        public void ChangeState(State name, String layername){
            state = name;
            gameObject.layer = LayerMask.NameToLayer(layername);
        }

        private bool IsFacingRight(){
            return transform.localScale.x > Mathf.Epsilon;
        }

        public void TakeDamage(float damage) {
            currentHealth -= damage;

            StartCoroutine(FlashWhite());
            AudioManager.Instance.PlaySoundEffect("Audio/SFX/Enemy/hit_damage", volume: 1f);

            if (currentHealth <= 0) Die();

            if (player != null) {
                Vector3 knockbackDirection = (transform.position - player.transform.position).normalized;
                rigidBody2D.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }

        private IEnumerator FlashWhite() {
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = originalColor;
        }

        private void Die() => ChangeState(State.Dead, "dead enemy");

        private void OnCollisionEnter2D(Collision2D collision2D) {
            if (!(groundLayer == (groundLayer | (1 << collision2D.gameObject.layer))) && state == State.Patrol) {
                TurnAround();
            }
        }

        public void TurnAround(){
            transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x), transform.localScale.y);
        }

    }
}
