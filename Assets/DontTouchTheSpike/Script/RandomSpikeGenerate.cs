using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class RandomSpikeGenerate : MonoBehaviour
{
    public GameObject rightspikes;
    public GameObject leftspikes;
    public TMP_Text gamescore;
    public int score = 0;
    private int i=0;
    private int random;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gamescore.text = "Score: " + score.ToString();
        if (((score/15)+2)>9)
        {
            i = 9;
        }
    }

    public void RightSpike()
    {
        score++;
        while (i<((score/15)+2))
        {
            random = Random.Range(0, 10); 
            rightspikes.transform.GetChild(random).localPosition = new Vector3(rightspikes.transform.GetChild(random).localPosition.x,0.408f,0);
            i++;
        }
        i = 0;
        foreach (Transform obj in leftspikes.transform)
        {
            obj.localPosition = new Vector3(obj.transform.localPosition.x,0.52f,0);
        }
    }
    public void LeftSpike()
    {
        score++;
        while (i<((score/15)+2))
        {
            random = Random.Range(0, 10); 
            leftspikes.transform.GetChild(random).localPosition = new Vector3(leftspikes.transform.GetChild(random).localPosition.x,0.43f,0);
            i++;
        }
        i = 0;
        foreach (Transform obj in rightspikes.transform)
        {
            obj.localPosition = new Vector3(obj.transform.localPosition.x,0.313f,0);
        }
    }
}
