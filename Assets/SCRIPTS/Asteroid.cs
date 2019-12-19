using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : AncestorBehaviour
{
    public bool isFree = true;
    public Transform Graphics;

    float Speed = 1;
    float lifeTS = 0;
    float Life_Time = 1;
    Vector3 velocity;
    public Vector3 Rotation_Axis;
    float Rotation_Speed = 1;

    public void Spawn(Vector3 position, float Speed, float Life_Time, AudioClip clip = null)
    {
        transform.localScale = Overmind.AsteroidGameManager.Asteroid_Spawn_Scale;
        isFree = false;
        transform.position = position;
        transform.LookAt(Overmind.PlayerStation.MainCamera.transform);
        lifeTS = 0;
        this.Speed = Speed;
        this.Life_Time = Life_Time;
        velocity = transform.forward.normalized * Speed;
        gameObject.SetActive(true);
        Rotation_Speed = Overmind.AsteroidGameManager.Asteroid_Rotation_Speed;
        Rotation_Axis = new Vector3(Random.Range(-1, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    public override void Ancestor_Update()
    {
        base.Ancestor_Update();
        if (!isFree)
        {
            Graphics.transform.Rotate(Rotation_Axis, Rotation_Speed);
            transform.position += velocity;
            lifeTS += Time.deltaTime;

            if (lifeTS >= Life_Time)
            {
                isFree = true;
                gameObject.SetActive(false);
            }
        }
    }

    public void Annihilate()
    {
        Overmind.AsteroidGameManager.SpawnExplosion(transform.position);
        isFree = true;
        gameObject.SetActive(false);
    }
}
