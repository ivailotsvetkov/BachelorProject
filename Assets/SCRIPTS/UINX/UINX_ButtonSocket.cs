using System.Collections;
using System.Collections.Generic;
using MVR;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UINX_ButtonSocket : ButtonSocket
{
    public void RescaleButtonToSocket()
    {
        var myRect = GetComponent<RectTransform>();
        for (int i = 0; i < transform.childCount; ++i)
        {
            var child = transform.GetChild(i);
            child.GetComponent<RectTransform>().sizeDelta = myRect.sizeDelta;
            var bc = child.GetComponent<UINX_ButtonClassic>();
            if (bc != null)
            {
                if (Type == UINX_ButtonType.Classic)
                {
                    bc.Text = Text;
                } else if (Type == UINX_ButtonType.Switcher)
                {
                    var switcher = bc as UINX_ButtonSwitcher;
                    switcher.SwitchOnOff(switcher.IsOn);
                }
            }
        }
    }

    public void SetAsEmpty()
    {
        name = "Empty";
        State = UINX_ButtonState.Nothing;
        Text = "";
        AddText = UINX_AddTextEnum.Nothing;
        Type = UINX_ButtonType.Classic;
        Prefab = null;
    }
}
