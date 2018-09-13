using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Collections;
using UnityEngine;

public class SaveDataModule : MonoBehaviour {
    private string directoryPath;
    public int userId = 1001; // test
    private StreamWriter configFile;
    private StreamWriter timeLineFile;
    private bool SaveUserData = false;
    private ConcurrentQueue<string> _timeLineQueue;

    public static SaveDataModule Instance;
    private bool initialized;
    private bool _stopSerialization = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new Exception("Multiple save modules detected.");
        directoryPath = Application.dataPath + @"\\User_Data\\";
        _timeLineQueue = new ConcurrentQueue<string>();
        StartCoroutine(Serializer());
    }

    // Use this for initialization
    private void Start()
    {
        if (true)
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
        if (true)
        {
            String currentTime = System.DateTime.Now.ToString("MM/dd HH:mm:ss");
            String finalMessage = currentTime + " :: " + v + "\n";
            _timeLineQueue.Enqueue(finalMessage);
        }
    }
    
    private void OnApplicationQuit()
    {
        CloseStreams();
    }

    private void CloseStreams()
    {
        if (true)
        {
            if (timeLineFile != null)
            {
                _stopSerialization = true;
                timeLineFile.WriteLine("===========================================GAMESessionEndded=============================================");
                timeLineFile.WriteLine("=========================================================================================================");

                
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

    public IEnumerator Serializer()
    {
        if (timeLineFile == null)
            Initialize();
        while (!_stopSerialization)
        {
            string finalMessagev = "";
            var success = _timeLineQueue.TryDequeue(out finalMessagev);
            if (success)
            {
                timeLineFile.WriteLine(finalMessagev);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
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
