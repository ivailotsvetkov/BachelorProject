using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Evereal.VRVideoPlayer;
using UnityEngine.SceneManagement;

public class SphereManager : MonoBehaviour
{

    public GameObject theVideoPlayer;
    public VideoPlayerCtrl firstVideo;
    public Decision decision;
    public GameObject bars;
    string decisionString;
    private float timerSpeed = 10f;
    private float elapsed;
    public UINX_DecisionMenu DecisionMenu;
    // Update is called once per frame
    bool videoFinishedOneTime = false;
    bool playingGame = false;

    void Update()
    {
        if (playingGame)
        {

        } else {
            changeDecisionName();
            checkIfVideoHasFinished();
        }
    }
    void checkIfVideoHasFinished()
    {
         if (firstVideo.currentState.ToString() == "Initialized")
        {
            hideDecision();
            Overmind.PlayerStation.SetUINXMode(false);
            Overmind.EventsManager.Send(new HideUINX_Window(){ HideAll = true});
            videoFinishedOneTime = false;
        }
        if (firstVideo.currentState.ToString() == "End")
        { // when the scene is finished
            print("ENDDD");
            if (!videoFinishedOneTime)
            {
                Overmind.PlayerStation.SetUINXMode(true);
                Overmind.EventsManager.Send(new ShowUINX_Window() { ID = "DecisionMenu" });
                videoFinishedOneTime = true;
            }
            decision.decision1.gameObject.SetActive(true);
            decision.decision2.gameObject.SetActive(true);
            decisionTimer(); 
        }  
    }
    
