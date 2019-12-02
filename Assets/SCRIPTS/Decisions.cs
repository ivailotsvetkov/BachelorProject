using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Decisions : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public Text decision1;
    public Text decision2;
    public SphereOne theVideo;
    

    // Start is called before the first frame update
    void Start()
    {
      theVideo = gameObject.GetComponent<SphereOne>();
     
    }

    // Update is called once per frame
    void Update()
    {
     // print(theVideo.firstVideo.name);
        
      ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if(Physics.Raycast(ray,out hit)){
          if(hit.transform.name == "Good"){
             
          }
      }
    }
     /*  void changeDecisionName(){
      if(theVideo.firstVideo.name == "VerryFirstScene.mp4"){
        decision1.name = "WakeUp";
        decision1.text = "Wake Up"; 
        decision2.name = "10moreminutes";
        decision2.text = "'10 More Minutes'";
      }
      if(theVideo.firstVideo.name == "VerryFirstSceneSLEEP.mp4"){
        decision1.name = "Sn";
        decision1.text = "Mars"; 
        decision2.name = "10moreminutes";
        decision2.text = "'10 More Minutes'";
      }
    }
    */
 
}
