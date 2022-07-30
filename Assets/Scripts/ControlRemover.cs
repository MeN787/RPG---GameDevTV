using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Trigger
{
    public class ControlRemover : MonoBehaviour
    {
        GameObject player;

        private void Start() 
        {
            player = GameObject.FindWithTag("Player");    
        }
        

        void DisableControl()
        {
            print("DisableControl");
            
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerInputController>().enabled = false;
        }

        void EnableControl()
        {
            print("EnableControl");
            player.GetComponent<PlayerInputController>().enabled = true;
        }
    }
}