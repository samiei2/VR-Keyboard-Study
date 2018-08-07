using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KeyboardInputHandler))]
public class KeyboardInputHandler_editor : Editor {
    public override void OnInspectorGUI()
    {
        KeyboardInputHandler kin = (KeyboardInputHandler)target;
        EditorGUILayout.BeginVertical();
        
        kin.SourceType = (InputType)EditorGUILayout.EnumPopup("Input Type:",kin.SourceType);
        kin.RepeatedKeyPressEnabled = (bool)EditorGUILayout.Toggle("Repeat Key Press:", kin.RepeatedKeyPressEnabled);
        EditorGUILayout.EndVertical();
    }
}
