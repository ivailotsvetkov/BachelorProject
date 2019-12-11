using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMemoryGameState
{
    None,
    ShowTime,
    Hide,
    Loop,
    First,
    Second,
    Win,
    Lose
}

public enum EGame
{
    Easy,
    Medium,
    Hard
}


public class MemoryGameOvermind : AncestorBehaviour
{
    public EMemoryGameState GameState = EMemoryGameState.None;
    public EGame GameDifficulty = EGame.Easy;

    public List<int> Memories = new List<int>();

    public UINX_Memory GameWindow;

    public float turnTS = 0;
    public float gameTS = 0;

    public int round = 0;
    public int score = 0;

    [Header("==SETUP==")]
    public float Round_Time = 3;
    [Header("==SOUNDS==")]
    public AudioSource[] Sounds = new AudioSource[0];

    protected override void Start()
    {
        base.Start();
    }

    public void SetGameState(EMemoryGameState GameState)
    {
        this.GameState = GameState;
        int result = 0;
        switch (GameState)
        {
            case EMemoryGameState.None:
                break;
            case EMemoryGameState.ShowTime:
                break;
            case EMemoryGameState.Hide:
                break;
            case EMemoryGameState.Loop:
                break;
            case EMemoryGameState.First:
                break;
            case EMemoryGameState.Second:
                break;
            case EMemoryGameState.Win:
                Overmind.PlayerStation.SetUINXMode(true);
                GameWindow.WinImg.gameObject.SetActive(true);
                GameWindow.StateTxt.gameObject.SetActive(false);
                GameWindow.Buttons[0].ForceShow();
                Sounds[3].Play();
                for (int i = 1; i < GameWindow.Buttons.Count; ++i)
                {
                    GameWindow.Buttons[i].Hide();
                }
                GameWindow.ResultTxt.gameObject.SetActive(true);
                GameWindow.ResultTxt.text = "You finished game in\n<color=red>" + (round / 2) + "</color> rounds\nin <color=red>" + (Mathf.Round(gameTS*100)/100) + "</color> secs!";
                break;
            case EMemoryGameState.Lose:
                Overmind.PlayerStation.SetUINXMode(true);
                GameWindow.LostImg.gameObject.SetActive(true);
                GameWindow.StateTxt.gameObject.SetActive(false);
                GameWindow.Buttons[0].ForceShow();
                Sounds[5].Play();
                for (int i = 1; i < GameWindow.Buttons.Count; ++i)
                {
                    GameWindow.Buttons[i].Hide();
                }
                GameWindow.ResultTxt.gameObject.SetActive(true);
                GameWindow.ResultTxt.text = "You finished game in\n<color=red>" + (round/2) + "</color> rounds\nin <color=red>" + (Mathf.Round(gameTS * 100) / 100) + "</color> secs!";
                break;
        }
    }

    public void NextState()
    {
        switch (GameDifficulty)
        {
            case EGame.Easy:
                if (score == 5)
                {
                    SetGameState(EMemoryGameState.Win);
                    return;
                }
                break;
            case EGame.Medium:
                if (score == 7)
                {
                    SetGameState(EMemoryGameState.Win);
                    return;
                }
                break;
            case EGame.Hard:
                if (score == 9)
                {
                    SetGameState(EMemoryGameState.Win);
                    return;
                }
                break;
        }
        if (GameState == EMemoryGameState.Second)
        {
            SetGameState(EMemoryGameState.Loop);
        } else
        {
            ++GameState;
            SetGameState(GameState);
        }
    }


    public override void Ancestor_Update()
    {
        base.Ancestor_Update();
        switch (GameState)
        {
            case EMemoryGameState.ShowTime:
                break;
            case EMemoryGameState.Hide:
                break;
            case EMemoryGameState.Loop:
                break;
            case EMemoryGameState.First:
                break;
            case EMemoryGameState.Second:
                break;
            case EMemoryGameState.Lose:
                break;
            case EMemoryGameState.Win:
                break;
        }

    }

    public void NewGame(EGame GameDifficulty)
    {
        this.GameDifficulty = GameDifficulty;
        score = 0;
        round = 0;
        gameTS = 0;
        GameWindow.StateTxt.fontSize = 36;
        Memories.Clear();
        switch (GameDifficulty)
        {
            case EGame.Easy:
                Randomness(5);
                break;
            case EGame.Medium:
                Randomness(7);
                break;
            case EGame.Hard:
                Randomness(9);
                break;
        }
        SetGameState(EMemoryGameState.ShowTime);
    }

    public void Randomness(int number)
    {
        int number2 = number * 2;
        for (int i = 0; i < number2; ++i)
        {
            int counter = 2;
            int r = Random.Range(0, number);
            while (counter == 2)
            {
                counter = 0;
                r = Random.Range(0, number);
                for (int j = 0; j < Memories.Count; ++j)
                {
                    if (Memories[j] == r)
                    {
                        ++counter;
                    }
                }
                if (counter < 2)
                {
                    Memories.Add(r);
                }
            }
        }
    }
}
