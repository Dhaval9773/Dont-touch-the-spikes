using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizing : MonoBehaviour
{
    public GameObject background;
    public GameObject spikes;
    void Start()
    {    
        background.transform.localScale =
            new Vector3(10 * (background.transform.localScale.x) * ((float) Camera.main.aspect / 5f),
                background.transform.localScale.y);
        
    }
    
    void Update()
    {
    }
}
