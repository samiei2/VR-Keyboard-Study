﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FitalyKeyboardLayout : KeyboardLayout {
    
    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    public override void CreateMainKeys()
    {
        GameObject hexPrefab = Resources.Load("BoxPrefab", typeof(GameObject)) as GameObject;

        keysDic.Add(KeyID.Q, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.W, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.E, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.R, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.T, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Y, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.U, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.I, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.O, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.P, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.A, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.S, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.D, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.F, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.G, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.H, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.J, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.K, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.L, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Z, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.X, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.C, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.V, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.B, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.N, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.M, Instantiate(hexPrefab) as GameObject);

        keysDic.Add(KeyID.Dot, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Backspace, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Comma, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Space, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Space2, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Shift, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Enter, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Next, Instantiate(hexPrefab) as GameObject);
    }

    public override void LayoutKeys()
    {
        // Middle Row
        keysDic[KeyID.Space].transform.localPosition = new Vector3(-1.65f - 1 * keyXDelta, 0, 0);
        keysDic[KeyID.Space].transform.localScale = new Vector3(2.1f, 1, 1);
        keysDic[KeyID.N].transform.localPosition = new Vector3(0, 0, 0);
        keysDic[KeyID.E].transform.localPosition = new Vector3(1.1f + 1 * keyXDelta, 0, 0);
        keysDic[KeyID.Space2].transform.localPosition = new Vector3(2.75f + 2 * keyXDelta, 0, 0);
        keysDic[KeyID.Space2].transform.localScale = new Vector3(2.1f, 1, 1);

        //Second Row
        keysDic[KeyID.F].transform.localPosition = new Vector3(-2.2f - 2 * keyXDelta, 1.1f + keyYDelta, 0);
        keysDic[KeyID.I].transform.localPosition = new Vector3(-1.1f - 1 * keyXDelta, 1.1f + keyYDelta, 0);
        keysDic[KeyID.T].transform.localPosition = new Vector3(0, 1.1f + keyYDelta, 0);
        keysDic[KeyID.A].transform.localPosition = new Vector3(1.1f + 1 * keyXDelta, 1.1f + keyYDelta, 0);
        keysDic[KeyID.L].transform.localPosition = new Vector3(2.2f + 2 * keyXDelta, 1.1f + keyYDelta, 0);
        keysDic[KeyID.Y].transform.localPosition = new Vector3(3.3f + 3 * keyXDelta, 1.1f + keyYDelta, 0);

        //First Row
        keysDic[KeyID.Z].transform.localPosition = new Vector3(-2.2f - 2 * keyXDelta, 2.2f + 2* keyYDelta, 0);
        keysDic[KeyID.V].transform.localPosition = new Vector3(-1.1f - 1 * keyXDelta, 2.2f + 2 * keyYDelta, 0);
        keysDic[KeyID.C].transform.localPosition = new Vector3(0, 2.2f + 2 * keyYDelta, 0);
        keysDic[KeyID.H].transform.localPosition = new Vector3(1.1f + 1 * keyXDelta, 2.2f + 2 * keyYDelta, 0);
        keysDic[KeyID.W].transform.localPosition = new Vector3(2.2f + 2 * keyXDelta, 2.2f + 2 * keyYDelta, 0);
        keysDic[KeyID.K].transform.localPosition = new Vector3(3.3f + 3 * keyXDelta, 2.2f + 2 * keyYDelta, 0);

        //Fourth Row
        keysDic[KeyID.G].transform.localPosition = new Vector3(-2.2f - 2 * keyXDelta, -1.1f - keyYDelta, 0);
        keysDic[KeyID.D].transform.localPosition = new Vector3(-1.1f - 1 * keyXDelta, -1.1f - keyYDelta, 0);
        keysDic[KeyID.O].transform.localPosition = new Vector3(0, -1.1f - keyYDelta, 0);
        keysDic[KeyID.R].transform.localPosition = new Vector3(1.1f + 1 * keyXDelta, -1.1f - keyYDelta, 0);
        keysDic[KeyID.S].transform.localPosition = new Vector3(2.2f + 2 * keyXDelta, -1.1f - keyYDelta, 0);
        keysDic[KeyID.B].transform.localPosition = new Vector3(3.3f + 3 * keyXDelta, -1.1f - keyYDelta, 0);

        //Fifth Row
        keysDic[KeyID.Q].transform.localPosition = new Vector3(-2.2f - 2 * keyXDelta, -2.2f - 2 * keyYDelta, 0);
        keysDic[KeyID.J].transform.localPosition = new Vector3(-1.1f - 1 * keyXDelta, -2.2f - 2 * keyYDelta, 0);
        keysDic[KeyID.U].transform.localPosition = new Vector3(0, -2.2f - 2 * keyYDelta, 0);
        keysDic[KeyID.M].transform.localPosition = new Vector3(1.1f + 1 * keyXDelta, -2.2f - 2 * keyYDelta, 0);
        keysDic[KeyID.P].transform.localPosition = new Vector3(2.2f + 2 * keyXDelta, -2.2f - 2 * keyYDelta, 0);
        keysDic[KeyID.X].transform.localPosition = new Vector3(3.3f + 3 * keyXDelta, -2.2f - 2 * keyYDelta, 0);

        // Sixth Row
        keysDic[KeyID.Dot].transform.localPosition = new Vector3(4.4f + 3 * keyXDelta, 0, 0);
        keysDic[KeyID.Shift].transform.localPosition = new Vector3(5.5f + 4 * keyXDelta, 0, 0);
        keysDic[KeyID.Backspace].transform.localPosition = new Vector3(0.55f, -3.5f - 3 * keyYDelta, 0);
        keysDic[KeyID.Backspace].transform.localScale = new Vector3(6.5f + 5* keyXDelta, 1, 1);
        keysDic[KeyID.Comma].transform.localPosition = new Vector3(-3.28f - 2 * keyXDelta, 0, 0);
        keysDic[KeyID.Enter].transform.localPosition = new Vector3(0.55f, 3.5f + 3 * keyYDelta, 0);
        keysDic[KeyID.Enter].transform.localScale = new Vector3(6.5f + 5* keyXDelta, 1, 1);
        keysDic[KeyID.Next].transform.localPosition = new Vector3(-4.38f - 3 * keyXDelta, 0, 0);
    }

    public override void SetProperties()
    {
        var normalMat = Resources.Load("NormalKeyMaterial") as Material; ;

        foreach (var key in keysDic.Keys)
        {
            keysDic[key].name = "Key_" + key;

            keysDic[key].transform.parent = this.transform;
            if (PRINTABLEKEYS.ContainsKey(key))
                keysDic[key].transform.Find("Text").GetComponent<TextMeshPro>().text = PRINTABLEKEYS[key].ToString().ToUpper();
            keysDic[key].AddComponent<KeyEvents>();
            keysDic[key].AddComponent<KeyProperties>();
            if (PRINTABLEKEYS.ContainsKey(key))
                keysDic[key].GetComponent<KeyProperties>().KeyText = PRINTABLEKEYS[key].ToString();
            keysDic[key].GetComponent<KeyProperties>().ID = key;

            keysDic[key].transform.Find("MainShape").GetComponent<MeshRenderer>().material = normalMat;


            keysDic[key].GetComponent<KeyEvents>().KeyEvents_OnKeyFocused += KeyboardEventHandler_OnFocusedHandler;
            keysDic[key].GetComponent<KeyEvents>().KeyEvents_OnKeyUnfocused += KeyboardEventHandler_OnUnfocusedHandler;
            keysDic[key].GetComponent<KeyEvents>().KeyEvents_OnKeyPressed += KeyboardEventHandler_OnPressedHandler;
            keysDic[key].GetComponent<KeyEvents>().KeyEvents_OnKeyReleased += KeyboardEventHandler_OnReleasedHandler;
        }

        keysDic[KeyID.Backspace].transform.Find("Text").GetComponent<TextMeshPro>().rectTransform.localScale = new Vector3(0.5f,2,1);
        keysDic[KeyID.Backspace].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 1.5f;
        keysDic[KeyID.Backspace].transform.Find("Text").GetComponent<TextMeshPro>().text = "Backspace";
        keysDic[KeyID.Enter].transform.Find("Text").GetComponent<TextMeshPro>().rectTransform.localScale = new Vector3(0.5f, 2, 1);
        keysDic[KeyID.Enter].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 2f;
        keysDic[KeyID.Enter].transform.Find("Text").GetComponent<TextMeshPro>().text = "Enter";

        keysDic[KeyID.Shift].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 1.9f;
        keysDic[KeyID.Shift].transform.Find("Text").GetComponent<TextMeshPro>().text = "Shift";
        keysDic[KeyID.Next].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 1.9f;
        keysDic[KeyID.Next].transform.Find("Text").GetComponent<TextMeshPro>().text = "Next";
    }

    public override void KeyboardEventHandler_OnReleasedHandler(object sender, KeyEventArgs args)
    {
        var transform = (Transform)sender;
        transform.Find("MainShape").GetComponent<MeshRenderer>().material = transform.GetComponent<KeyProperties>().normalMat;
    }

    public override void KeyboardEventHandler_OnPressedHandler(object sender, KeyEventArgs args)
    {
        var transform = (Transform)sender;
        transform.Find("MainShape").GetComponent<MeshRenderer>().material = transform.GetComponent<KeyProperties>().pressedMat;
        SaveDataModule.Instance.WriteToTimeLine("KeyPressed: " + Enum.GetName(typeof(KeyID), args.KeyId));
        if (args.KeyPrintable)
        {
            if (textArea != null)
            {
                OnKeyPressed(sender, args);
                if (keysDic[KeyID.Shift].GetComponent<KeyProperties>().IsShiftOn)
                {
                    textArea.GetComponent<TextMeshPro>().text += args.KeyText.ToUpper();
                }
                else
                    textArea.GetComponent<TextMeshPro>().text += args.KeyText;
            }
        }
        else
        {
            HandleNonPrintable(sender,args);
        }
    }

    public override void KeyboardEventHandler_OnUnfocusedHandler(object sender, KeyEventArgs args)
    {

        var transform = (Transform)sender;
        //transform.Find("MainShape").GetComponent<MeshRenderer>().material = transform.GetComponent<KeyProperties>().normalMat;
        transform.Find("Tint").gameObject.SetActive(false);
        if (zoomEffect)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (dwell)
        {
            transform.GetComponent<KeyProperties>().StopDwell();
        }
    }

    public override void KeyboardEventHandler_OnFocusedHandler(object sender, KeyEventArgs args)
    {

        var transform = (Transform)sender;
        //transform.Find("MainShape").GetComponent<MeshRenderer>().material = transform.GetComponent<KeyProperties>().focusedMat;
        transform.Find("Tint").gameObject.SetActive(true);
        if (zoomEffect)
        {
            transform.localScale *= 1.5f;
            //ChangeClosebyKeys(transform);
        }

        if (dwell)
        {
            transform.GetComponent<KeyProperties>().StartDwell(dwellWaitTime);
        }
    }

    public override void HighlightKeys(List<char> suggestedAlphabet)
    {
        if (GetComponent<KeyHighlightEffect>() != null && suggestionEnabled)
            GetComponent<KeyHighlightEffect>().HighlightKeys(suggestedAlphabet);
    }
    

    private void HandleNonPrintable(object sender, KeyEventArgs args)
    {
        var transform = (Transform)sender;
        if (args.KeyId == KeyID.Backspace)
        {
            if (textArea.GetComponent<TextMeshPro>().text.Length > 0)
                textArea.GetComponent<TextMeshPro>().text =
                    textArea.GetComponent<TextMeshPro>().text.Remove(textArea.GetComponent<TextMeshPro>().text.Length - 1, 1);
        }
        else if (args.KeyId == KeyID.Enter)
        {
            textArea.GetComponent<TextMeshPro>().text += '\n';
        }
        else if (args.KeyId == KeyID.Shift)
        {
            if (keysDic[KeyID.Shift].GetComponent<KeyProperties>().IsShiftOn)
            {
                keysDic[KeyID.Shift].GetComponent<KeyProperties>().DisableShift();
            }
            else
                keysDic[KeyID.Shift].GetComponent<KeyProperties>().EnableShift();
        }
    }

    public override void ScaleToVRDeskPosition()
    {
        transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        transform.position = VRDesk.position - new Vector3(0, 0.45f, -0.18f);
        transform.eulerAngles = VRDesk.eulerAngles;
        //screenArea.transform.LookAt(MainCamera);
        screenArea.transform.localPosition = new Vector3(0, 5.64f, -1.6f);
        screenArea.transform.localEulerAngles = new Vector3(-50, 0, 0);
    }

    public override void ScaleToFrontViewPosition()
    {
        //transform.eulerAngles = Vector3.zero;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //transform.position = new Vector3(0, 0, MainCamera.position.z + keyboardDistanceFromCamera);
        transform.position = VRWall.position - new Vector3(0, 0.2f, -0.3f);
        transform.eulerAngles = VRWall.eulerAngles;
        //transform.localScale = Vector3.one;
        screenArea.transform.LookAt(MainCamera);
        screenArea.transform.localPosition = new Vector3(0, 7.24f, -1.88f);
        screenArea.transform.localEulerAngles = new Vector3(-18f, 0f, 0);
    }
}
