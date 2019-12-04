using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Decision : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public Text decision1;
    public Text decision2;
    SphereOne theVideo;
    public string dictionaryValue;
    public Dictionary<string,string> theDecision = new Dictionary<string,string>();


    // Start is called before the first frame update
    void Start()
    {
      theDecision.Add("WakeUp","WakeUpYes.mp4");
      theDecision.Add("10moreminutes","WakeUpNo.mp4");

       theDecision.Add("Mars","Mars.mp4");
      theDecision.Add("Orange","Orange.mp4");
    
      theDecision.TryGetValue("WakeUp", out string test);
      dictionaryValue = test;
      
    
    }

    // Update is called once per frame
    void Update()
    {
      
      
    }
      
}