using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InputHandler))]
public class KeyboardInputHandler_editor : Editor {
    public override void OnInspectorGUI()
    {
        InputHandler kin = (InputHandler)target;
        EditorGUILayout.BeginVertical();
        
        kin.SourceType = (InputType)EditorGUILayout.EnumPopup("Input Type:",kin.SourceType);
        kin.RepeatedKeyPressEnabled = (bool)EditorGUILayout.Toggle("Repeat Key Press:", kin.RepeatedKeyPressEnabled);
        EditorGUILayout.EndVertical();
    }
}
