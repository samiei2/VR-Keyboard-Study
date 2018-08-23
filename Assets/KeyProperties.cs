using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyProperties : MonoBehaviour {
    public Material focusedMat;
    public Material pressedMat;
    public Material normalMat;
    public Material highlightMat;

    public KeyID ID;

    public bool IsPrintable => KeyboardLayout.PRINTABLEKEYS.ContainsKey(ID);
    public string KeyText = "";

    internal bool effectsEnabled;

    void Start()
    {
        if (focusedMat == null) 
            focusedMat = Resources.Load("FocusedKeyMaterial") as Material;
        if (normalMat == null)
            normalMat = Resources.Load("NormalKeyMaterial") as Material;
        if (pressedMat == null)
            pressedMat = Resources.Load("PressedKeyMaterial") as Material;
        if (highlightMat == null)
            highlightMat = Resources.Load("HighlightKeyMaterial") as Material;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void ResetToNormal()
    {
        transform.Find("HexCylinder").GetComponent<MeshRenderer>().material = normalMat;
    }
}
