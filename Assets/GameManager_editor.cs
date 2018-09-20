using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManager_editro : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager manager_script = (GameManager)target;
        
    }
}
