using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINX_Spot : UINX_Window
{
    public TextMeshProUGUI StateTxt;
    public TextMeshProUGUI ScoreTxt;

    public TextMeshProUGUI Good;
    float goodTS = 0;
    public Vector3 Good_Origin_Scale;
    public Vector3 Good_Target_Scale;
    public Color[] Good_Colors = new Color[2];
    public float Good_Anim_Time;

    public Transform Bad;
    float badTS = 0;
    public Vector3 Bad_Origin_Scale;
    public Vector3 Bad_Target_Scale;
    public float Bad_Anim_Time = 1.5f;
    int badState = -1;

    public int chances = 2;
    int foundDiffs = 0;
    public Transform WinImg;
    public Transform LostImg;


    public Transform PictureRoot;
    public List<GameObject> PicturePrefabs = new List<GameObject>();
    List<int> drawed = new List<int>();
    public SpotPicture Picture;

    protected override void InitClickButtons()
    {
        Delegates = new List<UINX_Button.OnUINX_ButtonClickDelegate>()
        { 
            OnNoButton,
            OnReturnButton
        };
    }

    public override void Show(AncestorBehaviourInitData initData = null)
    {
        base.Show(initData);
        drawed.Clear();
        Overmind.EventsOvermind.AddListener<SpotEvent>(OnSpotEvent);
        RefreshStateTxt();
        NextPicture();
        StateTxt.gameObject.SetActive(true);
        ScoreTxt.gameObject.SetActive(false);
        Bad.gameObject.SetActive(false);
        Good.gameObject.SetActive(false);
    }

    public override void Hide()
    {
        base.Hide();
        Overmind.EventsOvermind.RemoveListener<SpotEvent>(OnSpotEvent);
    }

    protected virtual void OnReturnButton(UINX_Button button)
    {
        Overmind.SpotGameOvermind.Sounds[1].Play();
        Overmind.GetInstance.SetGame(EGames.TicTackToe);
        Overmind.EventsOvermind.Send(new HideUINX_Window() { HideAll = true } );
        Overmind.PlayerStation.SetUINXMode(false);
    }

    protected virtual void OnNoButton(UINX_Button button)
    {
        Overmind.SpotGameOvermind.Sounds[0].Play();
        if (Picture.Differences == 0)
        {
            ++Overmind.SpotGameOvermind.score;
            Overmind.SpotGameOvermind.Sounds[2].Play();
        }
        NextPicture();
    }

    public override void Ancestor_Update()
    {
        base.Ancestor_Update();
        if (goodTS > Good_Anim_Time)
        {
            goodTS = Good_Anim_Time;
            Good.gameObject.SetActive(false);
        } else if (goodTS < Good_Anim_Time)
        {
            goodTS += Time.deltaTime;
            Good.transform.localScale = Vector3.Lerp(Good_Origin_Scale, Good_Target_Scale, goodTS / Good_Anim_Time);
            Good.color = Color.Lerp(Good_Colors[0], Good_Colors[1], goodTS / Good_Anim_Time);
        }
        if (badState == 1)
        {
            if (badTS > Bad_Anim_Time)
            {
                badTS = 0;
                badState = 0;
            } else if (badTS < Bad_Anim_Time)
            {
                badTS += Time.deltaTime;
                Bad.transform.localScale = Vector3.Lerp(Bad_Origin_Scale, Bad_Target_Scale, badTS / Bad_Anim_Time);
            }
        } else if (badState == 0)
        {
            if (badTS > Bad_Anim_Time/2)
            {
                badTS = Bad_Anim_Time / 2;
                Bad.gameObject.SetActive(false);
                badState = -1;
            } else if (badTS < Bad_Anim_Time / 2)
            {
                badTS += Time.deltaTime;
                Bad.transform.localScale = Vector3.Lerp(Bad_Target_Scale, Bad_Origin_Scale, badTS / (Bad_Anim_Time / 2));
            }
        }
    }

    public void OnSpotEvent(SpotEvent e)
    {
        if (e.Success)
        {
            Overmind.SpotGameOvermind.Sounds[2].Play();
            Good.transform.position = e.Pos;
            goodTS = 0;
            Good.transform.localScale = Good_Origin_Scale;
            Good.gameObject.SetActive(true);
            ++Overmind.SpotGameOvermind.score;
            ++foundDiffs;
            if (foundDiffs >= Picture.Differences)
            {
                NextPicture();
            }
        } else
        {
            Overmind.SpotGameOvermind.Sounds[5].Play();
            badState = 1;
            Bad.localScale = Bad_Origin_Scale;
            badTS = 0;
            Bad.gameObject.SetActive(true);
            --chances;
            if (chances <= 0)
            {
                NextPicture();
            }
            RefreshStateTxt();
        }
    }

    void NextPicture()
    {
        ++Overmind.SpotGameOvermind.Current_Picture;
        if (Picture != null)
        {
            Destroy(Picture.gameObject);
            Picture = null;
        }
        if (Overmind.SpotGameOvermind.Current_Picture >= Overmind.SpotGameOvermind.Pictures_To_Spot)
        {
            StateTxt.gameObject.SetActive(false);
            ScoreTxt.gameObject.SetActive(true);
            Overmind.SpotGameOvermind.SetGameState(ESpotGameState.Win);
        } else
        {
            int r = Random.Range(0, PicturePrefabs.Count);
            while (drawed.IndexOf(r) != -1)
            {
                r = Random.Range(0, PicturePrefabs.Count);
            }
            drawed.Add(r);
            Picture = Instantiate(PicturePrefabs[r], PictureRoot).GetComponent<SpotPicture>();
            chances = 2;
            foundDiffs = 0;
            Overmind.SpotGameOvermind.Max_Score += Picture.Differences == 0 ? 1 : Picture.Differences;
        }
        RefreshStateTxt();
    }

    void RefreshStateTxt()
    {
        StateTxt.text = "Find the difference in the pictures below (click only on the left)\n<color=red>It is possible that pictures have no difference!</color>\nYou have only <color=white>" + chances + "</color> chances left for this picture!";
    }
}
