
﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(GameManager))]
public class GameManager_editro : Editor {
    
    [SerializeField]
    public List<string> trainingPhrases;
    [SerializeField]
    public List<string> testPhrases;
    [SerializeField]
    public Dictionary<string,KeyboardLayout> availableKeyboards;


    //private SerializedProperty traingPhrases;
    //private SerializedProperty testPhrases;
    private GameManager manager_script;
    private SerializedProperty numberOfTrainingPhrases;
    private SerializedProperty numberOfTestPhrases;
    private SerializedProperty randomizeSets;
    private SerializedProperty autoIncrementUserId;
    private SerializedProperty trainingFoldout;
    private SerializedProperty testFoldout;
    private SerializedProperty trainingScrollPosition;
    private SerializedProperty testScrollPosition;

    private List<string> phrases = new List<string>();

    private void OnEnable()
    {
        serializedObject.UpdateIfRequiredOrScript();
        manager_script = (GameManager)target;
        numberOfTrainingPhrases = serializedObject.FindProperty("numberOfTrainingPhrases");
        numberOfTestPhrases = serializedObject.FindProperty("numberOfTestPhrases");
        randomizeSets = serializedObject.FindProperty("randomizeSets");
        autoIncrementUserId = serializedObject.FindProperty("autoIncrementUserId");
        trainingFoldout = serializedObject.FindProperty("trainingFoldout");
        testFoldout = serializedObject.FindProperty("testFoldout");
        trainingScrollPosition = serializedObject.FindProperty("trainingScrollPosition");
        testScrollPosition = serializedObject.FindProperty("testScrollPosition");


        phrases.Clear();
        phrases.AddRange(manager_script.Phrases);

        trainingPhrases = manager_script.trainingPhraseSet;
        testPhrases = manager_script.testPhraseSet;

        if (randomizeSets.boolValue)
        {
            trainingPhrases.Clear();
            testPhrases.Clear();
            for (int i = 0; i < numberOfTrainingPhrases.intValue; i++)
            {
                int randPhrase = Random.Range(0, phrases.Count-1);
                trainingPhrases.Add(phrases[randPhrase]);
                phrases.Remove(phrases[randPhrase]);
            }
            for (int i = 0; i < numberOfTestPhrases.intValue; i++)
            {
                int randPhrase = Random.Range(0, phrases.Count - 1);
                testPhrases.Add(phrases[randPhrase]);
            }
        }
        
        
        availableKeyboards = manager_script.availableKeyboards;
        //traingPhrases = serializedObject.FindProperty("trainingPhraseSet");
        //testPhrases = serializedObject.FindProperty("testPhraseSet");
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        serializedObject.Update();
        
        EditorGUILayout.BeginVertical();
        randomizeSets.boolValue = EditorGUILayout.Toggle("Randomize Sets",randomizeSets.boolValue);
        //autoIncrementUserId.boolValue = EditorGUILayout.Toggle("Autoincrement User Id", autoIncrementUserId.boolValue);
        
        trainingFoldout.boolValue = EditorGUILayout.Foldout(trainingFoldout.boolValue, "Training Phrases",true);
        if (trainingFoldout.boolValue)
        {
            numberOfTrainingPhrases.intValue = EditorGUILayout.IntField("Number of Phrases", numberOfTrainingPhrases.intValue);
            if (numberOfTrainingPhrases.intValue < 0)
                numberOfTrainingPhrases.intValue = 0;
            trainingScrollPosition.vector2Value = EditorGUILayout.BeginScrollView(trainingScrollPosition.vector2Value, false, false, GUILayout.MinHeight(10), GUILayout.MaxHeight(200));
            if (numberOfTrainingPhrases.intValue >= 0)
            {
                if (numberOfTrainingPhrases.intValue >= trainingPhrases.Count)
                {
                    for (int i = 0; i < numberOfTrainingPhrases.intValue - trainingPhrases.Count; i++)
                    {
                        trainingPhrases.Add("");
                    }
                }
                else
                {
                    trainingPhrases.RemoveRange(numberOfTrainingPhrases.intValue, trainingPhrases.Count - numberOfTrainingPhrases.intValue);
                }
                
                for (int i = 0; i < numberOfTrainingPhrases.intValue; i++)
                {
                    if (i < trainingPhrases.Count)
                        trainingPhrases[i] = GUILayout.TextArea(trainingPhrases[i], GUILayout.MinHeight(40), GUILayout.MaxHeight(80), GUILayout.MinWidth(40));
                    else
                    {
                        trainingPhrases.Add("");
                        trainingPhrases[i] = GUILayout.TextArea(trainingPhrases[i], GUILayout.MinHeight(40), GUILayout.MaxHeight(80), GUILayout.MinWidth(40));
                    }
                }

                if (Application.isPlaying)
                    manager_script.trainingPhraseSet = trainingPhrases;
            }
            EditorGUILayout.EndScrollView();
        }

        testFoldout.boolValue = EditorGUILayout.Foldout(testFoldout.boolValue, "Test Phrases", true, new GUIStyle(EditorStyles.foldout));
        if (testFoldout.boolValue) {
            numberOfTestPhrases.intValue = EditorGUILayout.IntField("Number of Phrases", numberOfTestPhrases.intValue);
            if (numberOfTestPhrases.intValue < 0)
                numberOfTestPhrases.intValue = 0;
            testScrollPosition.vector2Value = EditorGUILayout.BeginScrollView(testScrollPosition.vector2Value, false, false, GUILayout.MinHeight(10), GUILayout.MaxHeight(200));
            if (numberOfTestPhrases.intValue >= 0)
            {
                if (numberOfTestPhrases.intValue >= testPhrases.Count)
                {
                    for (int i = 0; i < numberOfTestPhrases.intValue - testPhrases.Count; i++)
                    {
                        testPhrases.Add("");
                    }
                }
                else
                {
                    testPhrases.RemoveRange(numberOfTestPhrases.intValue, testPhrases.Count - numberOfTestPhrases.intValue);
                }

                for (int i = 0; i < numberOfTestPhrases.intValue; i++)
                {
                    if (i < testPhrases.Count)
                        testPhrases[i] = GUILayout.TextArea(testPhrases[i], GUILayout.MinHeight(40), GUILayout.MaxHeight(80), GUILayout.MinWidth(40));
                    else
                    {
                        testPhrases.Add("");
                        testPhrases[i] = GUILayout.TextArea(testPhrases[i], GUILayout.MinHeight(40), GUILayout.MaxHeight(80), GUILayout.MinWidth(40));
                    }
                }

                if (Application.isPlaying)
                    manager_script.testPhraseSet = testPhrases;
            }
            EditorGUILayout.EndScrollView();
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        if (manager_script._inSession && Application.isPlaying)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Prev Phrase"))
            {
                manager_script.PreviousPhrase();
            }
            if (GUILayout.Button("Next Phrase"))
            {
                manager_script.NextPhrase();
            }
            EditorGUILayout.EndHorizontal();
            if (manager_script.currentSession == SessionType.Training)
            {
                if (GUILayout.Button("Stop Training"))
                {
                    manager_script.EndSession(SessionType.Training);
                    //manager_script._inSession = false;
                    //manager_script.currentSession = SessionType.None;
                }
            }
            if (manager_script.currentSession == SessionType.Test)
            {
                if (GUILayout.Button("Stop Test"))
                {
                    manager_script.EndSession(SessionType.Test);
                    //manager_script._inSession = false;
                    //manager_script.currentSession = SessionType.None;
                }
            }
        }
        else{
            if(GUILayout.Button("Start Training") && Application.isPlaying)
            {
                //manager_script._inSession = true;
                //manager_script.currentSession = SessionType.Training;
                manager_script.StartSession(SessionType.Training);
            }
            if (GUILayout.Button("Start Testing") && Application.isPlaying)
            {
                //manager_script._inSession = true;
                //manager_script.currentSession = SessionType.Test;
                manager_script.StartSession(SessionType.Test);
            }
        }
        EditorGUILayout.EndHorizontal();

        //EditorGUILayout.BeginVertical("box");

        //availableKeyboards.Clear();
        
        //foreach (var item in FindObjectsOfTypeAll<KeyboardLayout>())
        //{
        //    EditorGUILayout.BeginVertical();
        //    availableKeyboards.Add(item.name,item);
        //    var enabled = EditorGUILayout.Toggle(item.name, availableKeyboards[item.name].transform.gameObject.activeInHierarchy);
        //    if (enabled)
        //    {
        //        Debug.LogError("Enabled");
        //        foreach (var item2 in availableKeyboards)
        //        {
        //            if (item2.Key != item.name)
        //            {
        //                availableKeyboards[item.name].transform.gameObject.SetActive(false);
        //            }
        //        }
        //    }
        //    availableKeyboards[item.name].transform.gameObject.SetActive(enabled);
        //    EditorGUILayout.EndVertical();
        //}
        //EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }
        if (GUI.changed && !Application.isPlaying)
        {
            EditorUtility.SetDirty(target);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }

    public static List<T> FindObjectsOfTypeAll<T>()
    {
        List<T> results = new List<T>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var s = SceneManager.GetSceneAt(i);
            if (s.isLoaded)
            {
                var allGameObjects = s.GetRootGameObjects();
                for (int j = 0; j < allGameObjects.Length; j++)
                {
                    var go = allGameObjects[j];
                    results.AddRange(go.GetComponentsInChildren<T>(true));
                }
            }
        }
        return results;
    }
}
