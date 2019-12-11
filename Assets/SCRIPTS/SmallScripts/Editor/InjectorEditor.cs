using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Injector))]
[CanEditMultipleObjects]
public class InjectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Injector myScript = (Injector)target;
        GUI.backgroundColor = Color.blue;
        var style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = Color.white;
        if (GUILayout.Button("Inject", style))
        {
            myScript.Spawn();
        }
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Clear", style))
        {
            myScript.Clear();
        }
    }
}