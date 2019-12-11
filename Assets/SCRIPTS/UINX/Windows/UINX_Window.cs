using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVR;

public class UINX_SwitcherButtonData
{
    public string Name;
    public bool IsOn;
    public UINX_SwitcherButtonData(string Name, bool IsOn)
    {
        this.Name = Name;
        this.IsOn = IsOn;
    }
}

public class UINX_InitData : AncestorBehaviourInitData
{

}

public class UINX_Window : AncestorBehaviour
{
    public UINX_WindowsCanvas WindowsCanvas;
    public string ID = "";
    [SerializeField] protected RectTransform _transform = null;
    public RectTransform Transform { get { return _transform; } }
    [HideInInspector] public UINX_Window CallParent = null;

    [SerializeField] protected List<UINX_ButtonSocket> _buttonSockets = new List<UINX_ButtonSocket>();
    [HideInInspector] public List<UINX_Button> Buttons = new List<UINX_Button>();
    protected List<UINX_Button.OnUINX_ButtonClickDelegate> Delegates = new List<UINX_Button.OnUINX_ButtonClickDelegate>();
    protected bool buttonsSpawned = false;

    public UnityEngine.UI.Image Background = null;
    public float Size = 17f;

    public override void Init(AncestorBehaviourInitData initData = null)
    {
        base.Init(initData);
        Buttons.Clear();
        InitClickButtons();
        if (Application.isPlaying && !buttonsSpawned)
        {
            for (int i = 0; i < _buttonSockets.Count; ++i)
            {
                if (_buttonSockets[i].State != UINX_ButtonState.Nothing)
                {
                    if (_buttonSockets[i].Type == UINX_ButtonType.Switcher)
                    {
                        Buttons.Add(_buttonSockets[i].Spawn<UINX_ButtonSwitcher>(
                            new UINX_ButtonSwitcherInitData()
                            {
                                SocketRect = _buttonSockets[i].GetComponent<RectTransform>(),
                                BaseText = _buttonSockets[i].Text,
                                AddText = (int)_buttonSockets[i].AddText,
                                OnButtonClick = Delegates[i],
                                IsDisabled = _buttonSockets[i].State == UINX_ButtonState.ShowedDisabled,
                                ForceHide = _buttonSockets[i].State == UINX_ButtonState.Hidden
                            }));

                    } else if (_buttonSockets[i].Type == UINX_ButtonType.Classic)
                    {
                        Buttons.Add(_buttonSockets[i].Spawn<UINX_ButtonClassic>(
                            new UINX_ButtonClassicInitData()
                            {
                                SocketRect = _buttonSockets[i].GetComponent<RectTransform>(),
                                BaseText = _buttonSockets[i].Text,
                                OnButtonClick = Delegates[i],
                                IsDisabled = _buttonSockets[i].State == UINX_ButtonState.ShowedDisabled,
                                ForceHide = _buttonSockets[i].State == UINX_ButtonState.Hidden
                            }));
                    } else if (_buttonSockets[i].Type == UINX_ButtonType.Image)
                    {
                        Buttons.Add(_buttonSockets[i].Spawn<UINX_ButtonImage>(
                            new UINX_ButtonImageInitData()
                            {
                                SocketRect = _buttonSockets[i].GetComponent<RectTransform>(),
                                BaseText = _buttonSockets[i].Text,
                                OnButtonClick = Delegates[i],
                                IsDisabled = _buttonSockets[i].State == UINX_ButtonState.ShowedDisabled,
                                ForceHide = _buttonSockets[i].State == UINX_ButtonState.Hidden,
                                Material = _buttonSockets[i].Material
                            }));
                    } else if (_buttonSockets[i].Type == UINX_ButtonType.Slider)
                    {
                        //Buttons.Add(_buttonSockets[i].Spawn<UINX_Drag>());
                    }
                } else
                {
                    Buttons.Add(null);
                }
            }
            buttonsSpawned = true;
            SetupWindow(initData);
        }
    }

