﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class DrumsKeyboardLayout : KeyboardLayout
{
    public override void CreateMainKeys()
    {
        GameObject hexPrefab = Resources.Load("CylinderBigPrefab", typeof(GameObject)) as GameObject;

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
        keysDic.Add(KeyID.Shift, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Enter, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Next, Instantiate(hexPrefab) as GameObject);
    }


    public override void HighlightKeys(List<char> suggestedAlphabet)
    {
    }

    public override void KeyboardEventHandler_OnFocusedHandler(object sender, KeyEventArgs args)
    {
        var transform = (Transform)sender;


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

    public override void KeyboardEventHandler_OnPressedHandler(object sender, KeyEventArgs args)
    {
        var transform = (Transform)sender;

        transform.Find("MainShape").GetComponent<MeshRenderer>().material = transform.GetComponent<KeyProperties>().pressedMat;

        if (args.KeyPrintable)
        {
            if (textArea != null)
            {
                OnKeyPressed(sender, args);
                textArea.GetComponent<TextMeshPro>().text += args.KeyText;
            }
        }
        else
        {
            HandleNonPrintable(sender, args);
        }
    }

    public override void KeyboardEventHandler_OnReleasedHandler(object sender, KeyEventArgs args)
    {
        var transform = (Transform)sender;
        transform.Find("MainShape").GetComponent<Renderer>().material = transform.GetComponent<KeyProperties>().normalMat;
    }

    public override void KeyboardEventHandler_OnUnfocusedHandler(object sender, KeyEventArgs args)
    {
        var transform = (Transform)sender;

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

    public override void LayoutKeys()
    {
        foreach (var item in keysDic)
        {
            item.Value.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        
        // Top Row
        keysDic[KeyID.Q].transform.position = new Vector3(-2.5f, 0.6f, 0.65f);
        keysDic[KeyID.W].transform.position = new Vector3(-1.85f, 0.6f, 0.65f);
        keysDic[KeyID.E].transform.position = new Vector3(-1.2f, 0.6f, 0.65f);
        keysDic[KeyID.R].transform.position = new Vector3(-0.55f, 0.6f, 0.65f);
        keysDic[KeyID.T].transform.position = new Vector3(-0.2f, 0.6f, 0.65f);
        keysDic[KeyID.Y].transform.position = new Vector3(0.15f, 0.6f, 0.65f);
        keysDic[KeyID.U].transform.position = new Vector3(0.8f, 0.6f, 0.65f);
        keysDic[KeyID.I].transform.position = new Vector3(1.45f, 0.6f, 0.65f);
        keysDic[KeyID.O].transform.position = new Vector3(2.1f, 0.6f, 0.65f);
        keysDic[KeyID.P].transform.position = new Vector3(2.75f, 0.6f, 0.65f);
        keysDic[KeyID.Backspace].transform.position = new Vector3(3.4f, 0.6f, 0.65f);



        // Mid Row
        keysDic[KeyID.A].transform.position = new Vector3(-2.3f, 0f, 0f);
        keysDic[KeyID.S].transform.position = new Vector3(-1.65f, 0f, 0f);
        keysDic[KeyID.D].transform.position = new Vector3(-1.0f, 0f, 0f);
        keysDic[KeyID.F].transform.position = new Vector3(-0.35f, 0f, 0f);
        keysDic[KeyID.G].transform.position = new Vector3(0f, 0f, 0f);
        keysDic[KeyID.H].transform.position = new Vector3(0.35f, 0f, 0f);
        keysDic[KeyID.J].transform.position = new Vector3(1.0f, 0f, 0f);
        keysDic[KeyID.K].transform.position = new Vector3(1.65f, 0f, 0f);
        keysDic[KeyID.L].transform.position = new Vector3(2.3f, 0f, 0f);
        keysDic[KeyID.Enter].transform.position = new Vector3(2.95f, 0f, 0f);


        // Bottom Row
        keysDic[KeyID.Z].transform.position = new Vector3(-2.4f, -0.6f, -0.65f);
        keysDic[KeyID.X].transform.position = new Vector3(-1.75f, -0.6f, -0.65f);
        keysDic[KeyID.C].transform.position = new Vector3(-1.1f, -0.6f, -0.65f);
        keysDic[KeyID.V].transform.position = new Vector3(-0.45f, -0.6f, -0.65f);
        keysDic[KeyID.B].transform.position = new Vector3(0.2f, -0.6f, -0.65f);
        keysDic[KeyID.N].transform.position = new Vector3(0.85f, -0.6f, -0.65f);
        keysDic[KeyID.M].transform.position = new Vector3(1.5f, -0.6f, -0.65f);

        keysDic[KeyID.Dot].transform.position = new Vector3(2.15f, -0.6f, -0.65f);
        keysDic[KeyID.Comma].transform.position = new Vector3(2.8f, -0.6f, -0.65f);
        keysDic[KeyID.Shift].transform.position = new Vector3(3.45f, -0.6f, -0.65f);
        keysDic[KeyID.Next].transform.position = new Vector3(4.1f, -0.6f, -0.65f);

    }
    
    public override void SetProperties()
    {
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
            keysDic[key].GetComponent<KeyEvents>().KeyEvents_OnKeyFocused += KeyboardEventHandler_OnFocusedHandler;
            keysDic[key].GetComponent<KeyEvents>().KeyEvents_OnKeyUnfocused += KeyboardEventHandler_OnUnfocusedHandler;
            keysDic[key].GetComponent<KeyEvents>().KeyEvents_OnKeyPressed += KeyboardEventHandler_OnPressedHandler;
            keysDic[key].GetComponent<KeyEvents>().KeyEvents_OnKeyReleased += KeyboardEventHandler_OnReleasedHandler;
        }

        // Special Keys font size fix
        keysDic[KeyID.Backspace].transform.Find("Text").GetComponent<TextMeshPro>().fontSize -= 7;
        keysDic[KeyID.Backspace].transform.Find("Text").GetComponent<TextMeshPro>().text = "Backspace";
        keysDic[KeyID.Space].transform.Find("Text").GetComponent<TextMeshPro>().fontSize -= 7;
        keysDic[KeyID.Space].transform.Find("Text").GetComponent<TextMeshPro>().text = "Space";
        keysDic[KeyID.Shift].transform.Find("Text").GetComponent<TextMeshPro>().fontSize -= 6;
        keysDic[KeyID.Shift].transform.Find("Text").GetComponent<TextMeshPro>().text = "Shift";
        keysDic[KeyID.Next].transform.Find("Text").GetComponent<TextMeshPro>().fontSize -= 6;
        keysDic[KeyID.Next].transform.Find("Text").GetComponent<TextMeshPro>().text = "Next";
        keysDic[KeyID.Enter].transform.Find("Text").GetComponent<TextMeshPro>().fontSize -= 6;
        keysDic[KeyID.Enter].transform.Find("Text").GetComponent<TextMeshPro>().text = "Enter";
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
    }
}
