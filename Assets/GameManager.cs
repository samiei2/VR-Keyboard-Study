using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public SessionType currentSession = SessionType.None;
    public bool _inSession;

    [SerializeField]
    public List<string> trainingPhraseSet;
    [SerializeField]
    public List<string> testPhraseSet;
    [SerializeField]
    public Dictionary<string, KeyboardLayout> availableKeyboards = new Dictionary<string, KeyboardLayout>();

    public int numberOfTrainingPhrases;
    public int numberOfTestPhrases;
    private KeyboardLayout activeKeyboard;
    private ScreenAreaManager screenArea { get { return ScreenAreaManager.Instance; } }

    private int currentPhraseNumber = 0;
    private List<string> currentSet;

    private void Awake()
    {
        Instance = this;
    }

    void Start () {

        activeKeyboard = KeyboardLayout.Instance;
        if(activeKeyboard!=null)
        activeKeyboard.KeyboardLayout_OnKeyPressed += ActiveKeyboard_KeyboardLayout_OnKeyPressed;
        
	}

    private void ActiveKeyboard_KeyboardLayout_OnKeyPressed(object sender, KeyEventArgs args)
    {
        if (args.KeyId == KeyID.Enter)
        {
            ShowNextPhrase();
        }
    }

    private void ShowNextPhrase()
    {
        if (currentSet != null)
        {
            if (currentPhraseNumber < currentSet.Count)
            {
                string text = currentSet[currentPhraseNumber];
                screenArea.DisplayMessage(text);
                SaveDataModule.Instance.WriteToTimeLine(Enum.GetName(typeof(SessionType), currentSession) + " Session Text #" + currentPhraseNumber + " : " + text);
                currentPhraseNumber++;
            }
            else
            {
                EndSession(currentSession);
            }
        }
        else
        {
            Debug.LogError("Current set is null");
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
        else if (((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)))
            && Input.GetKeyDown(KeyCode.T)) //Start Testing
        {
            StartSession(SessionType.Test);
        }
        else if (((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)))
            && Input.GetKeyDown(KeyCode.E)) //Start Testing
        {
            EndSession(SessionType.Training);
        }
    }


    public void EndSession(SessionType type)
    {
        if (currentSession != SessionType.None)
        {
            SaveDataModule.Instance.WriteToTimeLine("==============================" + Enum.GetName(typeof(SessionType), type) + " Session Ended==============================");
            _inSession = false;
            currentSession = SessionType.None;
            currentSet = null;
            currentPhraseNumber = 0;
        }
    }

    public void StartSession(SessionType type)
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
            SaveDataModule.Instance.WriteToTimeLine("==============================" + Enum.GetName(typeof(SessionType), type) + " Session Started==============================");
            ShowNextPhrase();
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