    void decisionTimer(){
        elapsed += Time.deltaTime;
        if(elapsed >= timerSpeed){
            elapsed = 0f;
            int r = Random.Range(0, 1);
             if (r == 0){
                 makeDecision(decision.decision2.name);
             }
            else if (r == 1){
                makeDecision(decision.decision1.name);
            }
        }
    }
    void hideDecision(){
         decision.decision1.gameObject.SetActive(false);
         decision.decision2.gameObject.SetActive(false);
    }
    public void makeDecisionFromMenu(int number)
    {
        if (number == 1)
        {
            makeDecision(decision.decision1.name);
        } else {
            makeDecision(decision.decision2.name);
        }
    }
    public void makeDecision(string madedDecision)
    {
        videoFinishedOneTime = false;

        print(madedDecision);
        decision.theDecision.TryGetValue(madedDecision, out string test);
        decisionString = test; // decisionString - just a blank string
        hideDecision();
        if (decisionString != null)
        {

            print (decisionString);
            if (decisionString.Contains(".mp4"))
            {
                firstVideo.videoFile = decisionString;
                firstVideo.InitializeVideo();
            }
            switch(decisionString)
            {
                
                case "Mars.mp4":
                    Score.instance.healthBar.fillAmount -= 0.1f;
                    Score.instance.energyBar.fillAmount += 0.2f;
                    break;
                case "Orange.mp4":
                    Score.instance.healthBar.fillAmount += 0.2f;
                    Score.instance.energyBar.fillAmount += 0.1f;
                    break;
                case "WakeUpNo.mp4":
                    Score.instance.energyBar.fillAmount += 0.35f;
                    break;   
                case "AlarmRings.mp4":
                    bars.SetActive(true);
                    break;
                case "TicTacToe":
                    print("TicTacToe");
                    //StartCoroutine(LoadYourAsyncScene());
                    Overmind.EventsManager.Send(new ShowUINX_Window() { ID = "TicPlayMenu" });
                    Overmind.PlayerStation.SwitchPositionToGame();
                    theVideoPlayer.gameObject.SetActive(false);
                    playingGame = true;
                    break;    
                case "MemoryGame":
                    print("MemoryGame");
                    //StartCoroutine(LoadYourAsyncScene());
                    Overmind.EventsManager.Send(new ShowUINX_Window() { ID = "MemoryPlayMenu" });
                        Overmind.PlayerStation.SwitchPositionToGame();
                    //firstVideo.GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
                    firstVideo.targetSurface.GetComponent<Renderer>().enabled = false;
                    playingGame = true;
                    break;
                case "FindTheDifference":
                    print("FindTheDifference");
                    //StartCoroutine(LoadYourAsyncScene());
                    Overmind.EventsManager.Send(new ShowUINX_Window() { ID = "AsteroidPlayMenu" });
                    Overmind.PlayerStation.SwitchPositionToGame();
                    theVideoPlayer.gameObject.SetActive(false);
                    playingGame = true;
                    break;
                case "Shooter":
                    print("Shooter");
                    //StartCoroutine(LoadYourAsyncScene());
                    Overmind.EventsManager.Send(new ShowUINX_Window() { ID = "AsteroidPlayMenu" });
                    Overmind.PlayerStation.SwitchPositionToGame();
                    theVideoPlayer.gameObject.SetActive(false);
                    playingGame = true;
                    break;
                
                default:
                    break;
            }
        }

      
   
    }
    public void PlayVideo(string s)
    {
        firstVideo.videoFile = s;
        firstVideo.InitializeVideo();
    }
   IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Games");
        
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
    }
    // need to put in Decisiions
    void changeDecisionName()
    {

        if (firstVideo.videoFile == "TheExamDay.mp4")
        {
            DecisionMenu.SetButton1Text("I Am Ready");
            DecisionMenu.SetButton2Text("Lets Do This");//now you can set text but i must also add action for buttons

            decision.decision1.name = "IAmReady";
            decision.decision1.text = "I Am Ready";

            decision.decision2.name = "LetsDoThis";
            decision.decision2.text = "Lets Do This";
        }
         if (firstVideo.videoFile == "TheExamDay2.mp4")
        {
            DecisionMenu.SetButton1Text("Let's see this mental challenge");
            DecisionMenu.SetButton2Text("No i need to study?");
            decision.decision1.name = "MentalChallengeYes";
            decision.decision1.text = "";

            decision.decision2.name = "MentalChallengeNo";
            decision.decision2.text = "No i need to study?";
        }
        if (firstVideo.videoFile == "TheExamDayPlayNo.mp4" || firstVideo.videoFile == "TheExamDayPlayYes.mp4")
        {
            DecisionMenu.SetButton1Text("Rock The Exam");
            DecisionMenu.SetButton2Text("Smash The Exam");
            decision.decision1.name = "RockTheExam";
            decision.decision1.text = "Rock The Exam";

            decision.decision2.name = "SmashTheExam";
            decision.decision2.text = "Smash The Exam";
        }



         if (firstVideo.videoFile == "TheIntro.mp4")
        {
            DecisionMenu.SetButton1Text("Start The Journey");
            DecisionMenu.SetButton2Text("Lets Go");

            decision.decision1.name = "StartTheJourney";
            decision.decision1.text = "Start The Journey";

            decision.decision2.name = "LetsGo";
            decision.decision2.text = "Lets Go";
        }
        
        if (firstVideo.videoFile == "AlarmRings.mp4")
        {
            DecisionMenu.SetButton1Text("Start The Journey");
            DecisionMenu.SetButton2Text("Lets Go");//you can take care of it is easy just copy paste this strings
            decision.decision1.name = "WakeUp";
            decision.decision1.text = "Wake Up";

            decision.decision2.name = "10moreminutes";
            decision.decision2.text = "'10 More Minutes'";
        }
        if (firstVideo.videoFile == "WakeUpYes.mp4")
        {
            decision.decision1.name = "Orange";
            decision.decision1.text = "Orange";

            decision.decision2.name = "Mars";
            decision.decision2.text = "Mars";
        }
        if (firstVideo.videoFile == "Mars.mp4" || firstVideo.videoFile == "Orange.mp4")
        {
            decision.decision1.name = "Good";
            decision.decision1.text = "Good";

            decision.decision2.name = "SoSo";
            decision.decision2.text = "So so";
        }
        if (firstVideo.videoFile == "DidYouSeeTheEmail.mp4")
        {
            decision.decision1.name = "No";
            decision.decision1.text = "No";

            decision.decision2.name = "WhatEmail";
            decision.decision2.text = "What Email?";
        }
        if (firstVideo.videoFile == "WannaPlayTicTacToe.mp4")
        {
            decision.decision1.name = "YeahSure";
            decision.decision1.text = "Yeah Sure";

            decision.decision2.name = "NoLetsStudy";
            decision.decision2.text = "No, Lets Study";
        }
        
        if (firstVideo.videoFile == "WakeUpNo.mp4")
        {
            decision.decision1.name = "Sleeping";
            decision.decision1.text = "I Was Sleeping";

            decision.decision2.name = "Studying";
            decision.decision2.text = "I was studying (lie)";
        }
        
    }
     




}