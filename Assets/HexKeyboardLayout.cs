using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HexKeyboardLayout : MonoBehaviour {
    private Dictionary<String, GameObject> keysDic = new Dictionary<string, GameObject>();

    private void Start()
    {
        CreateMainKeys();
        LayoutKeys();
        SetProperties();
    }

    private void SetProperties()
    {
        foreach (var key in keysDic.Keys)
        {
            keysDic[key].name = "Key_" + key;
            keysDic[key].transform.parent = this.transform;
            keysDic[key].transform.Find("Text").GetComponent<TextMeshPro>().text = key.ToUpper();
            keysDic[key].AddComponent<KeyEvents>();
            keysDic[key].AddComponent<KeyProperties>();
            keysDic[key].GetComponent<KeyProperties>().keyText = key;
        }

        // Special Keys font size fix
        keysDic["spc"].transform.Find("Text").GetComponent<TextMeshPro>().fontSize -= 4;
        keysDic["spc"].transform.GetComponent<KeyProperties>().isPrintable = false;
        keysDic["spc"].GetComponent<KeyProperties>().keyText = "";
        keysDic["shft"].transform.Find("Text").GetComponent<TextMeshPro>().fontSize -= 5;
        keysDic["shft"].transform.GetComponent<KeyProperties>().isPrintable = false;
        keysDic["shft"].GetComponent<KeyProperties>().keyText = "";
        keysDic["123"].transform.Find("Text").GetComponent<TextMeshPro>().fontSize -= 4;
        keysDic["123"].transform.GetComponent<KeyProperties>().isPrintable = false;
        keysDic["123"].GetComponent<KeyProperties>().keyText = "";
        keysDic["ret"].transform.Find("Text").GetComponent<TextMeshPro>().fontSize -= 4;
        keysDic["ret"].transform.GetComponent<KeyProperties>().isPrintable = false;
        keysDic["ret"].GetComponent<KeyProperties>().keyText = "";

    }

    public void LayoutKeys()
    {
        // First row: . k w m u q '
        keysDic["."].transform.position = new Vector3(-6, 4, 0);
        keysDic["k"].transform.position = new Vector3(-4, 4, 0);
        keysDic["w"].transform.position = new Vector3(-2, 4, 0);
        keysDic["m"].transform.position = new Vector3(0, 4, 0);
        keysDic["u"].transform.position = new Vector3(2, 4, 0);
        keysDic["q"].transform.position = new Vector3(4, 4, 0);
        keysDic["'"].transform.position = new Vector3(6, 4, 0);

        // Second row: c h t o f z
        keysDic["c"].transform.position = new Vector3(-5, 2, 0);
        keysDic["h"].transform.position = new Vector3(-3, 2, 0);
        keysDic["t"].transform.position = new Vector3(-1, 2, 0);
        keysDic["o"].transform.position = new Vector3(1, 2, 0);
        keysDic["f"].transform.position = new Vector3(3, 2, 0);
        keysDic["z"].transform.position = new Vector3(5, 2, 0);

        // Third row: j i e space n g b
        keysDic["j"].transform.position = new Vector3(-6, 0, 0);
        keysDic["i"].transform.position = new Vector3(-4, 0, 0);
        keysDic["e"].transform.position = new Vector3(-2, 0, 0);
        keysDic["spc"].transform.position = new Vector3(0, 0, 0);
        keysDic["n"].transform.position = new Vector3(2, 0, 0);
        keysDic["g"].transform.position = new Vector3(4, 0, 0);
        keysDic["b"].transform.position = new Vector3(6, 0, 0);

        // Fourth row: v r s a d ret
        keysDic["v"].transform.position = new Vector3(-5, -2, 0);
        keysDic["r"].transform.position = new Vector3(-3, -2, 0);
        keysDic["s"].transform.position = new Vector3(-1, -2, 0);
        keysDic["a"].transform.position = new Vector3(1, -2, 0);
        keysDic["d"].transform.position = new Vector3(3, -2, 0);
        keysDic["ret"].transform.position = new Vector3(5, -2, 0);

        // Fifth row: , x p l y shift otherkeysDic
        keysDic[","].transform.position = new Vector3(-6, -4, 0);
        keysDic["x"].transform.position = new Vector3(-4, -4, 0);
        keysDic["p"].transform.position = new Vector3(-2, -4, 0);
        keysDic["l"].transform.position = new Vector3(0, -4, 0);
        keysDic["y"].transform.position = new Vector3(2, -4, 0);
        keysDic["shft"].transform.position = new Vector3(4, -4, 0);
        keysDic["123"].transform.position = new Vector3(6, -4, 0);
    }

    private void CreateMainKeys()
    {
        GameObject hexPrefab = Resources.Load("hexPrefab", typeof(GameObject)) as GameObject;

        keysDic.Add("q", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("w", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("e", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("r", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("t", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("y", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("u", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("i", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("o", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("p", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("a", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("s", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("d", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("f", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("g", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("h", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("j", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("k", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("l", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("z", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("x", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("c", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("v", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("b", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("n", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("m", Instantiate(hexPrefab) as GameObject);

        keysDic.Add(".", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("'", Instantiate(hexPrefab) as GameObject);
        keysDic.Add(",", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("spc", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("shft", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("ret", Instantiate(hexPrefab) as GameObject);
        keysDic.Add("123", Instantiate(hexPrefab) as GameObject);
    }
}
