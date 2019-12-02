using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Evereal.VRVideoPlayer;

public class SceneChanger : MonoBehaviour
{

    public VideoPlayerCtrl videoPlaying = new VideoPlayerCtrl();
    public VideoPlayerCtrl nextVideo = new VideoPlayerCtrl();
    public GameObject decision1;
    public GameObject decision2;

     public double time;
    public double currentTime;
    Ray ray;
     RaycastHit hit;



    void Start()
    {
      
       
     
    }

    // Update is called once per frame
    void Update()
    {
     
    
            
    }
    
    
     IEnumerator freezeScene(VideoPlayerCtrl video,float i,String name1, String name2){
        yield return new WaitForSeconds(i);
        video.Pause();
        decision1.SetActive(true);
        decision2.SetActive(true);
        decision1.name = name1;
        decision2.name = name2;
        
    }
    IEnumerator makeDecision(VideoPlayerCtrl video,float i){
         print("Before");
        
        yield return new WaitForSeconds(i);
         print("After");
        video.Pause();
    }
 
  

}
