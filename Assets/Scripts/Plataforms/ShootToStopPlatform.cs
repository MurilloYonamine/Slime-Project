using PLATFORMS;
using PLAYER;
using UnityEditor;
using UnityEngine;
namespace PLATFORMS{
public class ShootToStopPlatform : Platform
{


    [HideInInspector] private bool ison = true;
    [HideInInspector] private float oldspeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        oldspeed = moveSpeed;
    }

    // Update is called once per frame


    private void OnTriggerEnter2D(Collider2D collider2D) {

        if (collider2D.TryGetComponent<PlayerBullet>(out PlayerBullet bullet)) {
                CHANGE();
            }
        }

    void CHANGE(){
        ison = !ison;
            if (!ison){
                moveSpeed = 0;
            } else{
                moveSpeed = oldspeed;
            }
    }

    }
}