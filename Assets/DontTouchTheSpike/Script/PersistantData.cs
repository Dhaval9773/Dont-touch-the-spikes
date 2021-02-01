using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour
{
    public static PersistantData instance { get;set; }
    public bool[] allskin;
    public int myskin;
    private MenuControler mc;

    private void Start()
    {
        mc = FindObjectOfType<MenuControler>();
    }

    public void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SkinstringTodata(string skinin)
    {
        for (int i = 0; i < skinin.Length; i++)
        {
            if (int.Parse(skinin[i].ToString())>0)
            {
                allskin[i] = true;
            }
            else
            {
                allskin[i] = false;
            }
        }
        mc.SetupStore();
    }

    public string SkinDatatoString()
    {
        string tostring = "";
        for (int i = 0; i < allskin.Length; i++)
        {
            if (allskin[i]=true)
            {
                tostring+="1";
            }
            else
            {
                tostring+="0";
            }
        }
        return tostring;
    }
    // Update is called once per frame
    
}
