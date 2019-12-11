using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AncestorScriptable: UnityEngine.ScriptableObject
{
    public void Save()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
#endif
    }

    public T Instantiate<T>() where T : AncestorScriptable
    {
        return UnityEngine.Object.Instantiate(this) as T;
    }
}
