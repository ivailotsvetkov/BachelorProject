using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UINX_ButtonClassic))]
public class UINX_ButtonClassicEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        UINX_ButtonClassic myScript = (UINX_ButtonClassic)target;

        if (GUILayout.Button("Rescale Text"))
        {
            myScript.RescaleText();
        }
        if (GUILayout.Button("Get Text Scale Factor"))
        {
            myScript.GetTextScaleFactor();
        }
    }
}
