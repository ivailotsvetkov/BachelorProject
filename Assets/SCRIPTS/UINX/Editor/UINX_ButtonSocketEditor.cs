using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(UINX_ButtonSocket), true)]
public class UINX_ButtonSocketEditor : InjectorEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UINX_ButtonSocket but = (UINX_ButtonSocket)target;
        but.RescaleButtonToSocket();
        var style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = Color.white;
        GUI.backgroundColor = Color.gray;
        if (GUILayout.Button("Set As Empty", style))
        {
            but.SetAsEmpty();
        }
        if (but.Prefab != null)
        {
            var comp = but.Prefab.GetComponent<UINX_Button>();
            if (comp != null)
            {
                but.Type = comp.ButtonType;
            }
        }
    }
}