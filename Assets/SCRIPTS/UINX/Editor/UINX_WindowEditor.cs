using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UINX_Window), true)]
public class UINX_WindowEditor : AncestorBehaviourEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UINX_Window window = (UINX_Window)target;

        GUI.backgroundColor = Color.cyan;
        var style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = Color.white;
        if (GUILayout.Button("Grab Sockets", style))
        {
            window.GrabSockets();
        }
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.blue;
        if (GUILayout.Button("Inject Buttons", style))
        {
            window.InjectButtons();
        }
        GUI.backgroundColor = Color.red;

        if (GUILayout.Button("Clear Buttons", style))
        {
            window.ClearButtons();
        }
        GUI.backgroundColor = Color.white;

        GUILayout.EndHorizontal();
    }
}
