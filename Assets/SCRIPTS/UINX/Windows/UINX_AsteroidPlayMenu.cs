using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINX_AsteroidPlayMenu : UINX_Window
{
    protected override void InitClickButtons()
    {
        Delegates = new List<UINX_Button.OnUINX_ButtonClickDelegate>()
        {            
            OnPlayButton,
        };
    }

    protected virtual void OnPlayButton(UINX_Button button)
    {
        button.Hide();
        Overmind.AsteroidGameManager.Sounds[0].Play();
        Overmind.AsteroidGameManager.SetGameState(EAsteroidGameState.Tutorial);
        Overmind.EventsManager.Send(new ShowUINX_Window() { ID = "Tutorial" });
    }
    public override void Show(AncestorBehaviourInitData initData = null)
    {
        base.Show(initData);
        Overmind.GetInstance.SetGame(EGames.Asteroid);
    }
}
