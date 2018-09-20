
﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManager_editro : Editor {
    private bool trainingFoldout;
    private bool testFoldout = false;
    private Vector2 trainingScrollPosition = new Vector2(0, 0);
    private Vector2 testScrollPosition = new Vector2(0, 0);
    private bool _inSession;
    private SessionType _sessionType = SessionType.None;
    private SerializedProperty traingPhrases;
    private SerializedProperty testPhrases;
    private SerializedProperty numberOfTrainingPhrases;
    private SerializedProperty numberOfTestPhrases;

    private void OnEnable()
    {
        numberOfTrainingPhrases = serializedObject.FindProperty("numberOfTrainingPhrases");
        numberOfTestPhrases = serializedObject.FindProperty("numberOfTestPhrases");

        traingPhrases = serializedObject.FindProperty("trainingPhraseSet");
        testPhrases = serializedObject.FindProperty("testPhraseSet");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GameManager manager_script = (GameManager)target;
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
                if (numberOfTrainingPhrases.intValue >= (traingPhrases.objectReferenceValue as System.Object as List<string>).Count)
                {
                    for (int i = 0; i < numberOfTrainingPhrases.intValue - (traingPhrases.objectReferenceValue as System.Object as List<string>).Count; i++)
                    {
                        (traingPhrases.objectReferenceValue as System.Object as List<string>).Add("");
                    }
                }
                else
                {
                    (traingPhrases.objectReferenceValue as System.Object as List<string>).RemoveRange(numberOfTrainingPhrases.intValue, (traingPhrases.objectReferenceValue as System.Object as List<string>).Count - numberOfTrainingPhrases.intValue);
                }
                
                for (int i = 0; i < numberOfTrainingPhrases.intValue; i++)
                {
                    if (i < (traingPhrases.objectReferenceValue as System.Object as List<string>).Count)
                        (traingPhrases.objectReferenceValue as System.Object as List<string>)[i] = GUILayout.TextArea((traingPhrases.objectReferenceValue as System.Object as List<string>)[i], GUILayout.MinHeight(40), GUILayout.MaxHeight(80), GUILayout.MinWidth(40));
                    else
                    {
                        (traingPhrases.objectReferenceValue as System.Object as List<string>).Add("");
                        (traingPhrases.objectReferenceValue as System.Object as List<string>)[i] = GUILayout.TextArea((traingPhrases.objectReferenceValue as System.Object as List<string>)[i], GUILayout.MinHeight(40), GUILayout.MaxHeight(80), GUILayout.MinWidth(40));
                    }
                }

                if (Application.isPlaying)
                    manager_script.trainingPhraseSet = (traingPhrases.objectReferenceValue as System.Object as List<string>);
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
                if (numberOfTestPhrases.intValue >= (testPhrases.objectReferenceValue as System.Object as List<string>).Count)
                {
                    for (int i = 0; i < numberOfTestPhrases.intValue - (testPhrases.objectReferenceValue as System.Object as List<string>).Count; i++)
                    {
                        (testPhrases.objectReferenceValue as System.Object as List<string>).Add("");
                    }
                }
                else
                {
                    (testPhrases.objectReferenceValue as System.Object as List<string>).RemoveRange(numberOfTestPhrases.intValue, (testPhrases.objectReferenceValue as System.Object as List<string>).Count - numberOfTestPhrases.intValue);
                }

                for (int i = 0; i < numberOfTestPhrases.intValue; i++)
                {
                    if (i < (testPhrases.objectReferenceValue as System.Object as List<string>).Count)
                        (testPhrases.objectReferenceValue as System.Object as List<string>)[i] = GUILayout.TextArea((testPhrases.objectReferenceValue as System.Object as List<string>)[i], GUILayout.MinHeight(40), GUILayout.MaxHeight(80), GUILayout.MinWidth(40));
                    else
                    {
                        (testPhrases.objectReferenceValue as System.Object as List<string>).Add("");
                        (testPhrases.objectReferenceValue as System.Object as List<string>)[i] = GUILayout.TextArea((testPhrases.objectReferenceValue as System.Object as List<string>)[i], GUILayout.MinHeight(40), GUILayout.MaxHeight(80), GUILayout.MinWidth(40));
                    }
                }

                if (Application.isPlaying)
                    manager_script.testPhraseSet = (testPhrases.objectReferenceValue as System.Object as List<string>);
            }
            EditorGUILayout.EndScrollView();
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        if (_inSession && Application.isPlaying)
        {
            if (_sessionType == SessionType.Training)
            {
                if (GUILayout.Button("Stop Training"))
                {
                    manager_script.EndSession(_sessionType);
                    _inSession = false;
                    _sessionType = SessionType.None;
                }
            }
            if (_sessionType == SessionType.Test)
            {
                if (GUILayout.Button("Stop Test"))
                {
                    manager_script.EndSession(_sessionType);
                    _inSession = false;
                    _sessionType = SessionType.None;
                }
            }
        }
        else{
            if(GUILayout.Button("Start Training") && Application.isPlaying)
            {
                _inSession = true;
                _sessionType = SessionType.Training;
                manager_script.StartSession(_sessionType);
            }
            if (GUILayout.Button("Start Testing") && Application.isPlaying)
            {
                _inSession = true;
                _sessionType = SessionType.Test;
                manager_script.StartSession(_sessionType);
            }
        }
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}
