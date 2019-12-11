using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Decision : MonoBehaviour
{
    public Text decision1;
    public Text decision2;
    
   
    public Dictionary<string,string> theDecision = new Dictionary<string,string>();
    

    // Start is called before the first frame update
    void Start()
    {
      addDecisions();
    }

    // Update is called once per frame
    void Update()
    {
      
      
    }
    void addDecisions(){
      theDecision.Add("WakeUp","WakeUpYes.mp4");
      theDecision.Add("10moreminutes","WakeUpNo.mp4");

       theDecision.Add("Mars","Mars.mp4");
      theDecision.Add("Orange","Orange.mp4");
      
      theDecision.Add("Good","DidYouSeeTheEmail.mp4");
      theDecision.Add("SoSo","DidYouSeeTheEmail.mp4");

       theDecision.Add("No","WannaPlayTicTacToe.mp4");
      theDecision.Add("WhatEmail","WannaPlayTicTacToe.mp4");

      theDecision.Add("YeahSure","TicTacToe");
      theDecision.Add("NoLetsStudy","LetsGoInside.mp4");

       theDecision.Add("StartTheJourney","AlarmRings.mp4");
      theDecision.Add("LetsGo","AlarmRings.mp4");

       theDecision.Add("Sleeping","LetsPlayTicTacToe.mp4");
      theDecision.Add("Studying","LetsPlayTicTacToe.mp4");

      theDecision.Add("IAmReady","TheExamDay2.mp4");
      theDecision.Add("LetsDoThis","TheExamDay2.mp4");
    }
      
}