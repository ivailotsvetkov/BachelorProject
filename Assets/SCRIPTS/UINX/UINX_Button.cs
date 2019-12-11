using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum UINX_ButtonType
{
    Classic,
    Switcher,
    Image,
    Slider
}

public enum UINX_ButtonState
{
    Showed,
    ShowedDisabled,
    Hidden,
    Nothing
}

public enum UINX_AddTextEnum
{
    Nothing, 
    Off_On,
    Enabled_Disabled,
    Right_LeftHand,
    Fancy_High,
    Stop_Start
}

public class UINX_Button : UIElement
{
    public UINX_ButtonType ButtonType;
    public RectTransform Rect;
    [HideInInspector] public GameObject MySocket = null;

    public AudioSource AudioSource = null;

    public delegate void OnUINX_ButtonClickDelegate(UINX_Button button);

    public event OnUINX_ButtonClickDelegate OnButtonClick;

    public float fadeTime = 0f;
    public float ts = 0;
    public bool IsDisabled = false;

    protected override void OnTouchDown()
    {
        base.OnTouchDown();

        if (OnButtonClick != null)
        {
            if (AudioSource != null)
            {
                AudioSource.Play();
            }

            OnButtonClick.Invoke(this);
        }
    }

    //public virtual void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        TouchDown();
    //    }
    //}

    protected override void OnHover()
    {
        base.OnHover();
    }

    public virtual void SetDisabled()
    {
        IsDisabled = true;
    }

    public virtual void SetEnabled()
    {
        IsDisabled = false;
    }

    public virtual void SetObscured()
    {
        IsDisabled = false;
    }

    public virtual void ForceHide()
    {

    }
}