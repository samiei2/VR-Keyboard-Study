using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardZoomEffect : MonoBehaviour {
    public event KeyEvents.KeyEvent ZoomInEvent;
    public event KeyEvents.KeyEvent ZoomOutEvent;
    public void Apply()
    {
        transform.localScale *= 2;
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        var keyInFocus = GetComponent<KeyboardLayout>().KeyInFocus;

        if (keyInFocus != null)
        {
            //GetComponent<KeyboardLayout>().GetNeighboringKeys();
        }
	}
}
