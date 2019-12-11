using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINX_Memory : UINX_Window
{
    public TextMeshProUGUI StateTxt;
    public TextMeshProUGUI ResultTxt;
    public Transform WinImg;
    public Transform LostImg;

    int drawTxtPointer = 0;
    public string Draw_String = "Draw!";
    public List<GameObject> figures = new List<GameObject>();
    public Transform EasySetup;
    public Transform MediumSetup;
    public Transform HardSetup;
    public Color[] DigitColors = new Color[9];

    public float Show_Time = 6f;
    float rotTS = 0;
    float Rotate_Time = 1.5f;
    float Rewind_Time = 0.65f;
    bool firstTime = true;

    float roundTS = 0;

    Vector3 Show_Rotation = new Vector3(0, 180, 0);
    Vector3 Hide_Rotation = new Vector3(0, 0, 0);
    List<int> toRotate = new List<int>();
    List<float> toRotateTS = new List<float>();
    List<int> toRewind = new List<int>();
    List<float> toRewindTS = new List<float>();
    public GameObject[] particles = new GameObject[2];
    public Vector3 Particles_Offset;

    protected override void InitClickButtons()
    {
        Delegates = new List<UINX_Button.OnUINX_ButtonClickDelegate>()
        { 
            OnReturnButton,
            //Real Slots
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
            OnFieldButton,
        };
    }

    public override void Init(AncestorBehaviourInitData initData = null)
    {
        int i = 1;
        switch (Overmind.MemoryGameOvermind.GameDifficulty)
        {
            case EGame.Easy:
                for (int j = 0; j < EasySetup.childCount; ++j, ++i)
                {
                    _buttonSockets[i].transform.position = EasySetup.GetChild(j).position;
                    _buttonSockets[i].State = UINX_ButtonState.Showed;
                }
                break;
            case EGame.Medium:
                for (int j = 0; j < MediumSetup.childCount; ++j, ++i)
                {
                    _buttonSockets[i].transform.position = MediumSetup.GetChild(j).position;
                    _buttonSockets[i].State = UINX_ButtonState.Showed;
                }
                break;
            case EGame.Hard:
                for (int j = 0; j < HardSetup.childCount; ++j, ++i)
                {
                    _buttonSockets[i].transform.position = HardSetup.GetChild(j).position;
                    _buttonSockets[i].State = UINX_ButtonState.Showed;
                }
                break;
        }
        for (; i < _buttonSockets.Count; ++i)
        {
            _buttonSockets[i].State = UINX_ButtonState.Hidden;
        }
        base.Init(initData);
    }

    public override void Show(AncestorBehaviourInitData initData = null)
    {
        int i = 1;
        if (IsInitialized)
        {
            switch (Overmind.MemoryGameOvermind.GameDifficulty)
            {
                case EGame.Easy:
                    for (int j = 0; j < EasySetup.childCount; ++j, ++i)
                    {
                        _buttonSockets[i].transform.position = EasySetup.GetChild(j).position;
                        _buttonSockets[i].State = UINX_ButtonState.Showed;
                    }
                    break;
                case EGame.Medium:
                    for (int j = 0; j < MediumSetup.childCount; ++j, ++i)
                    {
                        _buttonSockets[i].transform.position = MediumSetup.GetChild(j).position;
                        _buttonSockets[i].State = UINX_ButtonState.Showed;
                    }
                    break;
                case EGame.Hard:
                    for (int j = 0; j < HardSetup.childCount; ++j, ++i)
                    {
                        Buttons[i].transform.position = HardSetup.GetChild(j).position;
                        _buttonSockets[i].State = UINX_ButtonState.Showed;
                    }
                    break;
            }
            for (; i < _buttonSockets.Count; ++i)
            {
                _buttonSockets[i].State = UINX_ButtonState.Hidden;
            }
        }
        particles[0].SetActive(false);
        particles[1].SetActive(false);
        ResultTxt.gameObject.SetActive(false);
        base.Show(initData);

        Overmind.PlayerStation.SetUINXMode(false);
        firstTime = true;
        for (i = 1; i < Buttons.Count; ++i)
        {
            Buttons[i].transform.localEulerAngles = Show_Rotation;
        }
        switch (Overmind.MemoryGameOvermind.GameDifficulty)
        {
            case EGame.Easy:
                for (int j = 0; j < EasySetup.childCount; ++j)
                {
                    var ubc = (Buttons[j + 1] as UINX_ButtonClassic);
                    ubc.DigitTxt.text = Overmind.MemoryGameOvermind.Memories[j].ToString();
                    ubc.DigitTxt.color = DigitColors[Overmind.MemoryGameOvermind.Memories[j]];
                    ubc.Nr = j+1;
                }
                break;
            case EGame.Medium:
                for (int j = 0; j < MediumSetup.childCount; ++j)
                {
                    var ubc = (Buttons[j + 1] as UINX_ButtonClassic);
                    ubc.DigitTxt.text = Overmind.MemoryGameOvermind.Memories[j].ToString();
                    ubc.DigitTxt.color = DigitColors[Overmind.MemoryGameOvermind.Memories[j]];
                    ubc.Nr = j+1;
                }
                break;
            case EGame.Hard:
                for (int j = 0; j < HardSetup.childCount; ++j)
                {
                    var ubc = (Buttons[j + 1] as UINX_ButtonClassic);
                    ubc.DigitTxt.text = Overmind.MemoryGameOvermind.Memories[j].ToString();
                    ubc.DigitTxt.color = DigitColors[Overmind.MemoryGameOvermind.Memories[j]];
                    ubc.Nr = j+1;
                }
                break;
        }
        WinImg.gameObject.SetActive(false);
        LostImg.gameObject.SetActive(false);
        StateTxt.gameObject.SetActive(true);
        drawTxtPointer = 0;
        StateTxt.text = "Remember it!\nHidding in <color=red>" + Show_Time + "</color> sec!";
    }

    protected virtual void OnFieldButton(UINX_Button button)
    {
        button.ForceShow();
        button.SetDisabled();
        Overmind.MemoryGameOvermind.Sounds[2].Play();
        var classic = (button as UINX_ButtonClassic);
        toRotate.Add(classic.Nr);
        toRotateTS.Add(0);
        ++Overmind.MemoryGameOvermind.round;
        RefreshStateTxt();
        if (Overmind.MemoryGameOvermind.round % 2 == 0)
        {
            roundTS = 0;
        }
        Overmind.MemoryGameOvermind.NextState();
    }

    protected virtual void OnReturnButton(UINX_Button button)
    {
        Overmind.MemoryGameOvermind.Sounds[1].Play();
        Overmind.EventsOvermind.Send(new HideUINX_Window() { HideAll = true } );
        Overmind.PlayerStation.SetUINXMode(false);
        for (int i = 0; i < figures.Count; ++i)
        {
            Destroy(figures[i]);
        }
        figures.Clear();
    }

    public bool NextDrawLetter()
    {
        if (drawTxtPointer > Draw_String.Length)
        {
            return false;
        }
        StateTxt.text = Draw_String.Substring(0, drawTxtPointer);
        ++drawTxtPointer;
        if (drawTxtPointer > Draw_String.Length)
        {
            return false;
        }
        return true;
    }

    public override void Ancestor_Update()
    {
        base.Ancestor_Update();
        if (firstTime)
        {
            rotTS += Time.deltaTime;
            if (Overmind.MemoryGameOvermind.GameState == EMemoryGameState.ShowTime)
            {
                StateTxt.text = "Remember it!\nHidding in <color=red>" + Mathf.Round(Show_Time - rotTS) + "</color> sec!";
                if (rotTS >= Show_Time)
                {
                    Overmind.MemoryGameOvermind.Sounds[2].Play();
                    Overmind.MemoryGameOvermind.NextState();
                    StateTxt.text = "Hidding!";
                    rotTS = 0;
                }
            } else
            {
                for (int i = 1; i < Buttons.Count; ++i)
                {
                    Buttons[i].transform.localEulerAngles = Vector3.Lerp(Show_Rotation, Hide_Rotation, rotTS / Rotate_Time);
                }
                if (rotTS >= Rotate_Time)
                {
                    firstTime = false;
                    RefreshStateTxt();
                    Overmind.PlayerStation.SetUINXMode(true);
                }
            }
        } else
        {
            if (Overmind.MemoryGameOvermind.GameState != EMemoryGameState.Lose && Overmind.MemoryGameOvermind.GameState != EMemoryGameState.Win)
            {
                roundTS += Time.deltaTime;
                Overmind.MemoryGameOvermind.gameTS += Time.deltaTime;
                RefreshStateTxt();
                if (roundTS >= Overmind.MemoryGameOvermind.Round_Time)
                {
                    ++Overmind.MemoryGameOvermind.round;
                    roundTS = 0;
                }
            }
               
            int endlings = 0;
            for (int i = 0; i < toRotate.Count; ++i)
            {
                if (toRotateTS[i] < Rotate_Time)
                {
                    toRotateTS[i] += Time.deltaTime;
                    Buttons[toRotate[i]].transform.localEulerAngles = Vector3.Lerp(Hide_Rotation, Show_Rotation, toRotateTS[i] / Rotate_Time);
                } else if (toRotateTS[i] > Rotate_Time)
                {
                    toRotateTS[i] = Rotate_Time;
                } else
                {
                    ++endlings;
                }
            }
            if (endlings == 2)
            {
                if (Overmind.MemoryGameOvermind.Memories[toRotate[1] - 1] == Overmind.MemoryGameOvermind.Memories[toRotate[0] - 1])
                {
                    Overmind.MemoryGameOvermind.Sounds[4].Play();
                    ++Overmind.MemoryGameOvermind.score;
                    Buttons[toRotate[0]].Hide();
                    Buttons[toRotate[1]].Hide();
                    particles[0].transform.position = Buttons[toRotate[0]].transform.position + Particles_Offset;
                    particles[1].transform.position = Buttons[toRotate[1]].transform.position + Particles_Offset;
                    particles[0].SetActive(false);
                    particles[1].SetActive(false);
                    particles[0].SetActive(true);
                    particles[1].SetActive(true);
                    RefreshStateTxt();
                    Overmind.MemoryGameOvermind.NextState();
                } else
                {
                    toRewind.Add(toRotate[0]);
                    toRewind.Add(toRotate[1]);
                    toRewindTS.Add(0);
                    toRewindTS.Add(0);
                }
                toRotate.RemoveAt(0);
                toRotateTS.RemoveAt(0);
                toRotate.RemoveAt(0);
                toRotateTS.RemoveAt(0);
            }
            endlings = 0;
            for (int i = 0; i < toRewind.Count; ++i)
            {
                if (toRewindTS[i] < Rewind_Time)
                {
                    toRewindTS[i] += Time.deltaTime;
                    Buttons[toRewind[i]].transform.localEulerAngles = Vector3.Lerp(Show_Rotation, Hide_Rotation, toRewindTS[i] / Rewind_Time);
                } else if (toRewindTS[i] > Rewind_Time)
                {
                    Buttons[toRewind[i]].SetEnabled();
                    toRewindTS[i] = Rewind_Time;
                } else
                {
                    ++endlings;
                }
            }
            if (toRewind.Count > 0 && endlings == toRewind.Count)
            {
                toRewind.Clear();
                toRewindTS.Clear();
            }
        }
    }

    void RefreshStateTxt()
    {
        switch (Overmind.MemoryGameOvermind.GameDifficulty)
        {
            case EGame.Easy:
                StateTxt.text = Overmind.MemoryGameOvermind.score + " / " + (EasySetup.childCount / 2)+ "\nRound [" +(Overmind.MemoryGameOvermind.round / 2)+ "]\nTime left: <color=red>" + Mathf.Round(Overmind.MemoryGameOvermind.Round_Time - roundTS)+"</color>s";
                break;
            case EGame.Medium:
                StateTxt.text = Overmind.MemoryGameOvermind.score + " / " + (MediumSetup.childCount / 2)+ "\nRound [" +(Overmind.MemoryGameOvermind.round / 2)+ "]\nTime left: <color=red>" + Mathf.Round(Overmind.MemoryGameOvermind.Round_Time - roundTS) + "</color>s";
                break;
            case EGame.Hard:
                StateTxt.text = Overmind.MemoryGameOvermind.score + " / " + (HardSetup.childCount / 2)+ "\nRound [" +(Overmind.MemoryGameOvermind.round / 2)+ "]\nTime left: <color=red>" + Mathf.Round(Overmind.MemoryGameOvermind.Round_Time - roundTS) + "</color>s";
                break;
        }
    }
}
