using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINX_MemoryPlayMenu : UINX_Window
{
    protected override void InitClickButtons()
    {
        Delegates = new List<UINX_Button.OnUINX_ButtonClickDelegate>()
        {            
            OnPlayButton,
            OnPlayEasyButton,
            OnPlayMediumButton,
            OnPlayHardButton
        };
    }

    protected virtual void OnPlayButton(UINX_Button button)
    {
        button.Hide();
        Buttons[1].ForceShow();
        Buttons[2].ForceShow();
        Buttons[3].ForceShow();
        Overmind.MemoryGameManager.Sounds[0].Play();
    }

    protected virtual void OnPlayEasyButton(UINX_Button button)
    {
        Overmind.MemoryGameManager.NewGame(EGame.Easy);
        Overmind.EventsManager.Send(new ShowUINX_Window() { ID = "Memory" });
        Overmind.MemoryGameManager.Sounds[0].Play();
    }
    protected virtual void OnPlayMediumButton(UINX_Button button)
    {
        Overmind.MemoryGameManager.NewGame(EGame.Medium);
        Overmind.EventsManager.Send(new ShowUINX_Window() { ID = "Memory" });
        Overmind.MemoryGameManager.Sounds[0].Play();
    }
    protected virtual void OnPlayHardButton(UINX_Button button)
    {
        Overmind.MemoryGameManager.NewGame(EGame.Hard);
        Overmind.EventsManager.Send(new ShowUINX_Window() { ID = "Memory" });
        Overmind.MemoryGameManager.Sounds[0].Play();
    }

    public override void Show(AncestorBehaviourInitData initData = null)
    {
        base.Show(initData);
        Overmind.GetInstance.SetGame(EGames.Memory);
    }
}
