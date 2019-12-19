using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MVR;
using System;

public class UINX_ConfirmPopupSetupData : UINX_InitData
{
    public string Text = "";
    public Action YesAddAction = null;
    public Action NoAddAction = null;
    public UINX_Window CallParent = null;
}

public class UINX_ConfirmPopup : UINX_Window
{
    public Text TextContent;
    protected Action YesAddAction;
    protected Action NoAddAction;

    public override void Init(AncestorBehaviourInitData initData = null)
    {
        base.Init(initData);
    }

    protected override void InitClickButtons()
    {
        Delegates = new List<UINX_Button.OnUINX_ButtonClickDelegate>()
        {
            //Yes
            OnYesButton,
            
            //No
            OnNoButton,
        };
    }

    protected virtual void OnYesButton(UINX_Button button)
    {
        if (YesAddAction != null)
        {
            YesAddAction();
        }
        ForceHide();
    }

    protected virtual void OnNoButton(UINX_Button button)
    {
        if (NoAddAction != null)
        {
            NoAddAction();
        }
        Overmind.EventsManager.Send(new HideUINX_Window() { Window = this});
    }

    public override void SetupWindow(AncestorBehaviourInitData initData = null)
    {
        base.SetupWindow(initData);
        if (initData != null)
        {
            var setupData = initData as UINX_ConfirmPopupSetupData;
            if (setupData != null)
            {
                if (setupData.Text != null)
                {
                    TextContent.text = setupData.Text;
                }
                if (setupData.YesAddAction != null)
                {
                    YesAddAction = setupData.YesAddAction;
                }
                if (setupData.NoAddAction != null)
                {
                    NoAddAction = setupData.NoAddAction;
                }
                if (setupData.CallParent != null)
                {
                    CallParent = setupData.CallParent;
                }
            }
        }
    }
}
