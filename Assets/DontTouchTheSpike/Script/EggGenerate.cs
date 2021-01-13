using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;
using TMPro;

public class EggGenerate : MonoBehaviour
{
    public List<GameObject> eggs;
    public GameObject randomegg;
    private int random;
    private RandomSpikeGenerate rpg;
    private bool side;
    private float speed=.8f;
    private Vector3 oldPos;
    private bool goUp;
    public GameObject eggscore;
    //public Camera cam;
    //public Vector3 eggscorepos;
    
    void Start()
    {
        rpg = FindObjectOfType<RandomSpikeGenerate>(); 
        Invoke("spawnegg",1);
        
    }

    private void Update()
    {
        eggscore.transform.position = Camera.main.WorldToScreenPoint(randomegg.transform.position);
        /*Vector3 eggscorepos = cam.WorldToScreenPoint(eggscore.transform.position);
        eggscorepos = randomegg.transform.position;*/
        //Vector3 target = cam.WorldToScreenPoint(randomegg.transform.position);
        //target = eggscore.transform.position;
        
        if (goUp)
        {
            if (randomegg.transform.position.y > oldPos.y + .5f)
            {
                goUp = false;
            }
            else
            {
                randomegg.transform.position += Vector3.up * speed * Time.deltaTime;
            }
        }
        else
        {
            if (randomegg.transform.position.y < oldPos.y)
            {
                goUp = true;
            }
            else
            {
                randomegg.transform.position -= Vector3.up * speed * Time.deltaTime;
            }
        }
    }

    public void spawnegg()
    {
        if (!side)
        {
           // Debug.Log("left spawn egg");
            random = Random.Range(0, 5);
            randomegg =Instantiate(eggs[random], new Vector2(-1.4f,Random.Range(2.8f,-2.5f)), Quaternion.identity) as GameObject;
            side = true;
            eggscore.SetActive(false);
        }
        else
        {
           // Debug.Log("right spawn egg");
            random = Random.Range(0, 5);
            randomegg = Instantiate(eggs[random], new Vector2(1.4f,Random.Range(2.8f,-2.5f)), Quaternion.identity) as GameObject;
            side = false;
            eggscore.SetActive(false);
        }
        oldPos = randomegg.transform.position;
    }
}
