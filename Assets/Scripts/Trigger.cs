using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Trigger
    
{
    public class Trigger : MonoBehaviour
    {
        bool alreadyTriggered = false;

        private void OnTriggerEnter(Collider other) 
        {
            if(!alreadyTriggered && other.tag == "Player")
            {
                alreadyTriggered = true;
            }
        }

        private void OnTriggerExit(Collider other) 
        {
            
        }
    }
}