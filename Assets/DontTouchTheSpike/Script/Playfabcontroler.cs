using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;


public class Playfabcontroler : MonoBehaviour
{
    private string username;
    private string password;
    private string email;
    public GameObject loginbutton;

    public void Start()
    {
        //loginpanel.SetActive(false);
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "4857B"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("Email"))
        {
            email=PlayerPrefs.GetString("Email");
            password= PlayerPrefs.GetString("Password");
            var request = new LoginWithEmailAddressRequest { Email = email, Password = password };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }
        else
        {
#if UNITY_ANDROID
            var requestandroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = Returnmobileid(), CreateAccount = true };
            PlayFabClientAPI.LoginWithAndroidDeviceID(requestandroid,OnAndroidLoginSuccess,OnAndroidLoginFailure);
#endif
        }
        
    }
    #region LOGIN
    private void OnAndroidLoginSuccess(LoginResult result)
    {
        Debug.Log("login success");
       
        Debug.Log("login success");
    }
    private void OnAndroidLoginFailure(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("login success");
        PlayerPrefs.SetString("Email",email);
        PlayerPrefs.SetString("Password", password);
        loginbutton.SetActive(false);
        Debug.Log("login success");
    }
    private void onRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("on register success");
        PlayerPrefs.SetString("Email", email);
        PlayerPrefs.SetString("Password", password);
        loginbutton.SetActive(false);
        Debug.Log("register success");
    }
    private void onRegisterfail(PlayFabError error)
    {
        Debug.Log(error);
    }
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("email"+email);
        Debug.Log("password"+password);
        Debug.Log("usename");
        var registerrequest = new RegisterPlayFabUserRequest { Email = email, Password = password, Username=username };
        PlayFabClientAPI.RegisterPlayFabUser(registerrequest,onRegisterSuccess,onRegisterfail);
    }
    public void getusername(string usernamein)
    {
        username = usernamein;
    }
    public void getemail(string emailin)
    {
        email = emailin;
    }
    public void getpassword(string passwordin)
    {
        password = passwordin;
    }
    public void onlogin()
    {
        var request = new LoginWithEmailAddressRequest { Email=email,Password=password};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        Debug.Log("on login");
        Debug.Log(request.Email);
    }
    public static string Returnmobileid()
    {
        string deviceid = SystemInfo.deviceUniqueIdentifier;
        return deviceid;
    }
    #endregion

    #region STASTICS

    public void SendLeaderboard(int score)
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
    #endregion
}
