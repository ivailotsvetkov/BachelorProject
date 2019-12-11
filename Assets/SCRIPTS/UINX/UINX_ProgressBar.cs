using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINX_ProgressBar : MonoBehaviour
{
    public RectTransform MyRect;
    public RectTransform BarRect;

    public void SetPercent(float percent)
    {
        percent = Mathf.Clamp(percent, 0f, 1f);
        BarRect.sizeDelta = new Vector2(-100 + percent * 98f, -1); 
    }

}
