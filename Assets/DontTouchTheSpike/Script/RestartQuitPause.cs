using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class RestartQuitPause : MonoBehaviour
{
    public GameObject restartpanel;
    //public GameObject gameoverpanel;
    //public GameObject player;
    public AudioSource onclick;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void pause()
    {
        onclick.Play();
        Time.timeScale = 0;
        restartpanel.SetActive(true);
    }

    public void resume()
    {
        onclick.Play();
        Time.timeScale = 1;
        restartpanel.SetActive(false);
    }

    public void mainmanu()
    {
        onclick.Play();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    
    public void Restart()
    {
        onclick.Play();
        //all panel disable
        //time scale = 1 optional
        //load scene 
       SceneManager.LoadScene(1);
    }
}
