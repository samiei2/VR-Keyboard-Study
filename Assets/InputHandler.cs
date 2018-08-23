using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {
    public InputType SourceType;
    private GameObject objectInFocus;
    private bool inKeyPress;
    public bool RepeatedKeyPressEnabled;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (SourceType == InputType.Mouse)
            HandleMouseInput();
        else if (SourceType == InputType.Tobii)
            HandleTobiiInput();
	}

    private void HandleTobiiInput()
    {
        throw new NotImplementedException();
    }

    private void HandleMouseInput()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if(hit.transform.name.Contains("Cylinder"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //Debug.Log("Mouse down on the " + hit.transform.parent.name);
                    inKeyPress = true;
                    hit.transform.parent.GetComponent<KeyEvents>().Key_PressedEvent();
                    StartCoroutine("RepeatKeyPress");
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    //Debug.Log("Mouse up on the " + hit.transform.parent.name);
                    StopCoroutine("RepeatKeyPress");
                    inKeyPress = false;
                    hit.transform.parent.GetComponent<KeyEvents>().Key_RealseEvent();
                }
                else
                {
                    if (!inKeyPress)
                    {
                        // There is a bug in visual update and we have to do the following 
                        foreach (Transform child in transform)
                        {
                            if(child != hit.transform.gameObject)
                                if(child != null)
                                    if(child.GetComponent<KeyEvents>()!=null)
                                        child.GetComponent<KeyEvents>().Key_UnfocusedEvent();
                        }
                        //////////////////////////////////////////////////////////////
                    
                        hit.transform.parent.GetComponent<KeyEvents>().Key_FocusedEvent();
                        objectInFocus = hit.transform.gameObject;
                    }
                }
            }
        }
        else
        {
            if (inKeyPress)
            {
                StopCoroutine("RepeatKeyPress");
                inKeyPress = false;
                objectInFocus.transform.parent.GetComponent<KeyEvents>().Key_RealseEvent();
            }
            if (objectInFocus != null)
            {
                if (objectInFocus.name.Contains("HexCylinder"))
                {
                    //Debug.Log("You realsed the " + objectInFocus.transform.parent.name);
                    objectInFocus.transform.parent.GetComponent<KeyEvents>().Key_UnfocusedEvent();
                    objectInFocus = null;
                    // There is a bug in visual update and we have to do the following 
                    foreach (Transform child in transform)
                    {
                        child.GetComponent<KeyEvents>().Key_UnfocusedEvent();
                    }
                    //////////////////////////////////////////////////////////////
                }
            }
        }
    }

    public IEnumerator RepeatKeyPress()
    {
        if (RepeatedKeyPressEnabled) {
            yield return new WaitForSeconds(0.7f); // initial wait
            while (inKeyPress)
            {
                if (objectInFocus != null)
                {
                    objectInFocus.transform.parent.GetComponent<KeyEvents>().Key_PressedEvent();
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
    }
}

public enum InputType
{
    Tobii,
    Mouse, 
    ViveController, 
};
