using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESpotGameState
{
    None,
    Win
}



public class SpotGameOvermind : AncestorBehaviour
{
    public ESpotGameState GameState = ESpotGameState.None;

    public UINX_Spot GameWindow;

    public float turnTS = 0;
    public float gameTS = 0;

    public int score = 0;
    public int Max_Score = 0;

    public int Pictures_To_Spot = 5;
    public int Current_Picture = 0;
    [Header("==SETUP==")]
    [Header("==SOUNDS==")]
    public AudioSource[] Sounds = new AudioSource[0];

    protected override void Start()
    {
        base.Start();
    }

    public void SetGameState(ESpotGameState GameState)
    {
        this.GameState = GameState;
        int result = 0;
        switch (GameState)
        {
            case ESpotGameState.None:
                break;
            case ESpotGameState.Win:
                GameWindow.Buttons[0].ForceHide();
                GameWindow.Buttons[1].ForceShow();
                Sounds[3].Play();
                GameWindow.ScoreTxt.text = "Nice!\nYour score is:\n<size=80><color=green>" + score + "</color> <color=white>/ "+Max_Score+"</color></size>";
                //Add score using score and Max_Score lines 22 and 23
                break;
        }
    }

    public override void Ancestor_Update()
    {
        base.Ancestor_Update();
        switch (GameState)
        {
            case ESpotGameState.Win:
                break;
        }

    }

    public void NewGame()
    {
        score = 0;
        gameTS = 0;
        Current_Picture = -1;
        Overmind.SpotGameOvermind.Max_Score = 0;
        SetGameState(ESpotGameState.None);
    }
}
