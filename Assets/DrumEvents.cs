using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumEvents : MonoBehaviour {
    private GameObject collisionObject;
    private bool _isColliding = false;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enter");
        if (collision.gameObject.name == "MainShape")
        {
            collisionObject = collision.gameObject;
            collisionObject.transform.parent.GetComponent<KeyEvents>().Key_PressedEvent();
            _isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "MainShape")
        {
            _isColliding = false;
            collisionObject.transform.parent.GetComponent<KeyEvents>().Key_ReleaseEvent();
            collisionObject = null;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "MainShape")
        {
            _isColliding = true;
        }
    }

    internal GameObject GetCollisionObject()
    {
        return collisionObject;
    }

    internal bool IsColliding()
    {
        return _isColliding;
    }
}
