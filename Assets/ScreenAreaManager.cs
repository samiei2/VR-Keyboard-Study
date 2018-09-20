using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenAreaManager : MonoBehaviour {
    public static ScreenAreaManager Instance;
	// Use this for initialization
	void Start () {
        if (Instance==null)
        {
            Instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void DisplayMessage(string v)
    {
        transform.Find("SourceTextArea").GetComponent<TextMeshPro>().text = v;
    }
}
