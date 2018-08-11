using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyboardEventHandler : MonoBehaviour {
    GameObject textArea;
	// Use this for initialization
	void Start () {
        textArea =  GameObject.Find("TextArea");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void KeyFocused(GameObject key)
    {
        GetComponent<KeyboardLayout>().KeyInFocus = key;
    }

    internal void KeyPressed(string key,bool isPrintable)
    {
        if (isPrintable)
        {
            if (textArea != null)
            {
                textArea.GetComponent<TextMeshPro>().text += key;
            }
        }
        else
        {

        }
    }

    internal void KeyUnfocused()
    {
        GetComponent<KeyboardLayout>().KeyInFocus = null;
    }
}
