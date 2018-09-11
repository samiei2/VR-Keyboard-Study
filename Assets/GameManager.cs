using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    System.Diagnostics.Stopwatch _timer;
	// Use this for initialization
	void Start () {
        _timer = new System.Diagnostics.Stopwatch();
        if (GameObject.Find("MenuModule")!=null)
        {
            var menuModule = GameObject.Find("MenuModule");
            var commandText = menuModule.transform.Find("MenuFollower").Find("Menu").Find("CommandText");
            commandText.GetComponent<TextMeshPro>().text = "";
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if ((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && Input.GetKeyDown(KeyCode.T)) //Start Training
        {
            StartSession(SessionType.Training);
        }
        else if ((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && Input.GetKeyDown(KeyCode.E)) //End Training
        {
            EndSession(SessionType.Training);
        }
        else if (((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.RightControl)))
            && Input.GetKeyDown(KeyCode.T)) //Start Testing
        {
            StartSession(SessionType.Test);
        }
        else if (((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.RightControl)))
            && Input.GetKeyDown(KeyCode.E)) //Start Testing
        {
            EndSession(SessionType.Training);
        }
    }


    private void EndSession(SessionType type)
    {
        if (!_timer.IsRunning)
        {
            Debug.LogError("Test: Timer Not Running");
            return;
        }
        SaveDataModule.Instance.WriteToTimeLine("=============================="+ Enum.GetName(typeof(SessionType), type) + " Session Ended==============================");
        SaveDataModule.Instance.SetSaveData(false);
    }

    private void StartSession(SessionType type)
    {
        if (_timer.IsRunning)
        {
            Debug.LogError("Test: Timer Still Running");
            return;
        }
        _timer.Start();
        SaveDataModule.Instance.SetSaveData(true);
        SaveDataModule.Instance.WriteToTimeLine("=============================="+Enum.GetName(typeof(SessionType),type)+" Session Started==============================");
    }

}

public enum SessionType
{
    Training,
    Test,
    Other
}
