using System.Collections;
using System.Collections.Generic;
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
        Debug.Log(index+"index");
        pd.allskin[index] = true;
        pc.Setuserdata(pd.SkinDatatoString());
        SetupStore();
    }

    public void setmyskin(int whichskin)
    {
        pd.myskin = whichskin;
    }
}
