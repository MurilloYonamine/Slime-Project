using PLAYER;
using UnityEngine;

namespace PLATFORMS {
    public class ColorPlatform : Platform{

        [SerializeField] bool isactive = false;
        private float oldspeed;

        protected override void Start()
        {
            base.Start();
            oldspeed = moveSpeed;
             if (!isactive){
                moveSpeed = 0;
            } else{
                moveSpeed = oldspeed;

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
            } else{
                moveSpeed = oldspeed;
            }
        }




    
    }
}