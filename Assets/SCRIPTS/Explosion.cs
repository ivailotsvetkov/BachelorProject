using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : AncestorBehaviour
{
    public bool isFree = true;
    public float LifeTime = 4f;
    float lifeTS = 0;
    public AudioSource Sound;

    public void Spawn(Vector3 position, AudioClip clip = null)
    {
        lifeTS = 0;
        transform.position = position;
        if (clip != null)
        {
            Sound.clip = clip;
            Sound.Play();
        }
        gameObject.SetActive(true);
        isFree = false;
    }

    public override void Ancestor_FixedUpdate()
    {
        base.Ancestor_FixedUpdate();
        if (!isFree)
        {
            lifeTS += Time.deltaTime;
            if (lifeTS >= LifeTime)
            {
                isFree = true;
                gameObject.SetActive(false);
            }
        }

    }
}
