using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class SignInScreen : MonoBehaviour
{   

    //This code appears to be a method for registering a new user in a game. The method retrieves the user's username and password from a canvas, 
    //constructs a User_Model object with the retrieved username and password, 
    //and uses a RestClient to send a request to a server to register the new user. 
    //If the request is successful, the method prints the status code and a message indicating the success of the registration. If the request is unsuccessful, the method prints the error message.
    public void RegisterUser()
    {
        string username = GameObject.Find("CanvasSignIn/Background/InputUsername").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("CanvasSignIn/Background/InputPassword").GetComponent<TMP_InputField>().text;

        User_Model newUser = new User_Model() { password = password, username = username };

        RestClient.Post(GameManager.baseURI + "/api/user-register", newUser).Then(response =>
        {
            print(response.StatusCode.ToString());
            JObject jObject = JObject.Parse(response.Text);
            print(jObject.GetValue("message"));

        })
        .Catch(err =>
        {
            var error = err as RequestException;
            print(error.StatusCode);
            print(error.Response);
            print(err.Message);
        });

    }

    //This code is for signing in a user in a game. The method retrieves the user's username and password from a canvas, 
    //constructs a User_Model object with the retrieved username and password, 
    //and uses a RestClient to send a request to a server to sign the user in. 
    //If the request is successful, the method saves the user's JWT (JSON Web Token) in PlayerPrefs. 
    //If the request is unsuccessful, the method prints the error message.	
    public void SignIn()
    {
        string username = GameObject.Find("CanvasSignIn/Background/InputUsername").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("CanvasSignIn/Background/InputPassword").GetComponent<TMP_InputField>().text;

        User_Model user = new User_Model() { password = password, username = username };
        RestClient.Post<User_Model>(GameManager.baseURI + "/api/user-signin", user).Then(response =>
        {
            //save login data in PlayerPrefs
            PlayerPrefs.SetString("JWT", response.accessToken);                      

            print("Logged in...jwt token saved in SharedPreferences");
        })
        .Catch(err =>
        {
            var error = err as RequestException;
            print(error.StatusCode);
            print(error.Response);
            print(err.Message);
        });
    }
}
