using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Evereal.VRVideoPlayer;


public class SphereOne : MonoBehaviour
{
    public enum VideoName{

    }

    public VideoPlayerCtrl firstVideo;
    public Decisions decision;
 
    

    void Start()
    { 
     
    }
 
    // Update is called once per frame
    void Update()
    {  
     checkIfVideoHasFinished(firstVideo);
       changeDecisionName();
    }
    void checkIfVideoHasFinished(VideoPlayerCtrl video){
        if(video.currentState.ToString() == "End"){
            decision.decision1.gameObject.SetActive(true);
            decision.decision2.gameObject.SetActive(true);   
            changeVideoName(firstVideo,"WakeUpYes.mp4");
            firstVideo.InitializeVideo();
      }
    } 
    
   public void changeVideoName(VideoPlayerCtrl video,string name)
        {
            video.videoFile = name;
            video.InitializeVideo();
        }
        
         void changeDecisionName(){
          //   print(firstVideo.videoFile);
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
        
   
   public void nowPlaying(){
        print(firstVideo.videoFile); 
    }
    IEnumerator hideDecision(int i){
        
        yield return new WaitForSeconds(i);
         
         decision.decision1.gameObject.SetActive(false);
    decision.decision2.gameObject.SetActive(false);   
    }
    
   

            
  
    
   
}