using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColourChange : MonoBehaviour
{

    public List<Color> background;
    public List<Color> spikes;

    public GameObject back;
    public GameObject upspike;
    public GameObject bottomspike;
    public GameObject rightspike;
    public GameObject leftspike;
    public TMP_Text eggscore;
    //public TMP_Text score;
    private int limit = 10;

    private int i = 0;

    private RandomSpikeGenerate rpg;
    // Start is called before the first frame update
    void Start()
    {
        rpg = FindObjectOfType<RandomSpikeGenerate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rpg.score>limit)
        {
            colour();
            limit += 10;
        }
        if (i>8)
        {
            i = 0;
        }
    }

    public void colour()
    {
        Debug.Log("colour"+i);
        back.GetComponent<SpriteRenderer>().color = background[i];
        upspike.GetComponent<SpriteRenderer>().color = spikes[i];
        bottomspike.GetComponent<SpriteRenderer>().color = spikes[i];
        foreach (Transform obj in upspike.transform)
        {
            obj.GetComponent<SpriteRenderer>().color=spikes[i];
        }
        foreach (Transform obj in bottomspike.transform)
        {
            obj.GetComponent<SpriteRenderer>().color=spikes[i];
        }
        foreach (Transform obj in rightspike.transform)
        {
            obj.GetComponent<SpriteRenderer>().color=spikes[i];
        }
        foreach (Transform obj in leftspike.transform)
        {
            obj.GetComponent<SpriteRenderer>().color=spikes[i];
        }
        eggscore.color = spikes[i];
        //score.color = background[i];
        i++;
    }
}
