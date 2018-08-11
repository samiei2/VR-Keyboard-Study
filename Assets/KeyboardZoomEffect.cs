﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardZoomEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        var keyInFocus = GetComponent<KeyboardLayout>().KeyInFocus;

        if (keyInFocus != null)
        {
            GetComponent<KeyboardLayout>().GetNeighboringKeys();
        }
	}
}
