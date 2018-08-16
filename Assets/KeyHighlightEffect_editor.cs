using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KeyHighlightEffect))]
public class KeyHighlightEffect_editor : Editor {
    public override void OnInspectorGUI()
    {
        KeyHighlightEffect highEffect = (KeyHighlightEffect)target;
        EditorGUILayout.BeginVertical();
        highEffect.SelectedEffect = (Highlights)EditorGUILayout.EnumPopup("Select highlight", highEffect.SelectedEffect);
        EditorGUILayout.EndVertical();
    }
}
