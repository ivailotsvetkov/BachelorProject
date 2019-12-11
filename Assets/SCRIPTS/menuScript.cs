using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuScript : MonoBehaviour
{
    SphereManager video;
    public GameObject theMenu;
    // Update is called once per frame
    public void PlayGame(){
        theMenu.SetActive(false);
        video.theVideoPlayer.SetActive(true);
    }
}
