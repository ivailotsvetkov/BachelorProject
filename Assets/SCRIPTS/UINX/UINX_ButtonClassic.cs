using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVR;

public class UINX_ButtonClassicInitData : AncestorBehaviourInitData
{
    public RectTransform SocketRect = null;
    public string BaseText = "";
    public bool ForceHide = false;
    public bool IsDisabled = false;
    public UINX_Button.OnUINX_ButtonClickDelegate OnButtonClick = null;
}

public class UINX_ButtonClassic : UINX_Button
{
    public bool Hovered { get; protected set; }
    public bool Clicked { get; protected set; }

    public SimpleAnim TouchDownAnim = null;
    public Transform TextAnimTransform = null;

    public SimpleAnim TouchDownAnimInstance = null;

    public Transform Transform = null;

    public SimpleAnimInstance ShowAnim = null;

    public bool ClickLock { get; set; }

    protected Vector3 _originButtonScale = Vector3.one;
    protected Vector3 _originTextScale = new Vector3(0.135f, 0.135f, 0.135f);

    public int gameX = 0;
    public int gameY = 0;

    [Header("Text")]
    protected string baseText = "";
    public Vector2 TextScaleFactor = Vector2.one; 
    public UnityEngine.UI.Text TextMesh = null;
    public Color DefaultTextColor;
    public Color HoverTextColor;
    public Color DisabledTextColor;

    [Header("Background")]
    public Renderer Background;
    public Color DefaultBgColor;
    public Color HoverBgColor;
    public Color DisabledBgColor;

    [Header("Blinking")]
    public bool IsBlinking = false;
    public float BlinkCap = 0.1f;
    public float BlinkSpeed = 0.1f;
    protected float blinkAlpha = 0f;
    protected bool blinkFlag = false;

    public AudioSource HoverAudioSource = null;

    public int Nr = 0;
    public TMPro.TextMeshProUGUI DigitTxt;

    public override void Init(AncestorBehaviourInitData initData = null)
    {
        base.Init(initData);

        _originButtonScale = Transform.localScale;
        _originTextScale = TextAnimTransform.localScale;
        Clicked = false;
        TouchDownAnimInstance = TouchDownAnim.Instantiate();
        TouchDownAnimInstance.OnAnimEnd += OnAnimEnd;

        ShowAnim.Instance.OnAnimEnd += OnShowAnimEnd;

        if (initData != null)
        {
            var buttonInitData = initData as UINX_ButtonClassicInitData;

            if (buttonInitData != null)
            {
                if (buttonInitData.SocketRect != null)
                {
                   (Transform as RectTransform).sizeDelta = buttonInitData.SocketRect.sizeDelta;
                   MySocket = buttonInitData.SocketRect.gameObject;
                }
                if (buttonInitData.BaseText != null)
                {
                    Text = buttonInitData.BaseText;
                    baseText = Text;
                }
                if (buttonInitData.OnButtonClick != null)
                {
                    OnButtonClick += buttonInitData.OnButtonClick;
                }
                if (buttonInitData.ForceHide)
                {
                    ForceHide();
                }
                if (buttonInitData.IsDisabled)
                {
                    SetDisabled();
                }
                InnerInit(buttonInitData);
            }
        }
    }

    protected virtual void InnerInit(UINX_ButtonClassicInitData initData) { }

    public virtual void Awake()
    {
        if (!IsInitialized)
        {
            _originButtonScale = Transform.localScale;
            _originTextScale = TextAnimTransform.localScale;
        }
    }

    public virtual void Start()
    {
        if (!IsInitialized)
        {
            Clicked = false;
            TouchDownAnimInstance = TouchDownAnim.Instantiate();
            TouchDownAnimInstance.OnAnimEnd += OnAnimEnd;

            ShowAnim.Instance.OnAnimEnd += OnShowAnimEnd;
        }
    }

    protected virtual void OnShowAnimEnd(SimpleAnim anim)
    {
        if (anim.Reversed)
        {
            Transform.localScale = Vector3.zero;
            TextAnimTransform.localScale = Vector3.zero;
            gameObject.SetActive(false);
        } else
        {
            Transform.localScale = _originButtonScale;
            TextAnimTransform.localScale = _originTextScale;

        }
    }

    public override void Hide()
    {
        ShowAnim.Instance.PlayReversed();
    }

    protected virtual void OnAnimEnd(SimpleAnim anim)
    {
        Hide();
    }

