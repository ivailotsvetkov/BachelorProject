using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

public class AncestorBehaviourInitData
{
}

public class AncestorBehaviour : MonoBehaviour
{
    [Header("== SETUP ==")]
    public bool UPDATING = true;
    public bool FIXED_UPDATING = false;
    public bool LATE_UPDATING = false;
    [HideInInspector]
    public bool IsInitialized = false;
    public virtual void Init(AncestorBehaviourInitData initData = null)
    {
        if (IsInitialized)
        {
            return;
        }
        IsInitialized = true;
    }

    protected virtual void Start()
    {

    }

    public virtual void Ancestor_Update()
    {

    }

    public virtual void Ancestor_FixedUpdate()
    {

    }

    public virtual void Ancestor_LateUpdate()
    {

    }

    protected virtual void OnEnable()
    {
        if (UPDATING && Overmind.AncestorsToUpdate.IndexOf(this) == -1)
        {
            Overmind.AncestorsToUpdate.Add(this);
        }
        if (FIXED_UPDATING && Overmind.AncestorsToFixedUpdate.IndexOf(this) == -1)
        {
            Overmind.AncestorsToFixedUpdate.Add(this);
        }
        if (LATE_UPDATING && Overmind.AncestorsToLateUpdate.IndexOf(this) == -1)
        {
            Overmind.AncestorsToLateUpdate.Add(this);
        }
    }

    protected virtual void OnDisable()
    {
        RemoveUpdates();
    }

    public virtual void BeforeDestroy()
    {
        if (Overmind.AncestorsBeforeDestroy.IndexOf(this) == -1)
        {
            Overmind.AncestorsBeforeDestroy.Add(this);
        }
    }

    protected virtual void OnDestroy()
    {
        RemoveUpdates();
    }

    void RemoveUpdates()
    {
        if (UPDATING && Overmind.AncestorsToUpdate.IndexOf(this) != -1)
        {
            Overmind.AncestorsToUpdate.Remove(this);
        }
        if (FIXED_UPDATING && Overmind.AncestorsToFixedUpdate.IndexOf(this) != -1)
        {
            Overmind.AncestorsToFixedUpdate.Remove(this);
        }
        if (LATE_UPDATING && Overmind.AncestorsToLateUpdate.IndexOf(this) != -1)
        {
            Overmind.AncestorsToLateUpdate.Remove(this);
        }
    }

#if UNITY_EDITOR
    public virtual void SetDirty()
    {
        EditorUtility.SetDirty(this);
        EditorSceneManager.MarkSceneDirty(gameObject.scene);
    }
#endif
}