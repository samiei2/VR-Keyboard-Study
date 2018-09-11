using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataModule : MonoBehaviour {
    private string directoryPath;
    public int userId = 1001; // test
    private StreamWriter configFile;
    private StreamWriter timeLineFile;
    private bool SaveUserData = false;

    public static SaveDataModule Instance;
    private bool initialized;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new Exception("Multiple save modules detected.");
        directoryPath = Application.dataPath + @"\\User_Data\\";
    }

    // Use this for initialization
    private void Start()
    {
        if (SaveUserData)
        {
            if(!initialized)
                Initialize();
        }

    }

    private void Initialize()
    {
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
        if (!File.Exists(GetPath(UserDataTypes.Timeline)))
            timeLineFile = File.CreateText(GetPath(UserDataTypes.Timeline));
        else
            timeLineFile = new StreamWriter(GetPath(UserDataTypes.Timeline),true);

        if (!File.Exists(GetPath(UserDataTypes.KeyboardConfig)))
            configFile = File.CreateText(GetPath(UserDataTypes.KeyboardConfig));
        else
            configFile = new StreamWriter(GetPath(UserDataTypes.KeyboardConfig), true);
        initialized = true;
    }

    private void Update()
    {
        if (!initialized)
        {
            Initialize();
        }
    }

    private string GetPath(UserDataTypes type)
    {
        return directoryPath +"\\"+ userId + "_"+ Enum.GetName(typeof(UserDataTypes),type) +"_" + System.DateTime.Now.ToString("MM.dd.yyyy") + ".txt";
    }

    internal void WriteToTimeLine(string v)
    {
        if (timeLineFile == null)
            Initialize();
        if (SaveUserData)
        {
            String currentTime = System.DateTime.Now.ToString("MM/dd HH:mm:ss");
            String finalMessage = currentTime + " :: " + v + "\n";
            
            timeLineFile.WriteLine(finalMessage);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        CloseStreams();
    }
    
    private void OnApplicationQuit()
    {
        CloseStreams();
    }

    private void CloseStreams()
    {
        if (SaveUserData)
        {
            if (timeLineFile != null)
            {

                WriteToTimeLine("===========================================GAMESessionEndded=============================================");
                WriteToTimeLine("=========================================================================================================");
                timeLineFile.Flush();
                timeLineFile.Close();
            }
            if (configFile != null)
            {
                configFile.Flush();
                configFile.Close();
            }
        }
    }

    public void SetSaveData(bool v)
    {
        SaveUserData = v;
    }
}

public enum UserDataTypes
{
    Timeline,
    KeyboardConfig
}
