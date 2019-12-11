using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINX_PackLayoutEnch : UINX_PackLayout
{
	void Update ()
    {
        if (isRefreshing)
        {

        }	
	}

    public override float Pack()
    {
        var height = 0f;
        var width = 0f;
        int row = 0;
        int column = 0;
        height -= transform.GetChild(0).GetComponent<RectTransform>().rect.height;
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (width + transform.GetChild(i).GetComponent<RectTransform>().rect.width + padding > transform.GetComponent<RectTransform>().rect.width)
            {
                ++row;
                column = 0;
                width = 0;
                height -= (transform.GetChild(i).GetComponent<RectTransform>().rect.height +
                    (i + 1 < transform.childCount ? padding : 0f)
                );
            }
            width += transform.GetChild(i).GetComponent<RectTransform>().rect.width + padding;
            transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2
            (
                width - transform.GetChild(i).GetComponent<RectTransform>().rect.width / 2 - padding,

                -transform.GetChild(i).GetComponent<RectTransform>().rect.height / 2 +
                -row * (transform.GetChild(i).GetComponent<RectTransform>().rect.height +
                padding)
            );

            ++column;
        }
        isRefreshing = false;
        return height;
    }
    public override void RescaleToChildren()
    {
        transform.GetComponent<RectTransform>().offsetMin = new Vector2(transform.GetComponent<RectTransform>().offsetMin.x, Pack() - extraPadding);
    }
}
