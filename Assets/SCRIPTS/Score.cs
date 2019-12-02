using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int maxInt = 100;
    
    public Image intBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rightPath();
    }   
    void rightPath(){
        Player.singleton.inteligence += 10;
        intBar.fillAmount = (float)Player.singleton.inteligence / (float)maxInt;
    }
}
