using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Camera_Follow : MonoBehaviour
    {
        [SerializeField] Transform target;
        float speed = 100;

        private void Start()
        {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void LateUpdate()
        {
            transform.position = target.position;
            

            if(Input.GetKey(KeyCode.E))
            {
                transform.Rotate(-Vector3.up * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.up * Time.deltaTime * speed);
            }
        }
    }
}