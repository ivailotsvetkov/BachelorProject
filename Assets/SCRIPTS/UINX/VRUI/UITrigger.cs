using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVR;

public class UITrigger : AncestorBehaviour
{
    public enum EMode
    {
        Touch,
        Hover
    }

    [SerializeField] private UIElement _uiElement = null;
    public UIElement UIElement { get { return _uiElement; } }

    [SerializeField] private EMode _mode = EMode.Touch;
    public EMode Mode { get { return _mode; } }
}
