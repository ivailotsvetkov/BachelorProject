using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : AncestorBehaviour
{
    float ts = 0;
    public float Show_Time = 1f;

    Vector3 startScale = Vector3.zero;

    protected override void Start()
    {
        base.Start();
        transform.localScale = Vector3.zero;
        var rand = Random.Range(0, 4);
        if (rand == 0)
        {
            startScale = Vector3.right;
        } else if (rand == 1)
        {
            startScale = Vector3.up;
        } else if (rand == 2)
        {
            startScale = Vector3.forward;
        } else
        {
            startScale = Vector3.zero;
        }
    }
    public override void Ancestor_Update()
    {
        base.Ancestor_Update();
        if (ts < Show_Time)
        {
            ts += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, Vector3.one, ts / Show_Time);
        } else if (ts > Show_Time)
        {
            ts = Show_Time;
        }
    }
}
