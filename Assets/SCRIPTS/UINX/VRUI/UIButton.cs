using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : UIElement
{
    public TextMesh TextMesh = null;
    public AudioSource AudioSource = null;    

    public delegate void OnButtonClickDelegate(UIButton button);

    public event OnButtonClickDelegate OnButtonClick;
    public MeshRenderer graphics;

    public float fadeTime = 0f;
    public float ts = 0;
    public Color defaultColor = Color.white;
    public Color rollOverColor = Color.blue;
    public bool IsDisabled = false;

    protected override void OnTouchDown()
    {
        base.OnTouchDown();

        if (OnButtonClick != null)
        {
            if(AudioSource != null)
            {
                AudioSource.Play();
            }

            OnButtonClick.Invoke(this);
        }        
    }

    public virtual void Update()
    {
        //ts += Time.deltaTime;
        //if (ts > fadeTime)
        //{
        //    graphics.material.color = defaultColor;
        //}
    }

    protected override void OnHover()
    {
        base.OnHover();
        //graphics.material.color = rollOverColor;        
    }

    public virtual void SetDisabled()
    {
        IsDisabled = true;
    }

    public virtual void SetEnabled()
    {
        IsDisabled = false;
    }

    public string Text { get { return TextMesh.text; } set { TextMesh.text = value; } }
}
