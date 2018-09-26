using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System;

public class RainbowKeyboardLayout : KeyboardLayout
{
    public float keyScale = 1;
    public int keyboardRadius = 10;

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
        var keys = keysDic.Keys.Where(x => x != KeyID.Space
                                        && x != KeyID.Backspace
                                        && x != KeyID.Next
                                        && x != KeyID.Comma
                                        && x != KeyID.Enter
                                        && x != KeyID.Shift
                                        && x != KeyID.Dot).OrderBy(x => x);
        var xcenter = 0;
        var ycenter = 0;
        var radius = keyboardRadius;
        float theta = -90;
        foreach (var key in keys)
        {
            var x = xcenter + radius * Mathf.Sin(ConvertToRadians(theta));
            var y = ycenter + radius * Mathf.Cos(ConvertToRadians(theta));
            var z = 0;

            theta += (180f / 26f);
            keysDic[key].transform.localScale = new Vector3(keyScale, keyScale, keyScale);
            keysDic[key].transform.position = new Vector3(x, y, z);
        }
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
        keysDic[KeyID.Backspace].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 2;
        keysDic[KeyID.Backspace].transform.Find("Text").GetComponent<TextMeshPro>().text = "Backspace";
        keysDic[KeyID.Space].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 3;
        keysDic[KeyID.Space].transform.Find("Text").GetComponent<TextMeshPro>().text = "Space";
        keysDic[KeyID.Shift].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 3;
        keysDic[KeyID.Shift].transform.Find("Text").GetComponent<TextMeshPro>().text = "Shift";
        keysDic[KeyID.Next].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 3;
        keysDic[KeyID.Next].transform.Find("Text").GetComponent<TextMeshPro>().text = "Next";
        keysDic[KeyID.Enter].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 3;
        keysDic[KeyID.Enter].transform.Find("Text").GetComponent<TextMeshPro>().text = "Enter";
    }

    public float ConvertToRadians(float angle)
    {
        return (Mathf.PI / 180) * angle;
    }

    public override void Update()
    {
        base.Update();
        
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
        transform.position = VRDesk.position;
        transform.position -= new Vector3(-.2f, 0.5f, 0);
        transform.eulerAngles = VRDesk.eulerAngles;
    }

    public override void ScaleToFrontViewPosition()
    {
        transform.eulerAngles = Vector3.zero;
        transform.position = new Vector3(0, 0, MainCamera.position.z + keyboardDistanceFromCamera);
        transform.localScale = Vector3.one;
    }
}
