using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
       [Header("=CAMERA=")]
    public Transform CameraRig;
    public Camera MainCamera;
    public OVRScreenFade ScreenFade;

    public Transform HandBoneParent;

    [Header("=UINX=")]

     public static Player singleton;
     public GameObject theCamera;
    public int inteligence;
    int mood;
    int energy;
    int social;
 
 void Awake()
{
    if(singleton != null)
        Destroy(this.gameObject);
    else
        singleton = this;
}
   
    void Start()
    {
       //inteligence = this;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
 

}
