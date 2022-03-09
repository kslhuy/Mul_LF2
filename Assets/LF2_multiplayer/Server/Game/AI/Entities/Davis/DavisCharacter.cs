using System;
using System.Collections.Generic;
using UnityEngine;

namespace LF2.Server{
    class DavisCharacter : ServerCharacter 
    {
        DavisBrain davisBrain;
        private void Start (){
            
            davisBrain = new DavisBrain(this);
        
        }
        private void Update()
        {
            davisBrain.Update() ;
                
        }
        private void FixedUpdate() {
            
        }
        public override void OnNetworkSpawn()
        {
        }

        public override void OnNetworkDespawn()
        {
        }
        
    }

}
