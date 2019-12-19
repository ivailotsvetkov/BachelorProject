using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVR;

public class UINXController : AncestorBehaviour
{
    public LineRenderer[] LineRenderer;
    public Transform End;
    public Renderer EndGraph;
    public GameObject Begin;
    public float Ray_Length; 
    
    [SerializeField] private Transform _transform = null;
    public Transform Transform { get { return _transform; } }

    public bool IsHovering { get; set; }

    public bool raycastActivated = false;
    UINX_Button lastUIButton;
    SpotTrigger lastSpotTrigger;

    public LayerMask TargetMask;
    public LayerMask SpotTargetMask;
    Vector3[] defaultLinePositions = new Vector3[2];

    public Material[] Default_Materials = new Material[3];

    public Material[] Tick_Materials = new Material[3];
    public Material[] Asteroid_Materials = new Material[3];
    public Material[] Memory_Materials = new Material[3];
    public Material[] Spot1_Materials = new Material[3];
    public Material[] Spot2_Materials = new Material[3];

    void OnUIRaycastStateEvent(UIRaycastStateEvent e)
    {
        raycastActivated = e.Activated;

        if (raycastActivated == false)
        {
            lastUIButton = null;
            lastSpotTrigger = null;
            LineRenderer[0].gameObject.SetActive(false);
            LineRenderer[1].gameObject.SetActive(false);
            End.gameObject.SetActive(false);
            Begin.SetActive(false);
        } else
        {
            Begin.SetActive(true);
            LineRenderer[0].gameObject.SetActive(true);
            LineRenderer[1].gameObject.SetActive(true);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (Overmind.EventsManager != null)
        {
            Overmind.EventsManager.RemoveListener<UIRaycastStateEvent>(OnUIRaycastStateEvent);
        }
    }

    public override void Ancestor_Update()
    {
        base.Ancestor_Update();
        if (raycastActivated)
        {
            CheckRayCastForUI();
            if (Overmind.GetInstance.CurrentGame == EGames.Spot)
            {
                CheckRayCastForSpot();
            }
            if (Input.GetKeyDown(KeyCode.B) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                TryClick();
                if (Overmind.GetInstance.CurrentGame == EGames.Asteroid)
                {
                    Overmind.PlayerStation.Fire();
                }
            }
        }
    }

    protected void CheckRayCastForUI()
    {
        Debug.DrawRay(Transform.position, Transform.right * Ray_Length, Color.red);

        Ray ray = new Ray(Transform.position, Transform.right * Ray_Length);
        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 20f, TargetMask);
        bool flag = false;
        for (int i = 0; i < raycastHits.Length; ++i)
        {
            var uiTrigger = raycastHits[i].collider.gameObject.GetComponent<UITrigger>();
            if (uiTrigger != null)
            {
                UINX_Button uiButton = uiTrigger.UIElement.GetComponent<UINX_Button>();
                if (uiButton != null && !uiButton.IsDisabled)
                {
                    flag = true;
                    if (lastUIButton != null && lastUIButton != uiButton)
                    {
                        lastUIButton.ts = lastUIButton.fadeTime;
                    }
                    lastUIButton = uiButton;
                    uiButton.ts = 0;
                    uiButton.Hover();
                    End.gameObject.SetActive(true);
                    End.transform.position = raycastHits[i].point;
                    defaultLinePositions[0] = LineRenderer[0].transform.position;
                    defaultLinePositions[1] = End.transform.position;
                    break;
                }
            }
        }
        if (!flag)
        {
            lastUIButton = null;
            End.gameObject.SetActive(false);
            defaultLinePositions[0] = LineRenderer[0].transform.position;
            defaultLinePositions[1] = Transform.position + transform.right * Ray_Length;
        }
        LineRenderer[0].SetPositions(defaultLinePositions);
        LineRenderer[1].SetPositions(defaultLinePositions);
    }

    protected void CheckRayCastForSpot()
    {
        Debug.DrawRay(Transform.position, Transform.right * Ray_Length, Color.red);

        Ray ray = new Ray(Transform.position, Transform.right * Ray_Length);
        RaycastHit[] raycastHits = Physics.RaycastAll(ray, 20f, SpotTargetMask);
        bool flag = false;
        bool good = false;
        for (int i = 0; i < raycastHits.Length; ++i)
        {
            var trigger = raycastHits[i].collider.gameObject.GetComponent<SpotTrigger>();
            if (trigger != null && trigger.Correct && !trigger.Found)
            {
                good = true;
                flag = true;
                lastSpotTrigger = trigger;
                break;
            } else
            {
                lastSpotTrigger = trigger;
                flag = true;
            }
        }
        if (!flag)
        {
            lastSpotTrigger = null;
            LineRenderer[0].material = Spot1_Materials[0];
            LineRenderer[1].material = Spot1_Materials[1];
            EndGraph.material = Spot1_Materials[2];
        } else
        {
            LineRenderer[0].material = Spot2_Materials[0];
            LineRenderer[1].material = Spot2_Materials[1];
            EndGraph.material = Spot2_Materials[2];
        }
        LineRenderer[0].SetPositions(defaultLinePositions);
        LineRenderer[1].SetPositions(defaultLinePositions);
    }

    public void TryClick()
    {
        if (lastUIButton != null)
        {
            lastUIButton.TouchDown();
        }
        if (lastSpotTrigger != null)
        {
            lastSpotTrigger.Click();
        }
    }

    public void SetColors(EGames Game)
    {
        switch (Game)
        {
            case EGames.TicTackToe:
                LineRenderer[0].material = Tick_Materials[0];
                LineRenderer[1].material = Tick_Materials[1];
                EndGraph.material = Tick_Materials[2];
                break;
            case EGames.Asteroid:
                LineRenderer[0].material = Asteroid_Materials[0];
                LineRenderer[1].material = Asteroid_Materials[1];
                EndGraph.material = Asteroid_Materials[2];
                break;
            case EGames.Memory:
                LineRenderer[0].material = Memory_Materials[0];
                LineRenderer[1].material = Memory_Materials[1];
                EndGraph.material = Memory_Materials[2];
                break;
            case EGames.Spot:
                break;
        }
    }
}
