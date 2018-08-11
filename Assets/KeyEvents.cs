﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvents : MonoBehaviour {

    public void Key_FocusedEvent()
    {
        transform.Find("HexCylinder").GetComponent<MeshRenderer>().material = transform.GetComponent<KeyProperties>().focusedMat;
        transform.parent.GetComponent<KeyboardEventHandler>().KeyFocused(gameObject);
    }

    public void Key_UnfocusedEvent()
    {
        transform.Find("HexCylinder").GetComponent<MeshRenderer>().material = transform.GetComponent<KeyProperties>().normalMat;
        transform.parent.GetComponent<KeyboardEventHandler>().KeyUnfocused();
    }

    public void Key_PressedEvent()
    {
        transform.Find("HexCylinder").GetComponent<MeshRenderer>().material = transform.GetComponent<KeyProperties>().pressedMat;
        transform.parent.GetComponent<KeyboardEventHandler>().KeyPressed(
            transform.GetComponent<KeyProperties>().keyText,
            transform.GetComponent<KeyProperties>().isPrintable);
    }

    public void Key_RealseEvent()
    {
        transform.Find("HexCylinder").GetComponent<MeshRenderer>().material = transform.GetComponent<KeyProperties>().normalMat;
    }
}
