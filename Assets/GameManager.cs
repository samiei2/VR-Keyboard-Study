using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    System.Diagnostics.Stopwatch _timer;
    private SessionType currentSession = SessionType.None;
    private bool _inSession;

    public string[] trainingPhraseSet;

    public string[] testPhraseSet;

    internal int numberOfTrainingPhrases;
    internal int numberOfTestPhrases;
    private KeyboardLayout activeKeyboard;
    private ScreenAreaManager screenArea;

    private int currentPhraseNumber = -1;
    private string[] currentSet;

    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        _timer = new System.Diagnostics.Stopwatch();

        activeKeyboard = KeyboardLayout.Instance;
        activeKeyboard.KeyboardLayout_OnKeyPressed += ActiveKeyboard_KeyboardLayout_OnKeyPressed;

        screenArea = ScreenAreaManager.Instance;
	}

    private void ActiveKeyboard_KeyboardLayout_OnKeyPressed(object sender, KeyEventArgs args)
    {
        if (args.KeyId == KeyID.Enter)
        {
            if (currentSet != null)
            {
                if (currentPhraseNumber < currentSet.Length)
                {
                    ShowNextPhrase();
                }
                else
                {
                    EndSession(currentSession);
                }
            }
        }
    }

    private void ShowNextPhrase()
    {
        screenArea.DisplayMessage(currentSet[currentPhraseNumber]);
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
        if (currentSession != SessionType.None)
        {
            if (!_timer.IsRunning)
            {
                Debug.LogError("Test: Timer Not Running");
                return;
            }
            SaveDataModule.Instance.WriteToTimeLine("==============================" + Enum.GetName(typeof(SessionType), type) + " Session Ended==============================");
            SaveDataModule.Instance.SetSaveData(false);
            _inSession = false;
            currentSession = SessionType.None;
            currentSet = null;
        }
    }

    private void StartSession(SessionType type)
    {
        if (currentSession == SessionType.None)
        {
            currentSession = type;
            if (type == SessionType.Training)
                currentSet = trainingPhraseSet;
            else if (type == SessionType.Test)
                currentSet = testPhraseSet;
            _inSession = true;
            SaveKeyboardLayoutSettings();
            if (_timer.IsRunning)
            {
                Debug.LogError("Test: Timer Still Running");
                return;
            }
            _timer.Start();
            SaveDataModule.Instance.SetSaveData(true);
            SaveDataModule.Instance.WriteToTimeLine("==============================" + Enum.GetName(typeof(SessionType), type) + " Session Started==============================");
        }
    }

    private void SaveKeyboardLayoutSettings()
    {
        activeKeyboard.WriteLayoutSettingsToFile();
    }

    public bool IsInSession()
    {
        return _inSession;
    }
}

public enum SessionType
{
    Training,
    Test,
    Other,
    None
}
