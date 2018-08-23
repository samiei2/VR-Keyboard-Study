using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour {

    public string fileName = "temp";
    public KeyboardLayout targetKeyboard;
	// Use this for initialization
	void Start () {
        //targetKeyboard.KeyboardLayout_OnKeyPressed += TargetKeyboard_KeyboardLayout_OnKeyPressed;
    }

    private void TargetKeyboard_KeyboardLayout_OnKeyPressed(object sender, KeyEventArgs args)
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
