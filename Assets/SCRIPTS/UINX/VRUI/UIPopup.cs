using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : UIElement
{
    public TextMesh TextMesh = null;

    public string Text { get { return TextMesh.text; } set { TextMesh.text = value; } }
}