    protected virtual void InitClickButtons()
    {
        Delegates = new List<UINX_Button.OnUINX_ButtonClickDelegate>()
        {
            //1
            (button) =>
            {

            },
            //2
            (button) =>
            {

            },
            //3
            (button) =>
            {

            },
            //4
            (button) =>
            {

            },
            //5
            (button) =>
            {

            },
            //6
            (button) =>
            {

            },
            //7
            (button) =>
            {

            },
            //8
            (button) =>
            {

            },
            //9
            (button) =>
            {

            }
        };
    }

    protected virtual void InitSwitcherButtons(List<UINX_SwitcherButtonData> data)
    {
        if (data != null)
        {
            for (int i = 0; i < Buttons.Count; ++i)
            {
                if (Buttons[i] != null && Buttons[i].ButtonType == UINX_ButtonType.Switcher)
                {
                    var switcher = Buttons[i] as UINX_ButtonSwitcher;
                    var find = data.Find(x => { return x.Name == Buttons[i].name; });
                    if (find != null)
                    {
                        switcher.SwitchOnOff(find.IsOn);
                    }
                }
            }
        }
    }

    public virtual void SetupWindow(AncestorBehaviourInitData initData = null)
    {

    }

    public void ShowButtons()
    {
        for (int i = 0; i < Buttons.Count; ++i)
        {
            if (Buttons[i] != null && _buttonSockets[i].State != UINX_ButtonState.Hidden)
            {
                Buttons[i].ForceShow();
            }
        }
    }

    public void HideButtons()
    {
        foreach (UINX_Button but in Buttons)
        {
            if (but != null)
            {
                but.Hide();
            }
        }
    }

    public void DisableButtons()
    {
        foreach (UINX_Button but in Buttons)
        {
            if (but != null)
            {
                but.SetDisabled();
            }
        }
    }

    public void ObscureButtons()
    {
        foreach (UINX_Button but in Buttons)
        {
            if (but != null)
            {
                but.SetObscured();
            }
        }
    }

    public void EnableButtons()
    {
        for (int i = 0; i < Buttons.Count; ++i)
        {
            if (Buttons[i] != null && _buttonSockets[i].State != UINX_ButtonState.ShowedDisabled)
            {
                Buttons[i].SetEnabled();
            }
        }
    }

    public virtual void Show(AncestorBehaviourInitData initData = null)
    {
        gameObject.SetActive(true);
        if (IsInitialized)
        {
            ShowButtons();
            SetupWindow(initData);
            EnableButtons();
        } else
        {
            Init(initData);
            ShowButtons();
        }
    }

    public void ForceHide()
    {
        foreach (UINX_Button but in Buttons)
        {
            if (but != null)
            {
                but.ForceHide();
            }
        }
        Hide();
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        if (CallParent != null)
        {
            CallParent.EnableButtons();
        }
        CallParent = null;
    }

    #region == EDITOR ==
    public void InjectButtons()
    {
        foreach (UINX_ButtonSocket bs in _buttonSockets)
        {
            if (bs.Prefab != null)
            {
                bs.Spawn();
                bs.RescaleButtonToSocket();
            }
        }
    }

    public void ClearButtons()
    {
        foreach (ButtonSocket bs in _buttonSockets)
        {
            bs.Clear();
        }
    }

    public void GrabSockets()
    {
        _buttonSockets.Clear();
        FindInHierarchy(transform);
    }

    void FindInHierarchy(Transform t)
    {
        if (t == null)
        {
            return;
        }
        var bs = t.GetComponent<UINX_ButtonSocket>();
        if (bs != null)
        {
            _buttonSockets.Add(bs);
        }
        if (t.childCount > 0)
        {
            for (int i = 0; i < t.childCount; ++i)
            {
                FindInHierarchy(t.GetChild(i));
            }
        }
        return;
    }

    void FindInHierarchyReverse(Transform t)
    {
        if (t == null)
        {
            return;
        }
        var p = t.GetComponent<UINX_WindowsCanvas>();
        if (p != null)
        {
            WindowsCanvas = p;
        }
        if (t.parent != null)
        {
            FindInHierarchyReverse(t.parent);
        }
        return;
    }

    public void GrabProjection()
    {
        FindInHierarchyReverse(transform);
    }
    #endregion
}
