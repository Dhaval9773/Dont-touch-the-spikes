using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab.MultiplayerModels;
using UnityEngine;
using UnityEngine.UI;

public class MenuControler : MonoBehaviour
{
    // Start is called before the first frame update
    public static MenuControler instance { get;set; }
    public GameObject[] buttonlocks;
    public Button[] unlockedbuttons;
    private PersistantData pd;
    private Playfabcontroler pc;
    public static int avilablebalance;

    public void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        // else
        // {
        //     Destroy(this.gameObject);
        // }
    }
    void Start()
    {
        pd = FindObjectOfType<PersistantData>();
        pc = FindObjectOfType<Playfabcontroler>();
        SetupStore();
    }

    public void SetupStore()
    {
        for (int i = 0; i < pd.allskin.Length; i++)
        {
            buttonlocks[i].SetActive(!pd.allskin[i]);
            unlockedbuttons[i].interactable = pd.allskin[i];
        }
    }

    public void unlockskin(int index)
    {
        
        if (avilablebalance<50)
        {
            Debug.Log("-------------------------------not eligible to purchase");
        }
        else
        {
            pd.allskin[index] = true;
            Debug.Log(index+"index");
            pc.Setuserdata(pd.SkinDatatoString());
            SetupStore();
        }
        
    }

    public void setmyskin(int whichskin)
    {
        pd.myskin = whichskin;
    }
}
