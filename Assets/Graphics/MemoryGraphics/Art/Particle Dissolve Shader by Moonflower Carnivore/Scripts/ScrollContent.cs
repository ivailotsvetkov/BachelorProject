using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollContent : MonoBehaviour
{
    public RectTransform rectTransform;
    public Scrollbar scrollBar;
    public RectTransform content;

    public void Move()
    {
        if (content.rect.height > rectTransform.rect.height)
        {
            content.anchoredPosition = new Vector2(0, scrollBar.value * (content.rect.height - rectTransform.rect.height));
        }
    }
}
