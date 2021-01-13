using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManu : MonoBehaviour
{
    
    public AudioSource audioplay;

    public GameObject playbutton;
    // Start is called before the first frame update
    private void Start()
    {
        playbutton.GetComponent<Button>().enabled = true;
    }

    public void play()
    {
        StartCoroutine(coroutine());
        playbutton.GetComponent<Button>().enabled = false;
        Debug.Log("play");
        
    }

    public void quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
    IEnumerator coroutine()
    {
        audioplay.Play();
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(1);
    }
}
