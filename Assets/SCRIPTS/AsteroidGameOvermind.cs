using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAsteroidGameState
{
    None,
    Tutorial,
    Loop,
    Win,
}

public class AsteroidGameOvermind : AncestorBehaviour
{
    public EAsteroidGameState GameState = EAsteroidGameState.None;

    public UINX_Score ScoreWindow;

    public int score = 0;

    public Transform Explos_Root;
    public Transform Asteroids_Root;
    public Transform Bullets_Root;

    public List<GameObject> AsteroidPrefabs = new List<GameObject>();
    public List<List<Asteroid>> Asteroids = new List<List<Asteroid>>();

    public List<Explosion> Explosions = new List<Explosion>();
    public List<AudioClip> ExplosionClips = new List<AudioClip>();
    public GameObject ExplosionPrefab;
    [Header("==SETUP==")]

    public float Game_Time = 10f;
    float gameTS = 0;
    public int Asteroids_At_Start = 10;
    public int Asteroids_Per_Spawn = 4;

    public float Asteroid_Min_Spawn_Time = 0.4f;
    public float Asteroid_Max_Spawn_Time = 1f;
    float Current_Asteroid_Spawn_Time = 1f;
    public float Asteroid_Spawn_Time { get { return Random.Range(Asteroid_Min_Spawn_Time, Asteroid_Max_Spawn_Time); } }
    float asteroidSpawnTS = 0;

    public float Bullet_Speed = 1f;
    public float Bullet_LifeTime = 5f;
    public float Round_Time = 3;
    public float Tutorial_Time = 7f;

    public float Asteroid_Min_Scale = 0.4f;
    public float Asteroid_Max_Scale = 1f;
    public Vector3 Asteroid_Spawn_Scale { get { float tmp = Random.Range(Asteroid_Min_Scale, Asteroid_Max_Scale); return new Vector3(tmp, tmp, tmp); } }

    public float Asteroid_Min_Distance = 10f;
    public float Asteroid_Max_Distance = 20f;
    public float Asteroid_Spawn_Distance { get { return Random.Range(Asteroid_Min_Distance, Asteroid_Max_Distance); } }

    public float Asteroid_Min_Speed = 1f;
    public float Asteroid_Max_Speed = 3f;
    public float Asteroid_Speed { get { return Random.Range(Asteroid_Min_Speed, Asteroid_Max_Speed); } }

    public float Asteroid_Min_Rotation_Speed = 1f;
    public float Asteroid_Max_Rotation_Speed = 3f;
    public float Asteroid_Rotation_Speed { get { return Random.Range(Asteroid_Min_Rotation_Speed, Asteroid_Max_Rotation_Speed); } }
    [Header("==SOUNDS==")]
    public AudioSource[] Sounds = new AudioSource[0];

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < AsteroidPrefabs.Count; ++i)
        {
            Asteroids.Add(new List<Asteroid>());
        }
    }

    public void SetGameState(EAsteroidGameState GameState)
    {
        this.GameState = GameState;
        switch (GameState)
        {
            case EAsteroidGameState.None:
                break;
            case EAsteroidGameState.Tutorial:
                break;
            case EAsteroidGameState.Loop:
                break;
            case EAsteroidGameState.Win:
                Overmind.PlayerStation.SetUINXMode(true);
                Overmind.EventsOvermind.Send(new ShowUINX_Window() { ID = "Score" });
                Sounds[3].Play();
                for (int i = 0; i < Asteroids.Count; ++i)
                {
                    for (int j = 0; j < Asteroids[i].Count; ++j)
                    {
                        if (!Asteroids[i][j].isFree)
                        {
                            Asteroids[i][j].Annihilate();
                        }
                    }
                }
                ScoreWindow.ScoreTxt.text = "Congratulations!\nYour score is:\n<size=60><color=white>"+score+"</color></size>";
                //Add score here with the score here with the score variable on line 19
                break;
        }
    }

    public void NextState()
    {
        ++GameState;
        SetGameState(GameState);
    }


    public override void Ancestor_Update()
    {
        base.Ancestor_Update();
        switch (GameState)
        {
            case EAsteroidGameState.Tutorial:
                break;
            case EAsteroidGameState.Loop:
                gameTS += Time.deltaTime;
                asteroidSpawnTS += Time.deltaTime;
                if (asteroidSpawnTS >= Current_Asteroid_Spawn_Time)
                {
                    Current_Asteroid_Spawn_Time = Asteroid_Spawn_Time;
                    asteroidSpawnTS = 0;
                    for (int i = 0; i < Asteroids_Per_Spawn; ++i)
                    {
                        float p = Random.Range(-Mathf.PI * 2, Mathf.PI * 2);
                        SpawnAsteroid(new Vector3(Mathf.Cos(p), 0, Mathf.Sin(p)) * Asteroid_Spawn_Distance);
                    }
                }
                if (gameTS >= Game_Time)
                {
                    SetGameState(EAsteroidGameState.Win);
                }
                break;
            case EAsteroidGameState.Win:
                break;
        }

    }

    public void NewGame()
    {
        score = 0;
        gameTS = 0;
        SetGameState(EAsteroidGameState.Loop);
        for (int i = 0; i < Asteroids_At_Start; ++i)
        {
            float p = Random.Range(-Mathf.PI * 2, Mathf.PI * 2);
            SpawnAsteroid(new Vector3(Mathf.Cos(p), 0, Mathf.Sin(p)) * Asteroid_Spawn_Distance);
        }
    }

    public void SpawnAsteroid(Vector3 position)
    {
        int r = Random.Range(0, AsteroidPrefabs.Count);
        bool found = false;
        for (int i = 0; i < Asteroids[r].Count; ++i)
        {
            if (Asteroids[r][i].isFree)
            {
                Asteroids[r][i].Spawn(position, Asteroid_Speed, 15f);
                found = true;
                break;
            }
        }
        if (!found)
        {
            var a = Instantiate(AsteroidPrefabs[r], Asteroids_Root).GetComponent<Asteroid>();
            a.Spawn(position, Asteroid_Speed, 15f);
            Asteroids[r].Add(a);
        }
    }
    public void SpawnExplosion(Vector3 position)
    {
        bool found = false;
        for (int i = 0; i < Explosions.Count; ++i)
        {
            if (Explosions[i].isFree)
            {
                Explosions[i].Spawn(position);
                found = true;
                break;
            }
        }
        if (!found)
        {
            var e = Instantiate(ExplosionPrefab, Explos_Root).GetComponent<Explosion>();
            e.Spawn(position, ExplosionClips[Random.Range(0, ExplosionClips.Count)]);
            Explosions.Add(e);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(SceneFinder.GetFromRoot<PlayerStation>().MainCamera.transform.position, Vector3.up, Asteroid_Max_Distance);
        UnityEditor.Handles.color = Color.magenta;
        UnityEditor.Handles.DrawWireDisc(SceneFinder.GetFromRoot<PlayerStation>().MainCamera.transform.position, Vector3.up, Asteroid_Min_Distance);
    }
#endif
}