    public override void ForceHide()
    {
        base.ForceHide();
        TouchDownAnimInstance.Stop();
        ShowAnim.Instance.StopReversed();
    }

    protected override void OnHover()
    {
        base.OnHover();
        Hovered = true;
    }

    protected void Blinking()
    {
        if (blinkFlag)
        {
            blinkAlpha += BlinkSpeed;
            if (blinkAlpha >= DefaultTextColor.a)
            {
                blinkAlpha = DefaultTextColor.a;
                blinkFlag = false;
            }
        } else
        {
            blinkAlpha -= BlinkSpeed;
            if (blinkAlpha <= BlinkCap)
            {
                blinkAlpha = BlinkCap;
                blinkFlag = true;
            }
        }
        RefreshBlinkAlpha();
    }

    public override void Ancestor_Update()
    {
        base.Ancestor_Update();
        if (ShowAnim.Instance.State == SimpleAnim.EState.Playing)
        {
            ShowAnim.Instance.Tick(Time.deltaTime);

            Transform.localScale = new Vector3(_originButtonScale.x * ShowAnim.Instance.Evaluate(0),
                                               _originButtonScale.y * ShowAnim.Instance.Evaluate(1),
                                               _originButtonScale.z);
        }
        if (!IsDisabled)
        {
            if (ClickLock)
            {
                if (Hovered || Clicked)
                {
                    SetButtonHoverColor();
                } else
                {
                    if (IsBlinking)
                    {
                        Blinking();
                    } else
                    {
                        SetButtonDefaultColor();
                    }
                }
            } else
            {
                if (Hovered)
                {
                    SetButtonHoverColor();
                } else
                {
                    if (IsBlinking)
                    {
                        Blinking();
                    } else
                    {
                        SetButtonDefaultColor();
                    }
                }

            }
        }

        if (!_prevHovered && Hovered)
        {
            if (!HoverAudioSource.isPlaying)
            {
                HoverAudioSource.Play();
            }
        }

        _prevHovered = Hovered;

        Hovered = false;
        if (TextAnimTransform.localScale != _originTextScale)
        {
            TextAnimTransform.localScale = _originTextScale * TouchDownAnimInstance.Tick(Time.deltaTime);
        }
    }

    protected override void OnTouchDown()
    {
        if ((ClickLock && !Clicked) || !ClickLock)
        {
            TouchDownAnimInstance.Play();
            Clicked = true;

            base.OnTouchDown();
        }
    }

    public override void Show()
    {
        base.Show();
        Clicked = false;
        Transform.localScale = new Vector3(0.0f, 0.0f, _originButtonScale.z);
        TextAnimTransform.localScale = new Vector3(0, 0, 0);
        ShowAnim.Instance.Play();
    }

    public override void ForceShow()
    {
        base.ForceShow();
        TouchDownAnimInstance.Stop();
        Show();
    }

    public override void SetDisabled()
    {
        base.SetDisabled();
        SetButtonDisabledColor();
    }

    protected virtual void RefreshBlinkAlpha()
    {
        TextMesh.color = new Color(DefaultTextColor.r, DefaultTextColor.g, DefaultTextColor.b, blinkAlpha);
        Background.material.color = new Color(DefaultBgColor.r, DefaultBgColor.g, DefaultBgColor.b, blinkAlpha);
    }

    protected virtual void SetButtonHoverColor()
    {
        TextMesh.color = HoverTextColor;
        Background.material.color = HoverBgColor;
    }

    protected virtual void SetButtonDefaultColor()
    {
        TextMesh.color = DefaultTextColor;
        Background.material.color = DefaultBgColor;
    }

    protected virtual void SetButtonDisabledColor()
    {
        TextMesh.color = DisabledTextColor;
        Background.material.color = DisabledBgColor;
    }

    public void RescaleText()
    {
        var myRect = GetComponent<RectTransform>();
        var textRect = TextMesh.GetComponent<RectTransform>();

        textRect.sizeDelta = new Vector2(myRect.sizeDelta.x * TextScaleFactor.x, myRect.sizeDelta.y * TextScaleFactor.y);
    }

    public void GetTextScaleFactor()
    {
        var myRect = GetComponent<RectTransform>();
        var textRect = TextMesh.GetComponent<RectTransform>();
        TextScaleFactor = new Vector2(textRect.sizeDelta.x / myRect.sizeDelta.x, textRect.sizeDelta.y / myRect.sizeDelta.y);
    }

    private bool _prevHovered = false;
    public string Text { get { return TextMesh.text; } set { TextMesh.text = value; RescaleText(); } }
}