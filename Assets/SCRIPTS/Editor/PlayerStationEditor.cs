using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerStation))]
public class PlayerStationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlayerStation myScript = (PlayerStation)target;

        if (GUILayout.Button("Setup Hand"))
        {
            var l = System.IO.File.ReadAllLines("ROneFinger.txt");
            var bones = new List<Transform>(GetTransforms(myScript.HandBoneParent));
            List<Vector3> Pos = new List<Vector3>();
            List<Quaternion> Rots = new List<Quaternion>();
            for (int j = 0; j < l.Length; ++j)
            {
                var sp = l[j].Replace(',','.').Split(';');
                if (j % 2 == 0)
                {
                    Pos.Add(new Vector3(float.Parse(sp[0]), float.Parse(sp[1]), float.Parse(sp[2])));
                } else
                {
                    Rots.Add(new Quaternion(float.Parse(sp[0]), float.Parse(sp[1]), float.Parse(sp[2]), float.Parse(sp[3])));
                }
            }
            for (int i = 0; i < bones.Count; ++i)
            {
                bones[i].localPosition = Pos[i];
                bones[i].localRotation = Rots[i];
            }
        }
    }

    SkinnedMeshRenderer findling;

    void FindTransforms(Transform t)
    {
        if (!t.name.Contains("ignore"))
        {
            findlings.Add(t);
            if (t.childCount > 0)
            {
                for (int i = 0; i < t.childCount; ++i)
                {
                    FindTransforms(t.GetChild(i));
                }
            }
        }
        return;
    }
    List<Transform> findlings = new List<Transform>();
    public List<Transform> GetTransforms(Transform t)
    {
        findlings.Clear();
        FindTransforms(t);
        findlings.RemoveAt(0);
        return findlings;
    }
}
