using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINX_PreludiumMenu : UINX_Window
{
    protected override void InitClickButtons()
    {
        Delegates = new List<UINX_Button.OnUINX_ButtonClickDelegate>()
        {            
            OnPlayButton
        };
    }

    protected virtual void OnPlayButton(UINX_Button button)
    {
        button.Hide();
        Overmind.TicGameOvermind.Sounds[0].Play();
        switch (Overmind.GetInstance.CurrentGame)
        {
            case EGames.TicTackToe:
                Overmind.EventsOvermind.Send(new ShowUINX_Window() { ID = "TicPlayMenu" });
                break;
            case EGames.Asteroid: 
                Overmind.EventsOvermind.Send(new ShowUINX_Window() { ID = "AsteroidPlayMenu" });
                break;
            case EGames.Memory:
                Overmind.EventsOvermind.Send(new ShowUINX_Window() { ID = "MemoryPlayMenu" });
                break;
            case EGames.Spot:
                Overmind.EventsOvermind.Send(new ShowUINX_Window() { ID = "SpotPlayMenu" });
                break;
        }
    }
}
