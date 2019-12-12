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
    private EventsOvermind<GameEvent> _eventsOvermind = new EventsOvermind<GameEvent>();

    // Update is called once per frame
    void Update()
    {

        changeDecisionName();
        checkIfVideoHasFinished();
    }
    void checkIfVideoHasFinished()
    {
         if (firstVideo.currentState.ToString() == "Initialized")
        {
            hideDecision();
        }
        if (firstVideo.currentState.ToString() == "End")
        { // when the scene is finished
            decision.decision1.gameObject.SetActive(true);
            decision.decision2.gameObject.SetActive(true);
             decisionTimer();

            if(Input.GetKeyUp(KeyCode.Space)){
                makeDecision(decision.decision1.name);
            }
             if(Input.GetKeyUp(KeyCode.Backspace)){
                makeDecision(decision.decision2.name);
            }
            if(OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad)){
                makeDecision(decision.decision1.name);
            }
            if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)){
                makeDecision(decision.decision2.name);
            }
         
         
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

    public void makeDecision(string madedDecision)
    {
        decision.theDecision.TryGetValue(madedDecision, out string test);
        decisionString = test; // decisionString - just a blank string
        print (decisionString);
        hideDecision();
        firstVideo.videoFile = decisionString;
        firstVideo.InitializeVideo();
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
                 StartCoroutine(LoadYourAsyncScene());
                 _eventsOvermind.Send(new ShowUINX_Window() { ID = "TicPlayMenu" });
                break;    
            case "MemoryGame":
                 print("MemoryGame");
                 StartCoroutine(LoadYourAsyncScene());
                 _eventsOvermind.Send(new ShowUINX_Window() { ID = "MemoryPlayMenu" });
                break;
            case "FindTheDifference":
                print("FindTheDifference");
                 StartCoroutine(LoadYourAsyncScene());
                EventsOvermind.Send(new ShowUINX_Window() { ID = "AsteroidPlayMenu" });
                break;
            case "Shooter":
                 print("Shooter");
                 StartCoroutine(LoadYourAsyncScene());
                 _eventsOvermind.Send(new ShowUINX_Window() { ID = "AsteroidPlayMenu" });
                break;
            
            default:
                break;
        }

      
   
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
            decision.decision1.name = "IAmReady";
            decision.decision1.text = "I Am Ready";

            decision.decision2.name = "LetsDoThis";
            decision.decision2.text = "Lets Do This";
        }
         if (firstVideo.videoFile == "TheExamDay2.mp4")
        {
            decision.decision1.name = "MentalChallengeYes";
            decision.decision1.text = "Let's see this mental challenge";

            decision.decision2.name = "MentalChallengeNo";
            decision.decision2.text = "No i need to study?";
        }
        if (firstVideo.videoFile == "TheExamDayPlayNo.mp4" || firstVideo.videoFile == "TheExamDayPlayYes.mp4")
        {
            decision.decision1.name = "RockTheExam";
            decision.decision1.text = "Rock The Exam";

            decision.decision2.name = "SmashTheExam";
            decision.decision2.text = "Smash The Exam";
        }



         if (firstVideo.videoFile == "TheIntro.mp4")
        {
            decision.decision1.name = "StartTheJourney";
            decision.decision1.text = "Start The Journey";

            decision.decision2.name = "LetsGo";
            decision.decision2.text = "Lets Go";
        }
        
        if (firstVideo.videoFile == "AlarmRings.mp4")
        {
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