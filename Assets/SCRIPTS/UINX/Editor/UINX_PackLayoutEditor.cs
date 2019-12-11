using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(UINX_PackLayout), true)]
public class UINX_PackLayoutEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        UINX_PackLayout myScript = (UINX_PackLayout)target;

        if (GUILayout.Button("Pack"))
        {
            myScript.Pack();
        }

        if (GUILayout.Button("Rescale to Children"))
        {
            myScript.RescaleToChildren();
        }

        if (GUILayout.Button("Only Rescale"))
        {
            myScript.OnlyRescale();
        }
    }
}