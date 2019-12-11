using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINX_SpotPlayMenu : UINX_Window
{
    protected override void InitClickButtons()
    {
        Delegates = new List<UINX_Button.OnUINX_ButtonClickDelegate>()
        {            
            OnPlayButton
        };
    }

    public override void Show(AncestorBehaviourInitData initData = null)
    {
        base.Show(initData);
        Overmind.GetInstance.SetGame(EGames.Spot);
    }

    protected virtual void OnPlayButton(UINX_Button button)
    {
        button.Hide();
        Overmind.SpotGameOvermind.NewGame();
        Overmind.SpotGameOvermind.Sounds[0].Play();
        Overmind.EventsOvermind.Send(new ShowUINX_Window() { ID = "Spot" });
    }
}
