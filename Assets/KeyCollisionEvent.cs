using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollisionEvent : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        print("velp: " + collision.impulse.magnitude.ToString("F4"));
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
