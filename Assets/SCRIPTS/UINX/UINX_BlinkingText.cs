using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINX_BlinkingText : AncestorBehaviour
{
    public Text text;
    public bool IsBlinking = false;
    public float BlinkCap = 0.1f;
    public float BlinkSpeed = 0.1f;
    float blinkAlpha = 0f;
    bool blinkFlag = false;
    Color originColor;

    private void Awake()
    {
        originColor = text.color;
    }

    void Update ()
    {
        if (IsBlinking)
        {
            Blinking();
        }
        else
        {
            text.color = originColor;
        }
    }

    void Blinking()
    {
        if (blinkFlag)
        {
            blinkAlpha += BlinkSpeed;
            if (blinkAlpha >= originColor.a)
            {
                blinkAlpha = originColor.a;
                blinkFlag = false;
            }
        }
        else
        {
            blinkAlpha -= BlinkSpeed;
            if (blinkAlpha <= BlinkCap)
            {
                blinkAlpha = BlinkCap;
                blinkFlag = true;
            }
        }
        text.color = new Color(originColor.r, originColor.g, originColor.b, blinkAlpha);
    }
}
