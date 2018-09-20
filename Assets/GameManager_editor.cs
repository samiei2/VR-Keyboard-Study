using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManager_editro : Editor {
    private bool trainingFoldout;
    private bool testFoldout = false;
    private Vector2 trainingScrollPosition = new Vector2(0, 0);
    private Vector2 testScrollPosition = new Vector2(0, 0);

    public override void OnInspectorGUI()
    {
        GameManager manager_script = (GameManager)target;
        EditorGUILayout.BeginVertical();
        trainingFoldout = EditorGUILayout.Foldout(trainingFoldout, "Training Phrases",true);
        if (trainingFoldout)
        {
            manager_script.numberOfTrainingPhrases = EditorGUILayout.IntField("Number of Phrases", manager_script.numberOfTrainingPhrases);
            trainingScrollPosition = EditorGUILayout.BeginScrollView(trainingScrollPosition, false, false, GUILayout.MinHeight(10), GUILayout.MaxHeight(200));
            if (manager_script.numberOfTrainingPhrases != 0)
            {
                for (int i = 0; i < manager_script.numberOfTrainingPhrases; i++)
                {
                    GUILayout.TextArea("Text " + i, GUILayout.MinHeight(40), GUILayout.MaxHeight(80), GUILayout.MinWidth(40));
                }
            }
            EditorGUILayout.EndScrollView();
        }

        testFoldout = EditorGUILayout.Foldout(testFoldout, "Test Phrases", true, new GUIStyle(EditorStyles.foldout));
        if (testFoldout) {
            manager_script.numberOfTestPhrases = EditorGUILayout.IntField("Number of Phrases", manager_script.numberOfTestPhrases);
            testScrollPosition = EditorGUILayout.BeginScrollView(testScrollPosition, false, false, GUILayout.MinHeight(10), GUILayout.MaxHeight(200));
            if (manager_script.numberOfTestPhrases != 0)
            {
                for (int i = 0; i < manager_script.numberOfTestPhrases; i++)
                {
                    GUILayout.TextArea("Text " + i, GUILayout.MinHeight(40), GUILayout.MaxHeight(80), GUILayout.MinWidth(40));
                }
            }
            EditorGUILayout.EndScrollView();
        }
        EditorGUILayout.EndVertical();
            
        if (GUILayout.Button("Start Training"))
        {

        }
    }
}
