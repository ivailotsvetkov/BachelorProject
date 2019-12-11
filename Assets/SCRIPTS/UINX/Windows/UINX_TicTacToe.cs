using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINX_TicTacToe : UINX_Window
{
    public TextMeshProUGUI StateTxt;
    public Transform WinImg;
    public Transform LostImg;

    int drawTxtPointer = 0;
    public string Draw_String = "Draw!";
    public List<GameObject> figures = new List<GameObject>();

    protected override void InitClickButtons()
    {
        Delegates = new List<UINX_Button.OnUINX_ButtonClickDelegate>()
        { 
            //LINES
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
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
            OnReturnButton,
        };
    }

    public override void Init(AncestorBehaviourInitData initData = null)
    {
        base.Init(initData);
        for (int i = 8, j = 0, k = 0; i < Buttons.Count; ++i, ++j)
        {
            if (j % 3 == 0 && j > 0)
            {
                ++k;
            }
            j %= 3;
            (Buttons[i] as UINX_ButtonClassic).gameY = j;
            (Buttons[i] as UINX_ButtonClassic).gameX = k;
        }
    }

    public override void Show(AncestorBehaviourInitData initData = null)
    {

        base.Show(initData);
        WinImg.gameObject.SetActive(false);
        LostImg.gameObject.SetActive(false);
        StateTxt.gameObject.SetActive(true);
        drawTxtPointer = 0;
    }

    protected virtual void OnFieldButton(UINX_Button button)
    {
        Overmind.TicGameOvermind.Sounds[2].Play();
        var classic = (button as UINX_ButtonClassic);
        button.Hide();
        if (Overmind.TicGameOvermind.playerIsX)
        {
            Overmind.TicGameOvermind.Fields[classic.gameX][classic.gameY] = EField.X;
            var x = Instantiate(Overmind.TicGameOvermind.XPrefab, transform);
            x.transform.position = classic.transform.position;
            x.transform.rotation = classic.transform.rotation;
            figures.Add(x);
        } else
        {
            Overmind.TicGameOvermind.Fields[classic.gameX][classic.gameY] = EField.O;
            var o = Instantiate(Overmind.TicGameOvermind.OPrefab, transform);
            o.transform.position = classic.transform.position;
            o.transform.rotation = classic.transform.rotation;
            figures.Add(o);
        }
        Overmind.TicGameOvermind.NextState();
    }
    protected virtual void OnReturnButton(UINX_Button button)
    {
        Overmind.TicGameOvermind.Sounds[1].Play();
        if (Overmind.TicGameOvermind.GameState == ETicGameState.Win)
        {
            Overmind.EventsOvermind.Send(new HideUINX_Window() { HideAll = true } );
            Overmind.PlayerStation.SetUINXMode(false);
        } else
        {
            Overmind.EventsOvermind.Send(new ShowUINX_Window() { ID = "TicPlayMenu" });
        }
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
}
