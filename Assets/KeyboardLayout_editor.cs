using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KeyboardLayout),false)]
public class KeyboardLayout_editor : Editor {
    public override void OnInspectorGUI()
    {
        KeyboardLayout layout = (KeyboardLayout)target;
        GUILayout.BeginVertical("box");
        GUILayout.Label("Keyboard controlls");
        layout.InputType = (KeyboardInputType)EditorGUILayout.EnumPopup("Input Type",layout.InputType);
        layout.keyboardDistanceFromCamera = EditorGUILayout.FloatField("Distance from Camera", layout.keyboardDistanceFromCamera);

        GUILayout.EndVertical();
        GUILayout.BeginVertical("box");
        GUILayout.Label("Between-key distance");
        layout.keyXDelta = EditorGUILayout.FloatField("X Delta Distance", layout.keyXDelta);
        layout.keyYDelta = EditorGUILayout.FloatField("Y Delta Distance", layout.keyYDelta);
        layout.updateLayout = EditorGUILayout.Toggle("Live update", layout.updateLayout);
        GUILayout.Label("Make sure to update after changes");
        GUILayout.Button("Update Layout",GUILayout.Width(100));
        GUILayout.EndVertical();
        GUILayout.BeginVertical("box");
        GUILayout.Label("Vive Controls");
        layout.rightTrackpadHandler = (ViveTrackpad)EditorGUILayout.ObjectField("Right Trackpad", layout.rightTrackpadHandler,typeof(ViveTrackpad),true);
        layout.leftTrackpadHandler = (ViveTrackpad)EditorGUILayout.ObjectField("Left Trackpad", layout.leftTrackpadHandler, typeof(ViveTrackpad), true);
        layout.rDrumLength = EditorGUILayout.FloatField("Right Drum Length", layout.rDrumLength);
        layout.lDrumLength = EditorGUILayout.FloatField("Left Drum Length", layout.lDrumLength);
        GUILayout.EndVertical();

        base.OnInspectorGUI();
    }
}
