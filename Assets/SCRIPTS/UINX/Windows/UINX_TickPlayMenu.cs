using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINX_TickPlayMenu : UINX_Window
{
    protected override void InitClickButtons()
    {
        Delegates = new List<UINX_Button.OnUINX_ButtonClickDelegate>()
        {            
            OnPlayOButton,
            OnPlayXButton
        };
    }

    public override void Show(AncestorBehaviourInitData initData = null)
    {
        base.Show(initData);
        Overmind.GetInstance.SetGame(EGames.TicTackToe);
    }

    protected virtual void OnPlayOButton(UINX_Button button)
    {
        Overmind.EventsManager.Send(new ShowUINX_Window() { ID = "TicTacToe" });
        Overmind.TicGameManager.NewGame(false);
        Overmind.TicGameManager.Sounds[0].Play();
    }
    protected virtual void OnPlayXButton(UINX_Button button)
    {
        Overmind.EventsManager.Send(new ShowUINX_Window() { ID = "TicTacToe" });
        Overmind.TicGameManager.NewGame(true);
        Overmind.TicGameManager.Sounds[0].Play();
    }
}
