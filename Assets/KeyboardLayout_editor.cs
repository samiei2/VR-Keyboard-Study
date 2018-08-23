using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KeyboardLayout))]
public class KeyboardLayout_editor : Editor {
    public override void OnInspectorGUI()
    {
        KeyboardLayout layout = (KeyboardLayout)target;
        EditorGUILayout.BeginVertical("box");
        layout.InputType = (KeyboardInputType)EditorGUILayout.EnumPopup("Input type: ", layout.InputType);
        

        EditorGUILayout.EndVertical();
        base.OnInspectorGUI();
    }
}
