      using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BirdController : MonoBehaviour
{
    public GameObject bird;
    private Rigidbody2D rb;
    private bool direction;
    private RandomSpikeGenerate rpg;
    private bool gameover=true;
    public GameObject gameoverpanel;
    public GameObject ps;
    private Enumrator myenum;
    private EggGenerate eg;
    public AudioSource rightside;
    public AudioSource leftside;
    public AudioSource gameoversound;
    public AudioSource eggsound;
    public AudioSource birdjump;
    private float speed = 3.5f;
    private ColourChange cc;
    private int limit = 10;
    public Sprite[] skinset;
    private Playfabcontroler pc;
    public ParticleSystem pts;
    public Gradient[] gradient;
    
    
    void Start()
    {
        ps.GetComponent<ParticleSystem>().Stop();
        // rpg = GameObject.Find("BackGround").GetComponent<RandomSpikeGenerate>();
    }

    private void OnEnable()
    {
        bird.transform.position=new Vector3(0.03f,0.33f,-6.900815f);
        rb = GetComponent<Rigidbody2D>();
        rpg = FindObjectOfType<RandomSpikeGenerate>();
        pc= FindObjectOfType<Playfabcontroler>();
        eg = FindObjectOfType<EggGenerate>();
        direction = false;
        bird.GetComponent<SpriteRenderer>().color=new Color(255,255,255,255);
        gameover = true;
        myenum = FindObjectOfType<Enumrator>();
        cc = FindObjectOfType<ColourChange>();
        Debug.Log("-----------------"+Playfabcontroler.myskindata);
        bird.GetComponent<SpriteRenderer>().sprite = skinset[Playfabcontroler.myskindata];
        var col = pts.colorOverLifetime;
        col.color = gradient[Playfabcontroler.myskindata];
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && gameover)
        {
            birdjump.Play();
            ps.GetComponent<ParticleSystem>().Play();
            
            if (!direction)
            {
                rb.velocity = new Vector2(speed, speed);
            }
            else
            {
                rb.velocity = new Vector2(-speed, speed);
            }
        }
        eg.eggscore.transform.localPosition+= Vector3.up;
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Enumrator>().identifier==Myenum.rightside && gameover)
        {
            //Debug.Log("right");
            direction = true;
            bird.transform.eulerAngles = new Vector3(0, 180, 0);
            rpg.LeftSpike();
            rightside.Play();
            if (rpg.score>limit)
            {
                //speed += 0.1f;
                Debug.Log("speed "+speed);
                //cc.colour();
                limit += 10;
            }
        }
        
        if (other.gameObject.GetComponent<Enumrator>().identifier==Myenum.leftside && gameover)
        {
            //Debug.Log("left");
            direction = false;
            bird.transform.eulerAngles=new Vector3(0,0,0);
            rpg.RightSpike();
            leftside.Play();
            if (rpg.score>limit)
            {
                //speed += 0.1f;
                Debug.Log("speed "+speed);
                //cc.colour();
                limit += 10;
            }
        }
        
        if (other.gameObject.GetComponent<Enumrator>().identifier==Myenum.spike && gameover)
        {     
           // Debug.Log("spike");
            gameoversound.Play();
            ps.GetComponent<ParticleSystem>().Stop();
            bird.GetComponent<SpriteRenderer>().color=Color.black;
            bird.transform.GetChild(0).gameObject.SetActive(true);
            rb.freezeRotation = false;
            rb.velocity=new Vector2(5,5);
            rb.rotation += 2;
            gameover = false;
            Playfabcontroler.addcurrency=rpg.score;
            //pc.SendLeaderboard(rpg.score);
            Invoke("gameisover",1.5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Enumrator>().identifier==Myenum.egg && gameover)
        {
           // Debug.Log("trigger");
            eggsound.Play();
            Destroy(eg.randomegg);
            eg.eggscore.SetActive(true); 
            eg.Invoke("spawnegg",0.5f);
            rpg.score += 5;
        }
    }

    public void gameisover()
    {
        bird.SetActive(false);
        gameoverpanel.SetActive(true);
        //Playfabcontroler.instance.SendLeaderboard(rpg.score);
    }
}
