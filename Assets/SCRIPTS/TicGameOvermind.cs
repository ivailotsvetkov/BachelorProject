using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETicGameState
{
    None,
    PlayerTurn,
    EnemyTurnDelay,
    EnemyTurn,
    Win,
    Lose,
    Draw
}

public enum EField
{
    Empty,
    O,
    X
}


public class TicGameOvermind : AncestorBehaviour
{
    public ETicGameState GameState = ETicGameState.None;

    public EField[][] Fields = new EField[3][];

    public UINX_TicTacToe GameWindow;
    public GameObject OPrefab;
    public GameObject XPrefab;

    public bool playerIsX = false;

    public float turnTS = 0;

    public float Draw_Letter_Time = 0.1f;

    [Header("==SETUP==")]
    public float Enemy_Turn_Delay = 1f;
    public float Enemy_Turn_Time = 1.5f;
    [Header("==SOUNDS==")]
    public AudioSource[] Sounds = new AudioSource[0];

    protected override void Start()
    {
        base.Start();
        ClearFields(true);
    }

    public void SetGameState(ETicGameState GameState)
    {
        this.GameState = GameState;
        int result = 0;
        switch (GameState)
        {
            case ETicGameState.None:
                break;
            case ETicGameState.PlayerTurn:
                result = CheckIfWin();
                if (result == -1)
                {
                    SetGameState(ETicGameState.Lose);
                } else if (result == 1)
                {
                    SetGameState(ETicGameState.Win);
                } else if (result == 2)
                {
                    SetGameState(ETicGameState.Draw);
                } else
                {
                    GameWindow.StateTxt.text = "Your turn!";
                    Overmind.PlayerStation.SetUINXMode(true);
                }
                break;
            case ETicGameState.EnemyTurnDelay:
                result = CheckIfWin();
                if (result == -1)
                {
                    SetGameState(ETicGameState.Lose);
                } else if (result == 1)
                {
                    SetGameState(ETicGameState.Win);
                } else if (result == 2)
                {
                    SetGameState(ETicGameState.Draw);
                } else
                {
                    List<int> empty = new List<int>();
                    for (int i = 0; i < 3; ++i)
                    {
                        for (int j = 0; j < 3; ++j)
                        {
                            if (Fields[i][j] == EField.Empty)
                            {
                                empty.Add(i);
                                empty.Add(j);
                            }
                        }
                    }
                    if (empty.Count == 0)
                    {
                        SetGameState(ETicGameState.Draw);
                    } else
                    {
                        GameWindow.StateTxt.text = "Enemy turn!";
                        Overmind.PlayerStation.SetUINXMode(false);
                        turnTS = 0;
                    }
                }
                break;
            case ETicGameState.EnemyTurn:
                
                turnTS = 0;
                break;
            case ETicGameState.Win:
                Overmind.PlayerStation.SetUINXMode(true);
                GameWindow.WinImg.gameObject.SetActive(true);
                GameWindow.StateTxt.gameObject.SetActive(false);
                for (int i = 0; i < GameWindow.Buttons.Count - 1; ++i)
                {
                    GameWindow.Buttons[i].SetDisabled();
                }
                (GameWindow.Buttons[GameWindow.Buttons.Count - 1] as UINX_ButtonClassic).Text = "Next";
                GameWindow.Buttons[GameWindow.Buttons.Count - 1].ForceShow();
                Sounds[3].Play();
                break;
            case ETicGameState.Lose:
                Sounds[5].Play();
                Overmind.PlayerStation.SetUINXMode(true);
                GameWindow.LostImg.gameObject.SetActive(true);
                GameWindow.StateTxt.gameObject.SetActive(false);
                for (int i = 0; i < GameWindow.Buttons.Count - 1; ++i)
                {
                    GameWindow.Buttons[i].SetDisabled();
                }
                (GameWindow.Buttons[GameWindow.Buttons.Count - 1] as UINX_ButtonClassic).Text = "Again";
                GameWindow.Buttons[GameWindow.Buttons.Count - 1].ForceShow();
                break;
            case ETicGameState.Draw:
                Sounds[4].Play();
                Overmind.PlayerStation.SetUINXMode(true);
                GameWindow.StateTxt.fontSize = 72;
                GameWindow.StateTxt.gameObject.SetActive(true);
                for (int i = 0; i < GameWindow.Buttons.Count - 1; ++i)
                {
                    GameWindow.Buttons[i].SetDisabled();
                }
                (GameWindow.Buttons[GameWindow.Buttons.Count - 1] as UINX_ButtonClassic).Text = "Again";
                GameWindow.Buttons[GameWindow.Buttons.Count - 1].ForceShow();
                break;
        }
    }

