using UnityEngine;
using PLAYER;

public class BirdDamageTest : MonoBehaviour
{
    public PlayerHealth pHealth;
    public float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            pHealth.Health -= damage;
        }
    }

}
