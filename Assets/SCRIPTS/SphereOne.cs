using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Evereal.VRVideoPlayer;


public class SphereOne : MonoBehaviour
{

    public VideoPlayerCtrl firstVideo;
    public Decision decision;
    string decisionString;
  

    void Start()
    { 
         
    }
 
    // Update is called once per frame
    void Update()
    {  
       
         changeDecisionName();
         checkIfVideoHasFinished();
    }
    void checkIfVideoHasFinished(){
        if(firstVideo.currentState.ToString() == "End"){ // when the scene is finished
            decision.decision1.gameObject.SetActive(true); 
            decision.decision2.gameObject.SetActive(true);  
            if(Input.GetKeyDown("left")){
                makeDecision(decision.decision1.name); // get the name of the decision
            }
            if(Input.GetKeyDown("right")){
                 makeDecision(decision.decision2.name);
    }
          
      }
       if(firstVideo.currentState.ToString() == "Initialized"){ 
             decision.decision1.gameObject.SetActive(false); 
            decision.decision2.gameObject.SetActive(false); 
           }
    } 
    public void makeDecision(string madedDecision){
               
                decision.theDecision.TryGetValue(madedDecision,out string test);
                decisionString = test;
                 firstVideo.videoFile = decisionString;
               firstVideo.InitializeVideo();
    }
   public void changeVideoName(string name)
        {
            firstVideo.videoFile = name; 
            
        }
        
        
  // need to put in Decisiions
   void changeDecisionName(){
      if(firstVideo.videoFile == "AlarmRings.mp4"){
        decision.decision1.name = "WakeUp";
        decision.decision1.text = "Wake Up";

        decision.decision2.name = "10moreminutes";
        decision.decision2.text = "'10 More Minutes'";
      }
      if(firstVideo.videoFile == "WakeUpYes.mp4"){
        decision.decision1.name = "Orange";
        decision.decision1.text = "Orange"; 

        decision.decision2.name = "Mars";
        decision.decision2.text = "Mars";
      }
        if(firstVideo.videoFile == "VerryFirstScene.mp4"){
        decision.decision1.name = "WakeUp";
        decision.decision1.text = "Wake Up";

        decision.decision2.name = "10moreminutes";
        decision.decision2.text = "'10 More Minutes'";
      }
      if(firstVideo.videoFile == "VerryFirstSceneSLEEP.mp4"){
        decision.decision1.name = "Sn";
        decision.decision1.text = "Mars"; 
        decision.decision2.name = "10moreminutes";
        decision.decision2.text = "'10 More Minutes'";
      }
    }
    
 
    IEnumerator hideDecision(int i){
        
        yield return new WaitForSeconds(i);
         
    decision.decision1.gameObject.SetActive(false);
    decision.decision2.gameObject.SetActive(false);   
    }
    
   

            
  
    
   
}