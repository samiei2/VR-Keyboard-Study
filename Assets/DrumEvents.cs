using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumEvents : MonoBehaviour {
    private GameObject collisionObject;
    private bool _isColliding = false;
    private Transform PrevOwner;
    private Vector3 movementDirection;
    private Vector3 drumHeadPrevPosition;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "MainShape")
        { 
            collisionObject = collision.gameObject;
            transform.position = collision.contacts[0].point;
            collisionObject.transform.parent.GetComponent<KeyEvents>().Key_PressedEvent();
            _isColliding = true;
            PrevOwner = transform.parent.parent;
            transform.parent.parent = null;
        }
    }

    public Vector3 GetMovementDirection()
    {
        return movementDirection;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "MainShape")
        {
            _isColliding = false;
            collisionObject = collision.gameObject;
            collisionObject.transform.parent.GetComponent<KeyEvents>().Key_ReleaseEvent();
            collisionObject = null;
            //transform.parent.parent = PrevOwner;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "MainShape")
        {
            _isColliding = true;
            transform.position = collision.contacts[0].point;
        }
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);
        if (drumHeadPrevPosition != null)
        {
            movementDirection = drumHeadPrevPosition - transform.position;
            //print(movementDirection.ToString("F4"));
            drumHeadPrevPosition = transform.position;
        }
        else
        {
            drumHeadPrevPosition = transform.position;
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
