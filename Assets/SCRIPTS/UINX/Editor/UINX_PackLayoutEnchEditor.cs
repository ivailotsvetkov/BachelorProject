using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(UINX_PackLayoutEnch))]
public class UINX_PackLayoutEnchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        UINX_PackLayoutEnch myScript = (UINX_PackLayoutEnch)target;

        if (GUILayout.Button("Pack"))
        {
            myScript.Pack();
        }

        if (GUILayout.Button("Rescale to Children"))
        {
            myScript.RescaleToChildren();
        }
    }
}