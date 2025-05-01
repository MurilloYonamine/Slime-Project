using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


namespace platshenanigans{
public class MovingPlatform : MonoBehaviour
    {
        public Transform pointA;
        public Transform pointB;

        public float moveSpeed = 2f;

        private Vector3 NextPosition;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            NextPosition = pointA.position;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, NextPosition, moveSpeed*Time.deltaTime);

            if(transform.position == NextPosition){
                NextPosition = (NextPosition == pointA.position) ? pointB.position : pointA.position;
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Player")){
                collision.gameObject.transform.parent = transform;
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Player")){
                collision.gameObject.transform.parent = GameManager.Instance.PlayerOriginalLayer;
            }
        }

    }
}