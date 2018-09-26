using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyProperties : MonoBehaviour {
    public Material focusedMat;
    public Material pressedMat;
    public Material normalMat;
    public Material highlightMat;
    public Material invisibleMat;

    public KeyID ID;

    public bool IsPrintable { get { return KeyboardLayout.PRINTABLEKEYS.ContainsKey(ID); } }
    public string KeyText = "";

    internal bool effectsEnabled;

    bool isDwelling = false;
    bool doneDwelling = false;
    internal bool IsShiftOn;
    private Material shiftMat;
    private Material tempMat;

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
        if (invisibleMat == null)
            invisibleMat = Resources.Load("InvisibleMaterial") as Material;
        if (shiftMat == null)
            shiftMat = Resources.Load("ShiftMaterial") as Material;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void ResetToNormal()
    {
        transform.Find("MainShape").GetComponent<MeshRenderer>().material = normalMat;
    }

    internal void StartDwell(float dwellWaitTime)
    {
        if (!doneDwelling)
        {
            isDwelling = true;
            StartCoroutine(SelectLetter(this.transform, dwellWaitTime));
        }
    }

    internal void StopDwell()
    {
        isDwelling = false;
        doneDwelling = false;
        transform.GetComponent<KeyEvents>().Key_ReleaseEvent();
    }

    IEnumerator SelectLetter(Transform transform, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (isDwelling)
        {
            transform.GetComponent<KeyEvents>().Key_PressedEvent();
            isDwelling = false;
            doneDwelling = true;
        }
    }

    internal void EnableShift()
    {
        IsShiftOn = true;
        //transform.Find("MainShape").GetComponent<MeshRenderer>().material = shiftMat;
        tempMat = normalMat;
        normalMat = shiftMat;
    }

    internal void DisableShift()
    {
        //transform.Find("MainShape").GetComponent<MeshRenderer>().material = normalMat;
        IsShiftOn = false;
        normalMat = tempMat;
    }
}
