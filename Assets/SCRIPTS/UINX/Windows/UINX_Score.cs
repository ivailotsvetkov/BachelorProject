using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINX_Score : UINX_Window
{
    public TMPro.TextMeshProUGUI ScoreTxt;

    protected override void InitClickButtons()
    {
        Delegates = new List<UINX_Button.OnUINX_ButtonClickDelegate>()
        {            
            OnMenuButton
        };
    }

    protected virtual void OnMenuButton(UINX_Button button)
    {
        button.Hide();
        Overmind.AsteroidGameManager.Sounds[0].Play();
        Overmind.EventsManager.Send(new HideUINX_Window() { HideAll = true } );
        Overmind.PlayerStation.SetUINXMode(false);
    }
}
