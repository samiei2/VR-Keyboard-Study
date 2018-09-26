
﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManager_editro : Editor {
    [SerializeField]
    private bool trainingFoldout;
    [SerializeField]
    private bool testFoldout;
    [SerializeField]
    private Vector2 trainingScrollPosition = new Vector2(0, 0);
    [SerializeField]
    private Vector2 testScrollPosition = new Vector2(0, 0);

    [SerializeField]
    public List<string> traingPhrases;
    [SerializeField]
    public List<string> testPhrases;

    //private SerializedProperty traingPhrases;
    //private SerializedProperty testPhrases;
    private GameManager manager_script;
    private SerializedProperty numberOfTrainingPhrases;
    private SerializedProperty numberOfTestPhrases;

    private void OnEnable()
    {
        manager_script = (GameManager)target;
        numberOfTrainingPhrases = serializedObject.FindProperty("numberOfTrainingPhrases");
        numberOfTestPhrases = serializedObject.FindProperty("numberOfTestPhrases");

        traingPhrases = manager_script.trainingPhraseSet;
        testPhrases = manager_script.testPhraseSet;
        //traingPhrases = serializedObject.FindProperty("trainingPhraseSet");
        //testPhrases = serializedObject.FindProperty("testPhraseSet");
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        serializedObject.Update();
        
        EditorGUILayout.BeginVertical();
        trainingFoldout = EditorGUILayout.Foldout(trainingFoldout, "Training Phrases",true);
        if (trainingFoldout)
        {
            numberOfTrainingPhrases.intValue = EditorGUILayout.IntField("Number of Phrases", numberOfTrainingPhrases.intValue);
            if (numberOfTrainingPhrases.intValue < 0)
                numberOfTrainingPhrases.intValue = 0;
            trainingScrollPosition = EditorGUILayout.BeginScrollView(trainingScrollPosition, false, false, GUILayout.MinHeight(10), GUILayout.MaxHeight(200));
            if (numberOfTrainingPhrases.intValue >= 0)
            {
                if (numberOfTrainingPhrases.intValue >= traingPhrases.Count)
                {
                    for (int i = 0; i < numberOfTrainingPhrases.intValue - traingPhrases.Count; i++)
                    {
                        traingPhrases.Add("");
                    }
                }
                else
                {
                    traingPhrases.RemoveRange(numberOfTrainingPhrases.intValue, traingPhrases.Count - numberOfTrainingPhrases.intValue);
                }
                
                for (int i = 0; i < numberOfTrainingPhrases.intValue; i++)
                {
                    if (i < traingPhrases.Count)
                        traingPhrases[i] = GUILayout.TextArea(traingPhrases[i], GUILayout.MinHeight(40), GUILayout.MaxHeight(80), GUILayout.MinWidth(40));
                    else
                    {
                        traingPhrases.Add("");
                        traingPhrases[i] = GUILayout.TextArea(traingPhrases[i], GUILayout.MinHeight(40), GUILayout.MaxHeight(80), GUILayout.MinWidth(40));
                    }
                }

                if (Application.isPlaying)
                    manager_script.trainingPhraseSet = traingPhrases;
            }
            EditorGUILayout.EndScrollView();
        }

        testFoldout = EditorGUILayout.Foldout(testFoldout, "Test Phrases", true, new GUIStyle(EditorStyles.foldout));
        if (testFoldout) {
            numberOfTestPhrases.intValue = EditorGUILayout.IntField("Number of Phrases", numberOfTestPhrases.intValue);
            if (numberOfTestPhrases.intValue < 0)
                numberOfTestPhrases.intValue = 0;
            testScrollPosition = EditorGUILayout.BeginScrollView(testScrollPosition, false, false, GUILayout.MinHeight(10), GUILayout.MaxHeight(200));
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
}
