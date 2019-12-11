using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINX_PackLayout : MonoBehaviour
{
    public bool isRefreshing;
    public float padding = 44f;
    public float lastObjectAdditionalPadding = 0f;
    public float extraPadding = 0;
    public virtual float Pack()
    {
        var height = 0f;
        for (int i = 0; i < transform.childCount; ++i)
        {

            transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(
                0,
                -(transform.GetChild(i).localScale.y*transform.GetChild(i).GetComponent<RectTransform>().rect.height)/2 +
                -i * (transform.GetChild(i).localScale.y * transform.GetChild(i).GetComponent<RectTransform>().rect.height +
                padding +
                (i == transform.childCount - 1 ? lastObjectAdditionalPadding : 0))
                
            );
            height -= (transform.GetChild(i).localScale.y * transform.GetChild(i).GetComponent<RectTransform>().rect.height +
                padding + (i == transform.childCount - 1 ? lastObjectAdditionalPadding : 0)
            );
        }
        height += padding;
        isRefreshing = false;
        return height;
    }

    public virtual void RescaleToChildren()
    {
        transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, Pack() - extraPadding);
    }

    public void OnlyRescale()
    {
        var height = 0f;
        for (int i = 0; i < transform.childCount; ++i)
        {
            height -= (transform.GetChild(i).GetComponent<RectTransform>().rect.height +
                padding + (i == transform.childCount - 1 ? lastObjectAdditionalPadding : 0)
            );
        }
        transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, height - extraPadding);
    }
}
