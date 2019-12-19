using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AncestorBehaviour
{
    public bool isFree = true;
    public AudioSource Sound;
    float Speed = 1;
    float lifeTS = 0;
    float Life_Time = 1;
    Vector3 velocity;
    public LayerMask TargetMask;
    public float Ray_Length = 1f;
    bool canDestroy = true;
    public Renderer Rend;

    public void Spawn(Vector3 position, Quaternion rotation, float Speed, float Life_Time, AudioClip clip = null)
    {
        canDestroy = true;
        isFree = false;
        transform.position = position;
        transform.rotation = rotation;
        lifeTS = 0;
        this.Speed = Speed;
        this.Life_Time = Life_Time;
        velocity = transform.forward.normalized * Speed;
        if (clip != null)
        {
            Sound.clip = clip;
        }
        gameObject.SetActive(true);
        Sound.Play();
        Rend.enabled = true;
    }

    public override void Ancestor_Update()
    {
        base.Ancestor_Update();
        if (!isFree)
        {
            transform.position += velocity;
            lifeTS += Time.deltaTime;
            if (lifeTS >= Life_Time)
            {
                isFree = true;
                gameObject.SetActive(false);
            }
            if (canDestroy)
            {
                CheckCollision();
            }
        }
    }

    protected void CheckCollision()
    {
        
        Debug.DrawRay(transform.position, transform.forward * Ray_Length, Color.red);

        Ray ray = new Ray(transform.position, transform.forward * Ray_Length);
        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 20f, TargetMask);
        for (int i = 0; i < raycastHits.Length; ++i)
        {
            raycastHits[i].collider.transform.parent.GetComponent<Asteroid>().Annihilate();
            canDestroy = false;
            Rend.enabled = false;
            ++Overmind.AsteroidGameManager.score;
            break;
        }
    }
}
