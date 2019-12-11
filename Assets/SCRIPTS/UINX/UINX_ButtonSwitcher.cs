using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVR;

public class UINX_ButtonSwitcherInitData : UINX_ButtonClassicInitData
{
    public int AddText = 0;
    public bool IsOn = true;
}

public struct SwitcherAddTexts
{
    public string Off;
    public string On;
    public SwitcherAddTexts(string Off, string On)
    {
        this.Off = Off;
        this.On = On;
    }
}

public class UINX_ButtonSwitcher : UINX_ButtonClassic
{
    static SwitcherAddTexts[] AddTextsArray = new SwitcherAddTexts[]
    {
        new SwitcherAddTexts("nothing", "nothing"),
        new SwitcherAddTexts("Off", "On"),
        new SwitcherAddTexts("Disabled", "Enabled"),
        new SwitcherAddTexts("Left Hand Mode", "Right Hand Mode"),
        new SwitcherAddTexts("High", "Fancy"),
        new SwitcherAddTexts("Stop", "Start")
    };
    protected int addText = 0;
    [HideInInspector] public bool IsOn = true;

    protected override void InnerInit(UINX_ButtonClassicInitData initData)
    {
        base.InnerInit(initData);
        var innerData = initData as UINX_ButtonSwitcherInitData;
        if (innerData != null)
        {
            addText = innerData.AddText;
            IsOn = innerData.IsOn;
            Text = baseText + (IsOn ? AddTextsArray[addText].On : AddTextsArray[addText].Off);
            OnButtonClick += SwitchOnOff;
        }
    }

    protected virtual void SwitchOnOff(UINX_Button button)
    {
        IsOn = IsOn ? false : true;
        Text = baseText + (IsOn ? AddTextsArray[addText].On : AddTextsArray[addText].Off);
    }

    public void SwitchOnOff(bool IsOn)
    {
        this.IsOn = IsOn;
        Text = baseText + (IsOn ? AddTextsArray[addText].On : AddTextsArray[addText].Off);
    }
}