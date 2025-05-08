using PLAYER;
using UnityEngine;

namespace PLATFORMS {
    public class ColorPlatform : Platform{

        [SerializeField] bool isactive = false;
        private BoxCollider2D boxcollider2D;
        private float oldspeed;

        protected override void Start()
        {
            base.Start();
            oldspeed = moveSpeed;
            boxcollider2D = GetComponent<BoxCollider2D>();
             if (!isactive){
                moveSpeed = 0;
                boxcollider2D.enabled = false;
            } else{
                moveSpeed = oldspeed;
                boxcollider2D.enabled = true;
            }
        }
        protected override void Update()
        {
            base.Update();

        }
        


         public void CHANGE()
         {
            isactive = !isactive;
            if (!isactive){
                moveSpeed = 0;
                boxcollider2D.enabled = false;
            } else{
                moveSpeed = oldspeed;
                boxcollider2D.enabled = true;
            }
        }




    
    }
}