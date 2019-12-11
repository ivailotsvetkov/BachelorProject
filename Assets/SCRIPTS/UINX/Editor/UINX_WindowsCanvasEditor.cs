using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(UINX_WindowsCanvas))]
public class UINX_WindowsCanvasEditor : AncestorBehaviourEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UINX_WindowsCanvas myScript = (UINX_WindowsCanvas)target;
        if (GUILayout.Button("Grab Windows"))
        {
            myScript.ws.Clear();
            for (int i = 0; i < myScript.transform.childCount; ++i)
            {
                myScript.ws.Add(myScript.transform.GetChild(i).GetComponent<UINX_Window>());
            }
        }
        if (GUILayout.Button("Grab Canvas"))
        {
            foreach(var w in myScript.ws)
            {
                w.GrabProjection();
            }
        }
    }
}
