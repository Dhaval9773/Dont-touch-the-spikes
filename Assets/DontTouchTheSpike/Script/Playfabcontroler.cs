﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Playfabcontroler : MonoBehaviour
{
    private string username;
    /*private string password;
    private string email;*/
    public GameObject loginbutton;
    //public GameObject rawprefab;
    //public Transform rawparent;
    private string myId;
    private PersistantData pd;
    private BirdController bc;
    public int myskin;
    public static int myskindata;
    public static int addcurrency;
    public TMP_Text currency;
    
    
    public static Playfabcontroler instance { get;set; }

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
    
    public void Start()
    {
        
        bc = FindObjectOfType<BirdController>();
        pd = FindObjectOfType<PersistantData>();
        //PlayerPrefs.DeleteAll();
        //loginpanel.SetActive(false);
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
         if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "4857B"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        //PlayerPrefs.DeleteAll();
        // if (PlayerPrefs.HasKey("Email"))
        // {
        //     email=PlayerPrefs.GetString("Email");
        //     password= PlayerPrefs.GetString("Password");
        //     var request = new LoginWithEmailAddressRequest { Email = email, Password = password };
        //     PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        // }
        // else
        // {
#if UNITY_ANDROID
            var requestandroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = Returnmobileid(), CreateAccount = true };
            PlayFabClientAPI.LoginWithAndroidDeviceID(requestandroid,OnAndroidLoginSuccess,OnAndroidLoginFailure);
#endif
        // }
        
    }
    #region LOGIN
    private void OnAndroidLoginSuccess(LoginResult result)
    {
       // Debug.Log("login success");
        if (!PlayerPrefs.HasKey("Username"))
        {
            loginbutton.SetActive(true);
        }
        else
        {
            loginbutton.SetActive(false);
        }

        myId = result.PlayFabId;
        GetplayerData();
        GetMyskinData();
        addvirtualcurrency();
    }

    public void UpdateUsername()
    {
        var requestandroid = new UpdateUserTitleDisplayNameRequest(){DisplayName = username};
        PlayFabClientAPI.UpdateUserTitleDisplayName(requestandroid,
            nameResult =>
            {
               // Debug.Log("username updateed.....");
                PlayerPrefs.SetString("Username",username);
               
            }, 
            error =>
            {
               // Debug.Log(error.GenerateErrorReport());
            });
    }
    private void OnAndroidLoginFailure(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
    /*private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("login success");
        /*PlayerPrefs.SetString("Email",email);#1#
        /*PlayerPrefs.SetString("Password", password);#1#
        Debug.Log("login success");
    }
    private void onRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("on register success");
        /*PlayerPrefs.SetString("Email", email);
        PlayerPrefs.SetString("Password", password);#1#
        Debug.Log("register success");
    }
    private void onRegisterfail(PlayFabError error)
    {
        Debug.Log(error);
    }*/
    /*private void OnLoginFailure(PlayFabError error)
    {
        /*Debug.Log("email"+email);
        Debug.Log("password"+password);#1#
        Debug.Log("usename");
        /*var registerrequest = new RegisterPlayFabUserRequest { Email = email, Password = password, Username=username };
        PlayFabClientAPI.RegisterPlayFabUser(registerrequest,onRegisterSuccess,onRegisterfail);#1#
    }*/
    public void getusername(string usernamein)
    {
        username = usernamein;
    }
    /*public void getemail(string emailin)
    {
        email = emailin;
    }
    public void getpassword(string passwordin)
    {
        password = passwordin;
    }*/
    /*public void onlogin()
    {
        /*var request = new LoginWithEmailAddressRequest { Email=email,Password=password};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);#1#
        Debug.Log("on login");
        /*Debug.Log(request.Email);#1#
    }*/
    public static string Returnmobileid()
    {
        string deviceid = SystemInfo.deviceUniqueIdentifier;
        return deviceid;
    }
    #endregion

    #region STASTICS

    /*public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName="PlayerScore",
                    Value=score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, onleaderboardupdate, onerror);
       
    }

    public void onleaderboardupdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("success");
    }

    public void onerror(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlayerScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request,onLeaderboardget,onerror);
    }

    
    
    public void onLeaderboardget(GetLeaderboardResult result)
    {
        foreach (Transform item in rawparent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rawprefab,rawparent); 
            TMP_Text[] texts;
            texts = newGo.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position+1).ToString();
            texts[1].text = item.DisplayName        ;
            texts[2].text = item.StatValue.ToString();

        }
    }*/
    #endregion

    #region PlayerData

    public void GetplayerData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myId,
            Keys = null
        },OnDatasuccess,
            
            error =>
            {
               // Debug.Log(error.GenerateErrorReport());
            });
    }

    public void OnDatasuccess(GetUserDataResult result)
    {
        if (result.Data==null || !result.Data.ContainsKey("Skins"))
        {
           // Debug.Log("skins not set");
           // Debug.Log(result.Data);
        }
        else
        {
           // Debug.Log(result.Data);
            pd.SkinstringTodata(result.Data["Skins"].Value);
        }
    }

    public void Setuserdata(string skindata)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>()
                {
                    {"Skins",skindata}
                }
            },
            onsuccess =>
        {
            //Debug.Log("data updateed.....");
           
               
        }, 
        error =>
        {
           // Debug.Log(error.GenerateErrorReport());
        });
        
        
    }
    
    public void GetMyskinData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
            {
                PlayFabId = myId,
                Keys = null
            },OnSkinDatasuccess,
            
            error =>
            {
                // Debug.Log(error.GenerateErrorReport());
            });
    }

    public void OnSkinDatasuccess(GetUserDataResult result)
    {
        if (result.Data==null || !result.Data.ContainsKey("MySkin"))
        {
            // Debug.Log("skins not set");
            // Debug.Log(result.Data);
        }
        else
        {
            myskindata=Int32.Parse(result.Data["MySkin"].Value);
            print("Skin data is" + myskindata + "From Playfab controller");
            pd.myskin=Int32.Parse(result.Data["MySkin"].Value);
        }
    }

    public void SetMyskindata(int skindata)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>()
                {
                    {"MySkin",skindata.ToString()}
                }
            },
            onsuccess =>
            {
                //Debug.Log("data updateed.....");
           GetMyskinData();
               
            }, 
            error =>
            {
                // Debug.Log(error.GenerateErrorReport());
            });
        
        
    }
    
    #endregion

    #region virtualcurrency

    public void addvirtualcurrency()
    {
        
        AddUserVirtualCurrencyRequest request = new AddUserVirtualCurrencyRequest();      
        request.VirtualCurrency = "CO"; //put your virtual currency code here
        request.Amount = addcurrency; //put the amount in here
        
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddCurrencyResult, OnPlayFabError);
    }
    void OnAddCurrencyResult(ModifyUserVirtualCurrencyResult result)
    {
        MenuControler.avilablebalance=Int32.Parse(result.Balance.ToString());
        currency.text = result.Balance.ToString();
    }
    void OnPlayFabError(PlayFabError _thisErrorResult)
    {
       
    }
    
    public void substractvirtualcurrency()
    {
        if (MenuControler.avilablebalance<50)
        {
            Debug.Log("-------------------------------not eligible to purchase");
        }
        else
        {
            SubtractUserVirtualCurrencyRequest request = new SubtractUserVirtualCurrencyRequest();
            request.VirtualCurrency = "CO"; //put your virtual currency code here
            request.Amount = 50; //put the amount in here
        
            PlayFabClientAPI.SubtractUserVirtualCurrency(request, _OnAddCurrencyResult, _OnPlayFabError); 
        }
        
    }
    void _OnAddCurrencyResult(ModifyUserVirtualCurrencyResult result)
    {
        MenuControler.avilablebalance=Int32.Parse(result.Balance.ToString());
        currency.text = result.Balance.ToString();
        Debug.Log(result.ToJson());
    }
    void _OnPlayFabError(PlayFabError _thisErrorResult)
    {
       Debug.Log(_thisErrorResult.ErrorMessage);
    }
    

    

    #endregion
}
