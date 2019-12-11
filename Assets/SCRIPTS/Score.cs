using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Image intBar;
    public Image healthBar;
    public Image energyBar;
 
    public static Score instance;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else Destroy(this);
    }
 
    
}
