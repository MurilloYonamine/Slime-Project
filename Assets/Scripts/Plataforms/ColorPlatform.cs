using PLAYER;
using UnityEngine;

namespace PLATFORMS {
    public class ColorPlatform : Platform{

        [SerializeField] public bool isactive = false;
        public float oldspeed;

        protected override void Start(){
            base.Start();
            oldspeed = moveSpeed;
             if (!isactive){
                moveSpeed = 0;
            } else{
                moveSpeed = oldspeed;

            }
        }
        protected override void Update(){
            base.Update();

        }
         public void CHANGE(){
            isactive = !isactive;
            if (!isactive){
                moveSpeed = 0;
            } else{
                moveSpeed = oldspeed;
            }
        }    
    }
}