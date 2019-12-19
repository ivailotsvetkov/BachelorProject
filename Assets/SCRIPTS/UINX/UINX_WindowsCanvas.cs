using System.Collections;
using System.Collections.Generic;
using MVR;
using UnityEngine;

public class ShowUINX_Window : GameEvent
{
    public string ID = "";
    public bool Additive = false;
    public UINX_InitData SetupData = null;
}
public class HideUINX_Window : GameEvent
{
    public UINX_Window Window = null;
    public bool HideAll = false;
}

public class AssignCameraToCanvas : GameEvent
{
    public Camera Camera = null;
}

public class UINX_WindowsCanvas : AncestorBehaviour
{
    public RectTransform MyAnchor;
    public List<UINX_Window> ws = new List<UINX_Window>();
    List<UINX_Window> active_ws = new List<UINX_Window>();

    public UINX_ConfirmPopup ConfirmPopup;

    float windowsSize = 0f;
    [Header("Sounds")]
    public AudioSource PersistentClickSound;

    public override void Init(AncestorBehaviourInitData initData = null)
    {
        base.Init(initData);
        Overmind.EventsManager.AddListener<ShowUINX_Window>(OnShowUINX_Window);
        Overmind.EventsManager.AddListener<HideUINX_Window>(OnHideUINX_Window);
        Overmind.EventsManager.AddListener<AssignCameraToCanvas>(OnAssignCameraToCanvas);
        if (MyAnchor != null)
        {
            var comp = GetComponent<RectTransform>();
            comp.position = MyAnchor.position;
            comp.rotation = MyAnchor.rotation;
            comp.localScale = MyAnchor.localScale;
            comp.sizeDelta = MyAnchor.sizeDelta;
        }
    }
    public void Deactivate()
    {
        IsInitialized = false;
        HideWindows();
        Overmind.EventsManager.AddListener<ShowUINX_Window>(OnShowUINX_Window);
        Overmind.EventsManager.AddListener<HideUINX_Window>(OnHideUINX_Window);
        Overmind.EventsManager.AddListener<AssignCameraToCanvas>(OnAssignCameraToCanvas);
    }
    public override void BeforeDestroy()
    {
        base.BeforeDestroy();
        Overmind.EventsManager.RemoveListener<ShowUINX_Window>(OnShowUINX_Window);
        Overmind.EventsManager.RemoveListener<HideUINX_Window>(OnHideUINX_Window);
        Overmind.EventsManager.RemoveListener<AssignCameraToCanvas>(OnAssignCameraToCanvas);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (Overmind.EventsManager != null)
        {
            BeforeDestroy();
        }
    }

    protected void OnShowUINX_Window(ShowUINX_Window e)
    {
        if (e.Additive)
        {
            ShowWindowAdditional(e.ID, e.SetupData);
        } else
        {
            ShowWindow(e.ID, e.SetupData);
        }
    }

    protected void OnHideUINX_Window(HideUINX_Window e)
    {
        if (e.HideAll)
        {
            HideWindows();
        } else if (e.Window != null)
        {
            HideWindowAdditional(e.Window);
        }
    }

    public UINX_Window GetWindow(string ID)
    {
        foreach (var w in ws)
        {
            if (w.ID == ID)
            {
                return w;
            }
        }
        Debug.LogWarning("THERE IS NO WINDOW WITH ID: <" + ID + ">");
        return null;
    }

    public void ShowWindow(string ID, UINX_InitData SetupData)
    {
        var window = GetWindow(ID);
        if (window != null)
        {
            HideWindows();
            active_ws = new List<UINX_Window>() { window };
            window.Transform.anchoredPosition3D = new Vector3(window.Transform.anchoredPosition3D.x, window.Transform.anchoredPosition3D.y, 0f);
            windowsSize += window.Size;
            window.Show(SetupData);
        }
    }

    public void HideWindows()
    {
        for (int i = 0; i < active_ws.Count; ++i)
        {
            active_ws[i].ForceHide();
        }
        windowsSize = 0f;
    }

    public void ShowWindowAdditional(string ID, UINX_InitData SetupData)
    {
        var window = GetWindow(ID);
        if (window != null)
        {
            if (active_ws.Count > 0)
            {
                active_ws[active_ws.Count - 1].ObscureButtons();
                window.CallParent = active_ws[active_ws.Count - 1];
            }
            active_ws.Add(window);
            window.Transform.anchoredPosition3D = new Vector3(window.Transform.anchoredPosition3D.x, window.Transform.anchoredPosition3D.y, windowsSize);
            windowsSize += window.Size;
            window.Show(SetupData);
        }
    }

    public void HideWindowAdditional(UINX_Window window)
    {
        var index = active_ws.IndexOf(window);
        if (index != -1)
        {
            active_ws.RemoveAt(index);
            windowsSize -= window.Size;
            window.ForceHide();
        }
    }

    public void PlayPersistentClickSound(Vector3 position)
    {
        if (PersistentClickSound != null)
        {
            PersistentClickSound.transform.position = position;
            PersistentClickSound.Play();
        }
    }

    void OnAssignCameraToCanvas(AssignCameraToCanvas e)
    {
        this.GetComponent<Canvas>().worldCamera = e.Camera;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (MyAnchor != null)
        {
            var comp = GetComponent<RectTransform>();
            comp.position = MyAnchor.position;
            comp.rotation = MyAnchor.rotation;
            comp.localScale = MyAnchor.localScale;
            comp.sizeDelta = MyAnchor.sizeDelta;
        }
    }
#endif
}
