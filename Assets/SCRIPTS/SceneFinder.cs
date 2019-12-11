using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFinder
{
    public const string war1 = "There is no <";
    public const string war2 = "> on the scene!";

    public static T GetFromRoot<T>()
    {
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            Scene s = SceneManager.GetSceneAt(i);
            T obj = default(T);
            var root = s.GetRootGameObjects();
            foreach (var rootling in root)
            {
                obj = rootling.GetComponent<T>();
                if (obj != null)
                {
                    return obj;
                }
            }
            Debug.LogWarning(war1 + (typeof(T).ToString()) + war2);
        }
        return default(T);
    }

    public static List<T> GetAllFromRoot<T>()
    {
        List<T> list = new List<T>();
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            Scene s = SceneManager.GetSceneAt(i);
            T obj = default(T);
            var root = s.GetRootGameObjects();
            foreach (var rootling in root)
            {
                obj = rootling.GetComponent<T>();
                if (obj != null)
                {
                    list.Add(obj);
                }
            }
            Debug.LogWarning(war1 + (typeof(T).ToString()) + war2);
        }
        return list;
    }
    public static List<T> GetAllFromScene<T>()
    {
        List<T> list = new List<T>();
        List<Transform> transfs = new List<Transform>();
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            Scene s = SceneManager.GetSceneAt(i);
            var root = s.GetRootGameObjects();
            foreach (var rootling in root)
            {
                transfs.AddRange(GetTransforms<T>(rootling.transform));
            }
        }
        for (int i = 0; i < transfs.Count; ++i)
        {
            list.Add(transfs[i].GetComponent<T>());
        }
        return list;
    }

    static List<Transform> findlings;
    static void FindTransforms<T>(Transform t)
    {
        if (t.GetComponent<T>() != null)
        {
            findlings.Add(t);
        }
        if (t.childCount > 0)
        {
            for (int i = 0; i < t.childCount; ++i)
            {
                FindTransforms<T>(t.GetChild(i));
            }
        }
        return;
    }
    static List<Transform> GetTransforms<T>(Transform t)
    {
        findlings = new List<Transform>();
        FindTransforms<T>(t);
        return findlings;
    }
}
