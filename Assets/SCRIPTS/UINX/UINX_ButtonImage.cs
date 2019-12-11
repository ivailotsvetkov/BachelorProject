using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MVR;


public class UINX_ButtonImageInitData : UINX_ButtonClassicInitData
{
    public Material Material = null;
}

public class UINX_ButtonImage : UINX_ButtonClassic
{
    [Header("Image")]
    public Image img;
    public Material ImageMaterial;

    protected override void InnerInit(UINX_ButtonClassicInitData initData)
    {
        base.InnerInit(initData);
        var innerData = initData as UINX_ButtonImageInitData;
        if (innerData != null)
        {
            if (innerData.Material != null)
            {
                ImageMaterial = innerData.Material;
                SetupImage(ImageMaterial);
            }
        }
    }

    protected override void SetButtonHoverColor()
    {
        img.material.color = HoverTextColor;
        Background.material.color = HoverBgColor;
    }

    protected override void SetButtonDefaultColor()
    {
        img.material.color = DefaultTextColor;
        Background.material.color = DefaultBgColor;
    }

    protected override void SetButtonDisabledColor()
    {
        img.material.color = DisabledTextColor;
        Background.material.color = DisabledBgColor;
    }

    protected override void RefreshBlinkAlpha()
    {
        base.RefreshBlinkAlpha();
        img.material.color = new Color(DefaultBgColor.r, DefaultBgColor.g, DefaultBgColor.b, blinkAlpha);
    }

    public void SetupImage(Material material)
    {
        img.material = material;
    }
}