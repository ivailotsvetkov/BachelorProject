using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINX_DecisionMenu : UINX_Window
{
    public SphereManager SphereManager;
    public float distance = 1f;
    public override void Show(AncestorBehaviourInitData initData = null)
    {
        base.Show(initData);
        Vector3 v3 = Overmind.PlayerStation.MainCamera.transform.forward;
        v3.y = 0;
        transform.position = Overmind.PlayerStation.MainCamera.transform.position + v3 * distance;
    }

    protected override void InitClickButtons()
    {
        Delegates = new List<UINX_Button.OnUINX_ButtonClickDelegate>()
        {            
            OnDecision1Button,
            OnDecision2Button
        };
    }

    protected virtual void OnDecision1Button(UINX_Button button)
    {
        Overmind.TicGameManager.Sounds[0].Play();
        SphereManager.makeDecisionFromMenu(1);
        Hide();
    }

    protected virtual void OnDecision2Button(UINX_Button button)
    {
        Overmind.TicGameManager.Sounds[0].Play();
        SphereManager.makeDecisionFromMenu(2);
        Hide();
    } 

    public void SetButton1Text(string s)
    {

        if (IsInitialized)
        {
            (Buttons[0] as UINX_ButtonClassic).Text = s;
        }
    }

    public void SetButton2Text(string s)
    {
        if (IsInitialized)
        {
            (Buttons[1] as UINX_ButtonClassic).Text = s;
        }
    }
}
