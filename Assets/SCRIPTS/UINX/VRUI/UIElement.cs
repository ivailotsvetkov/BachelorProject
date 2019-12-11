using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVR;

public class UIElement : AncestorBehaviour
{
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void ForceShow()
    {

    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsVisible()
    {
        return gameObject.activeSelf;
    }
    
    public void TouchDown()
    {
        OnTouchDown();
    }

    public void TouchUp()
    {
        OnTouchUp();
    }

    public virtual void Hover()
    {
        OnHover();
    }

    protected virtual void OnTouchDown()
    {

    }

    protected virtual void OnTouchUp()
    {

    }

    protected virtual void OnHover()
    {

    }
}
