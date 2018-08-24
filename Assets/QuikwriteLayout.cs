using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuikwriteLayout : KeyboardLayout {
    public GameObject[] keys;
    private GameObject KeyboardCenter;

    // Use this for initialization

    public override void CreateMainKeys()
    {
        GameObject hexPrefab = Resources.Load("CirclePrefab", typeof(GameObject)) as GameObject;

        keysDic.Add(KeyID.Q, null);
        keysDic.Add(KeyID.W, null);
        keysDic.Add(KeyID.E, null);
        keysDic.Add(KeyID.R, null);
        keysDic.Add(KeyID.T, null);
        keysDic.Add(KeyID.Y, null);
        keysDic.Add(KeyID.U, null);
        keysDic.Add(KeyID.I, null);
        keysDic.Add(KeyID.O, null);
        keysDic.Add(KeyID.P, null);
        keysDic.Add(KeyID.A, null);
        keysDic.Add(KeyID.S, null);
        keysDic.Add(KeyID.D, null);
        keysDic.Add(KeyID.F, null);
        keysDic.Add(KeyID.G, null);
        keysDic.Add(KeyID.H, null);
        keysDic.Add(KeyID.J, null);
        keysDic.Add(KeyID.K, null);
        keysDic.Add(KeyID.L, null);
        keysDic.Add(KeyID.Z, null);
        keysDic.Add(KeyID.X, null);
        keysDic.Add(KeyID.C, null);
        keysDic.Add(KeyID.V, null);
        keysDic.Add(KeyID.B, null);
        keysDic.Add(KeyID.N, null);
        keysDic.Add(KeyID.M, null);
    }

    public override void HighlightKeys(List<char> suggestedAlphabet)
    {

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
    }

    public override void KeyboardEventHandler_OnPressedHandler(object sender, KeyEventArgs args)
    {

    }

    public override void KeyboardEventHandler_OnReleasedHandler(object sender, KeyEventArgs args)
    {

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
    }

    public override void LayoutKeys()
    {
        keysDic[KeyID.A] = keys[0];
        keysDic[KeyID.B] = keys[1];
        keysDic[KeyID.C] = keys[2];
        keysDic[KeyID.D] = keys[3];
        keysDic[KeyID.E] = keys[4];
        keysDic[KeyID.F] = keys[5];
        keysDic[KeyID.G] = keys[6];
        keysDic[KeyID.H] = keys[7];
        keysDic[KeyID.I] = keys[8];
        keysDic[KeyID.J] = keys[9];
        keysDic[KeyID.K] = keys[10];
        keysDic[KeyID.L] = keys[11];
        keysDic[KeyID.M] = keys[12];
        keysDic[KeyID.N] = keys[13];
        keysDic[KeyID.O] = keys[14];
        keysDic[KeyID.P] = keys[15];
        keysDic[KeyID.Q] = keys[16];
        keysDic[KeyID.R] = keys[17];
        keysDic[KeyID.S] = keys[18];
        keysDic[KeyID.T] = keys[19];
        keysDic[KeyID.U] = keys[20];
        keysDic[KeyID.V] = keys[21];
        keysDic[KeyID.W] = keys[22];
        keysDic[KeyID.X] = keys[23];
        keysDic[KeyID.Y] = keys[24];
        keysDic[KeyID.Z] = keys[25];

        keysDic[KeyID.Shift] = keys[26];
        keysDic[KeyID.Enter] = keys[27];
        keysDic[KeyID.Comma] = keys[28];
        keysDic[KeyID.Dot] = keys[29];
        keysDic[KeyID.Next] = keys[30];
        keysDic[KeyID.Backspace] = keys[31];
    }

    public override void ResetKeyBoard()
    {
        throw new System.NotImplementedException();
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
        keysDic[KeyID.Shift].transform.Find("Text").GetComponent<TextMeshPro>().fontSize -= 6;
        keysDic[KeyID.Shift].transform.Find("Text").GetComponent<TextMeshPro>().text = "Shift";
        keysDic[KeyID.Next].transform.Find("Text").GetComponent<TextMeshPro>().fontSize -= 6;
        keysDic[KeyID.Next].transform.Find("Text").GetComponent<TextMeshPro>().text = "Next";
        keysDic[KeyID.Enter].transform.Find("Text").GetComponent<TextMeshPro>().fontSize -= 6;
        keysDic[KeyID.Enter].transform.Find("Text").GetComponent<TextMeshPro>().text = "Enter";
        keysDic[KeyID.Backspace].transform.Find("Text").GetComponent<TextMeshPro>().fontSize -= 6;
        keysDic[KeyID.Backspace].transform.Find("Text").GetComponent<TextMeshPro>().text = "Backspace";
    }
}

