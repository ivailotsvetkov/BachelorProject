using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AncestorBehaviour), true)]
[CanEditMultipleObjects]
public class AncestorBehaviourEditor : Editor
{
    string[] propertiesInBaseClass = new string[] { "UPDATING", "FIXED_UPDATING", "LATE_UPDATING" };
    SerializedProperty updating;
    SerializedProperty fixed_updating;
    SerializedProperty late_updating;
    Color specialColor = new Color(0.6198113f, 0.7107599f, 1f);
    public override void OnInspectorGUI()
    {
        AncestorBehaviour script = (AncestorBehaviour)target;

        GUI.backgroundColor = specialColor;

        serializedObject.Update();
        updating = serializedObject.FindProperty("UPDATING");
        fixed_updating = serializedObject.FindProperty("FIXED_UPDATING");
        late_updating = serializedObject.FindProperty("LATE_UPDATING");
        EditorGUILayout.PropertyField(updating);
        EditorGUILayout.PropertyField(fixed_updating);
        EditorGUILayout.PropertyField(late_updating);
        GUI.backgroundColor = Color.white;
        DrawPropertiesExcluding(serializedObject, propertiesInBaseClass);
        //style.normal.textColor = Color.white;
        //if (GUILayout.Button("Grab Sockets", style))
        //{
        //}
        serializedObject.ApplyModifiedProperties();
    }
}