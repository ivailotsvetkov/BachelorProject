using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINX_Tutorial : UINX_Window
{
    public TextMeshProUGUI TutorialTxt;
    public TextMeshProUGUI TimeTxt;
    public TextMeshProUGUI Asteroidogeon;

    float tutorialTS = 0;
    [Multiline]
    public string Tutorial_String = "";
    int stringPointer = 0;
    float letterTS = 0;
    public float Letter_Time = 0.1f;
    bool writingString = true;
    bool ended = false;
    float showTS = 0;
    public float Show_Time = 2f;

    public Color tutorialTxtColor;
    public Color tutorialTxtColorHide;
    public Color asteroColor;

    public Color timeTxtColor;
    public Color timeTxtColorHide;
    Color asteroColorHide;
    int hidding = -1;

    private void Awake()
    {
        tutorialTxtColorHide = new Color(TutorialTxt.color.r, TutorialTxt.color.g, TutorialTxt.color.b, 0);
        timeTxtColorHide = new Color(TimeTxt.color.r, TimeTxt.color.g, TimeTxt.color.b, 0);
        asteroColorHide = new Color(Asteroidogeon.color.r, Asteroidogeon.color.g, Asteroidogeon.color.b, 0);
    }

    protected override void InitClickButtons()
    {
        Delegates = new List<UINX_Button.OnUINX_ButtonClickDelegate>()
        {            
            OnSkipButton
        };
    }

    protected virtual void OnSkipButton(UINX_Button button)
    {
        button.Hide();
        Overmind.AsteroidGameOvermind.Sounds[0].Play();
        ended = true;
        Overmind.PlayerStation.SetUINXMode(false);
        Asteroidogeon.gameObject.SetActive(true);
    }

    public override void Show(AncestorBehaviourInitData initData = null)
    {
        tutorialTS = 0;
        letterTS = 0;
        stringPointer = 0;
        showTS = 0;
        writingString = true;
        hidding = -1;
        TimeTxt.gameObject.SetActive(false);
        Asteroidogeon.gameObject.SetActive(false);
        TutorialTxt.color = tutorialTxtColor;
        TimeTxt.color = timeTxtColor;


        TutorialTxt.text = "";
        ended = false;
        base.Show(initData);
    }

    public override void Ancestor_Update()
    {
        base.Ancestor_Update();
        if (!ended)
        {
            if (writingString)
            {
                letterTS += Time.deltaTime;
                if (letterTS >= Letter_Time)
                {
                    letterTS = 0;
                    TutorialTxt.text = Tutorial_String.Substring(0, stringPointer);
                    if (stringPointer >= Tutorial_String.Length)
                    {
                        writingString = false;
                        TimeTxt.gameObject.SetActive(true);
                        TimeTxt.text = "";
                    }
                    ++stringPointer;
                }
            } else
            {
                if (tutorialTS < Overmind.AsteroidGameOvermind.Tutorial_Time)
                {
                    tutorialTS += Time.deltaTime;
                    TimeTxt.text = "Time to start:\n" + (Mathf.Floor((Overmind.AsteroidGameOvermind.Tutorial_Time - tutorialTS)));
                } else if (tutorialTS > Overmind.AsteroidGameOvermind.Tutorial_Time)
                {
                    tutorialTS = Overmind.AsteroidGameOvermind.Tutorial_Time;
                    TimeTxt.text = "TIME TO START!";
                    ended = true;
                    Overmind.PlayerStation.SetUINXMode(false);
                    Asteroidogeon.gameObject.SetActive(true);
                    Buttons[0].Hide();
                }
            }
        } else
        {
            showTS += Time.deltaTime;
            if (hidding == -1)
            {
                TutorialTxt.color = Color.Lerp(tutorialTxtColor, tutorialTxtColorHide, showTS / Show_Time);
                TimeTxt.color = Color.Lerp(timeTxtColor, timeTxtColorHide, showTS / Show_Time);
                Asteroidogeon.color = Color.Lerp(asteroColorHide, asteroColor, showTS / Show_Time);
                if (showTS >= Show_Time)
                {
                    hidding = 0;
                    showTS = 0;
                }
            } else if (hidding == 0)
            {
                Asteroidogeon.color = Color.Lerp(asteroColor, asteroColorHide, showTS / Show_Time);
                if (showTS >= Show_Time)
                {
                    hidding = 1;
                    Overmind.AsteroidGameOvermind.NewGame();
                    Hide();
                }
            }
        }
    }

    public override void Hide()
    {
        base.Hide();
    }
}
