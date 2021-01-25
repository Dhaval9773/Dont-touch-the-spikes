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
    public void Start()
    {
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "4857B"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        PlayerPrefs.SetString("Email",email);
        PlayerPrefs.SetString("Password", password);
    }
    private void onRegisterSuccess(RegisterPlayFabUserRequest result)
    {
        PlayerPrefs.SetString("Email", email);
        PlayerPrefs.SetString("Password", password);
    }
    private void onRegisterfail(PlayFabError error)
    {

    }
    private void OnLoginFailure(PlayFabError error)
    {
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
    }
}