    public void NextState()
    {
        if (GameState == ETicGameState.EnemyTurn)
        {
            SetGameState(ETicGameState.PlayerTurn);
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
            case ETicGameState.PlayerTurn:
                break;
            case ETicGameState.EnemyTurnDelay:
                turnTS += Time.deltaTime;
                if (turnTS >= Enemy_Turn_Delay)
                {
                    SetGameState(ETicGameState.EnemyTurn);
                }
                break;
            case ETicGameState.EnemyTurn:
                turnTS += Time.deltaTime;
                if (turnTS >= Enemy_Turn_Time)
                {
                    List<int> empty = new List<int>();
                    for (int i = 0; i < 3; ++i)
                    {
                        for (int j = 0; j < 3; ++j)
                        {
                            if (Fields[i][j] == EField.Empty)
                            {
                                empty.Add(i);
                                empty.Add(j);
                            }
                        }
                    }
                    int rand = Random.Range(0, empty.Count / 2);
                    for (int i = 8; i < GameWindow.Buttons.Count; ++i)
                    {
                        Overmind.TicGameOvermind.Sounds[2].Play();
                        var classic = (GameWindow.Buttons[i] as UINX_ButtonClassic);
                        if (classic.gameX == empty[rand * 2] && classic.gameY == empty[rand * 2 + 1])
                        {
                            classic.Hide();
                            if (playerIsX)
                            {
                                Fields[classic.gameX][classic.gameY] = EField.O;
                                var o = Instantiate(OPrefab, classic.MySocket.transform.parent);
                                o.transform.position = classic.transform.position;
                                o.transform.rotation = classic.transform.rotation;
                                GameWindow.figures.Add(o);
                            } else
                            {
                                Fields[classic.gameX][classic.gameY] = EField.X;
                                var x = Instantiate(XPrefab, classic.MySocket.transform.parent);
                                x.transform.position = classic.transform.position;
                                x.transform.rotation = classic.transform.rotation;
                                GameWindow.figures.Add(x);
                            }
                        }
                    }
                    SetGameState(ETicGameState.PlayerTurn);
                }
                break;
            case ETicGameState.Win:
                break;
            case ETicGameState.Lose:
                break;
            case ETicGameState.Draw:
                turnTS += Time.deltaTime;
                if (turnTS > Draw_Letter_Time)
                {
                    if (GameWindow.NextDrawLetter())
                    {
                        turnTS = 0;
                    } else
                    {
                        turnTS = Draw_Letter_Time;
                    }
                }
                break;
        }

    }

    public void NewGame(bool playerIsX)
    {
        this.playerIsX = playerIsX;
        ClearFields();
        GameWindow.StateTxt.fontSize = 36;
        if (playerIsX)
        {
            SetGameState(ETicGameState.EnemyTurnDelay);
        } else
        {
            SetGameState(ETicGameState.PlayerTurn);
        }
    }


    public void ClearFields(bool firstTime = false)
    {
        for (int i = 0; i < 3; ++i)
        {
            if (firstTime)
            {
                Fields[i] = new EField[3];
            }
            for (int j = 0; j < 3; ++j)
            {
                Fields[i][j] = EField.Empty;
            }
        }
    }

    public int CheckIfWin()
    {
        bool emptyFound = false;
        bool oWin = false;
        bool xWin = false;
        int[] oMatch = { 0, 0, 0, 0 };
        int[] xMatch = { 0, 0, 0, 0 };
        for (int i = 0; i < 3; ++i)
        {
            xMatch[3] = 0;
            oMatch[3] = 0;
            for (int j = 0; j < 3; ++j)
            {
                if (Fields[i][j] == EField.Empty)
                {
                    emptyFound = true;
                } else if (Fields[i][j] == EField.O)
                {
                    ++oMatch[3];
                    if (oMatch[3] == 3)
                    {
                        oWin = true;
                    }
                } else
                {
                    ++xMatch[3];
                    if (xMatch[3] == 3)
                    {
                        xWin = true;
                    }
                }
            }
        }
        if (!xWin && !oWin)
        {
            for (int i = 0; i < 3; ++i)
            {
                xMatch[3] = 0;
                oMatch[3] = 0;
                for (int j = 0; j < 3; ++j)
                {
                    if (Fields[j][i] == EField.Empty)
                    {
                        emptyFound = true;
                    } else if (Fields[j][i] == EField.O)
                    {
                        ++oMatch[3];
                        if (oMatch[3] == 3)
                        {
                            oWin = true;
                        }
                    } else
                    {
                        ++xMatch[3];
                        if (xMatch[3] == 3)
                        {
                            xWin = true;
                        }
                    }
                }
            }
        }

        if (!xWin && !oWin)
        {
            //sprawdzam na ukos
            if ((Fields[0][0] == EField.O && Fields[1][1] == EField.O && Fields[2][2] == EField.O)
                || (Fields[2][0] == EField.O && Fields[1][1] == EField.O && Fields[0][2] == EField.O))
            {
                oWin = true;
            }
            if ((Fields[0][0] == EField.X && Fields[1][1] == EField.X && Fields[2][2] == EField.X)
                || (Fields[2][0] == EField.X && Fields[1][1] == EField.X && Fields[0][2] == EField.X))
            {
                xWin = true;
            }

            if (oMatch[0] + oMatch[1] + oMatch[2] >= 3)
            {
                oWin= true;
            } else if (xMatch[0] + xMatch[1] + xMatch[2] >= 3)
            { 
                xWin = true;
            }
        }
        if (xWin)
        {
            if (playerIsX)
            {
                print("You win!");
                return 1;
            } else
            {
                print("You lost!");
                return -1;
            }
        } else if (oWin)
        {
            if (!playerIsX)
            {
                print("You win!");
                return 1;
            } else
            {
                print("You lost!");
                return -1;
            }
        } else if (!emptyFound && !xWin && !oWin)
        {
            print("Draw");
            return 2;
        }
        return 0;
    }
}
