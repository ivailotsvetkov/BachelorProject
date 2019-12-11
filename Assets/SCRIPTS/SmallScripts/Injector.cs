using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injector : AncestorBehaviour
{
    [SerializeField] protected GameObject _prefab = null;
    public virtual GameObject Prefab { get { return _prefab; } set { _prefab = value; } }
    protected List<GameObject> SpawnedObjects = new List<GameObject>();
    public bool SpawnOnStart = false;

    protected override void Start()
    {
        base.Start();
        if (SpawnOnStart)
        {
            Spawn();
        }
    }

    public virtual T Spawn<T>(AncestorBehaviourInitData initData = null, bool resetTransform = true, bool addToList = true) where T : UnityEngine.MonoBehaviour
    {
        var spawnedObject = Spawn(initData, resetTransform, addToList);
        var component = spawnedObject.GetComponent<T>();

        InitMonoBehaviour(component as AncestorBehaviour, initData);

        return component;
    }

    public virtual GameObject Spawn(AncestorBehaviourInitData initData = null, bool resetTransform = true, bool addToList = true)
    {
        var gameObject = InstantiatePrefab(transform, initData);

        OnObjectSpawn(gameObject, resetTransform, addToList);

        return gameObject;
    }

    protected virtual void InitMonoBehaviour(AncestorBehaviour monoBehaviour, AncestorBehaviourInitData initData)
    {
        if (monoBehaviour != null)
        {
            monoBehaviour.Init(initData);
        }
    }

    protected virtual void OnObjectSpawn(GameObject gameObject, bool resetTransform = true, bool addToList = true)
    {
        if (resetTransform)
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            gameObject.transform.localScale = Vector3.one;
        }

        if (addToList)
        {
            SpawnedObjects.Add(gameObject);
        }
    }

    protected virtual GameObject InstantiatePrefab(Transform parent, AncestorBehaviourInitData initData)
    {
        return Instantiate(Prefab, parent);
    }

    public void SwitchSpawnedObjects(List<GameObject> SpawnedObjects)
    {
        this.SpawnedObjects = SpawnedObjects;
    }

    public virtual void Clear()
    {
        foreach (var spawnedObject in SpawnedObjects)
        {
            if (spawnedObject != null)
            {
                if (spawnedObject != null)
                {
#if UNITY_EDITOR
                    if (!Application.isPlaying)
                    {
                        DestroyImmediate(spawnedObject);
                    } else
                    {
                        Destroy(spawnedObject);
                    }
#else
                        Destroy(spawnedObject);
#endif
                }
            }
        }

        SpawnedObjects.Clear();
    }
}
