using Newtonsoft.Json.Linq;
using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetAllPlayersScreen : MonoBehaviour
{
    private void OnEnable()
    {
        GetAllPlayers();
    }

    //GetAllPlayers() makes a call to a remote API using the RestClient.GetArray method, passing in a URL that includes the string "/api/players-get-all". This call retrieves an array of FootballPlayer_Model objects from the API.
    //Next, the method finds all GameObjects in the current scene with the tag "PlayerCard". It then iterates over each of these game objects, using the data from the FootballPlayer_Model objects returned by the API to update the properties of each game object.
    //For each game object, the method updates the player's name, sets the onClick listener for a button named "BtnAddToCollection" to call the AddPlayerToCollection method and pass in the player's ID, and sets the player's image by making another API call to retrieve the player's image from a URL stored in the image_url property of the FootballPlayer_Model object.
    //If any errors occur during the execution of this method, they are caught and logged using the Catch method.
    private void GetAllPlayers()
    {
        RestClient.GetArray<FootballPlayer_Model>(GameManager.baseURI + "/api/players-get-all").Then(response =>
        {
            //print(response.Length);
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            int responseIndex = 0;

            foreach(GameObject playerCard in playerCards)
            {
                //change player name
                playerCard.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = response[responseIndex].name;

                //set button
                int playerId = response[responseIndex].id;
                playerCard.transform.Find("BtnAddToCollection").GetComponent<Button>().onClick.AddListener(delegate { AddPlayerToCollection(playerId); });

                //set image of player
                RestClient.Get(new RequestHelper
                {
                    Uri = response[responseIndex].image_url,
                    DownloadHandler = new DownloadHandlerTexture()
                }).Then(response =>
                {
                    Texture2D texture = ((DownloadHandlerTexture)response.Request.downloadHandler).texture as Texture2D;
                    Sprite webSprite = GameManager.SpriteFromTexture2D(texture);
                    playerCard.transform.Find("Image").GetComponent<Image>().sprite = webSprite;

                }).Catch(err =>
                {
                    var error = err as RequestException;
                    print(err.Message);
                });


                responseIndex += 1;
            }         


        }).Catch(err =>
        {
            var error = err as RequestException;
            print(error.StatusCode);
            print(error.Response);
            print(err.Message);
        });

    }

    //This method makes a call to a remote API using the RestClient.Post method. 
    //The playerId parameter passed to the method is used to create a new FootballPlayer_User_Model object, which is then passed as the Body parameter in the RequestHelper object passed to the RestClient.Post method. The URL for the API call includes the string "/api/player-user-add", and a header with the key "x-access-token" is added to the request, with the value taken from the "JWT" key in the PlayerPrefs store.
    //If the API call is successful, the response is parsed as a JObject and the value of the "message" property is printed to the console. If any errors occur during the execution of this method, they are caught and logged using the Catch method.
    private void AddPlayerToCollection(int playerId)
    {
        print("You have clicked the button with Player ID:" + playerId);
        FootballPlayer_User_Model footballPlayer = new FootballPlayer_User_Model() { playerId = playerId };

        RestClient.Post(new RequestHelper
        {
            Uri = GameManager.baseURI + "/api/player-user-add",
            Method = "POST",
            Headers = new Dictionary<string, string>
            {
                {"x-access-token",PlayerPrefs.GetString("JWT") }
            },
            Body = footballPlayer
        }).Then(response =>
        {
            JObject jObject = JObject.Parse(response.Text);
            print(jObject.GetValue("message"));

        }).Catch(err =>
        {
            var error = err as RequestException;
            print(error.StatusCode);
            print(error.Response);
            print(err.Message);
        });


    }

}
