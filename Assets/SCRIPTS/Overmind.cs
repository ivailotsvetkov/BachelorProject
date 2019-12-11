using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvent
{
}

public enum EGames
{
    TicTackToe,
    Asteroid,
    Memory,
    Spot
}

public class Overmind : MonoBehaviour
{
    public static Overmind _instance = null;
    public static List<AncestorBehaviour> AncestorsToUpdate = new List<AncestorBehaviour>();
    public static List<AncestorBehaviour> AncestorsToLateUpdate = new List<AncestorBehaviour>();
    public static List<AncestorBehaviour> AncestorsToFixedUpdate = new List<AncestorBehaviour>();

    public static List<AncestorBehaviour> AncestorsBeforeDestroy = new List<AncestorBehaviour>();

    private EventsOvermind<GameEvent> _eventsOvermind = new EventsOvermind<GameEvent>();

    public PlayerStation _playerStation = null;
    public static PlayerStation PlayerStation { get { return GetInstance._playerStation; } private set { GetInstance._playerStation = value; } }

    public UINX_WindowsCanvas WindowsCanvas;

    public EGames CurrentGame = EGames.TicTackToe;
    public void SetGame(EGames Game)
    {
        switch(Game)
        {
            case EGames.TicTackToe:
                PlayerStation.SwitchAsteroidMode(false);
                break;
            case EGames.Asteroid:
                PlayerStation.SwitchAsteroidMode(true);
                break;
            case EGames.Memory:
                PlayerStation.SwitchAsteroidMode(false);
                break;
            case EGames.Spot:
                PlayerStation.SwitchAsteroidMode(false);
                break;
        }
        PlayerStation.SetColors(Game);
        CurrentGame = Game;
    }
    public TicGameOvermind _ticGameOvermind;
    public static TicGameOvermind TicGameOvermind
    {
        get
        {
            return GetInstance._ticGameOvermind;
        }
    }

    public AsteroidGameOvermind _asteroidGameOvermind;
    public static AsteroidGameOvermind AsteroidGameOvermind
    {
        get
        {
            return GetInstance._asteroidGameOvermind;
        }
    }

    public MemoryGameOvermind _memoryGameOvermind;
    public static MemoryGameOvermind MemoryGameOvermind
    {
        get
        {
            return GetInstance._memoryGameOvermind;
        }
    }

    public SpotGameOvermind _spotGameOvermind;
    public static SpotGameOvermind SpotGameOvermind
    {
        get
        {
            return GetInstance._spotGameOvermind;
        }
    }


    public static EventsOvermind<GameEvent> EventsOvermind
    {
        get
        {
            var i = GetInstance;
            if (i != null)
            {
                return GetInstance._eventsOvermind;
            }
            return null;
        }
    }

    public static Overmind GetInstance
    {
        get
        {
            if (_instance == null)
            {
                var overminds = Resources.FindObjectsOfTypeAll<Overmind>();
                if (overminds.Length > 1)
                {
                    Debug.Log("THERE IS MORE THAN 1 <Overmind> ON THE SCENE!!");
                    return null;
                } else if (overminds.Length == 0)
                {
                    Debug.Log("THERE IS NO <Overmind> ON THE SCENE!!");
                    return null;
                }
                _instance = overminds[0];
            }
            return _instance;
        }
    }

    private void Awake()
    {
        WindowsCanvas.Init();
        PlayerStation.SetUINXMode(true);
        EventsOvermind.Send(new ShowUINX_Window() { ID = "SpotPlayMenu" });//change here fo example change id for MemoryPlayMenu or other play menu name
    }

    private void Update()
    {
        for (int i = 0; i < AncestorsToUpdate.Count; ++i)
        {
            if (AncestorsToUpdate[i] != null)
            {
                AncestorsToUpdate[i].Ancestor_Update();
            }
        }
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < AncestorsToFixedUpdate.Count; ++i)
        {
            if (AncestorsToFixedUpdate[i] != null)
            {
                AncestorsToFixedUpdate[i].Ancestor_FixedUpdate();
            }
        }
    }
    private void LateUpdate()
    {
        for (int i = 0; i < AncestorsToLateUpdate.Count; ++i)
        {
            if (AncestorsToLateUpdate[i] != null)
            {
                AncestorsToLateUpdate[i].Ancestor_LateUpdate();
            }
        }
    }
    public void Destroy(GameObject go)
    {
        var abs = go.GetComponentsInChildren<AncestorBehaviour>();
        foreach (AncestorBehaviour ab in abs)
        {
            ab.BeforeDestroy();
        }
        Destroy(go);
    }

    protected void OnDestroy()
    {
        for (int i = 0; i < AncestorsBeforeDestroy.Count; ++i)
        {
            if (AncestorsBeforeDestroy[i] != null)
            {
                AncestorsBeforeDestroy[i].BeforeDestroy();
            }
        }
    }

    public static string GetFormattedTime(float time, out uint minutes, out uint seconds)
    {
        minutes = 0;
        seconds = 0;

        time /= 60.0f;

        minutes = ((uint)time) % 60;
        seconds = ((uint)(time * 60.0f) % 60);
        //miliseconds = ((uint)(time * 60.0f * 1000.0f) % 1000);

        return string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));//, miliseconds.ToString("000"));
    }

    public static string GetFormattedTime(float time)
    {
        uint minutes = 0;
        uint seconds = 0;

        return GetFormattedTime(time, out minutes, out seconds);
    }

    public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
    {
        Vector3 AB = b - a;
        Vector3 AV = value - a;
        return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
    }

    public static List<AudioSource> playingSources = new List<AudioSource>();
}