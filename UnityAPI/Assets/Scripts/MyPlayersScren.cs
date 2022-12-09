using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyPlayersScren : MonoBehaviour
{
    private void OnEnable()
    {
        GetPlayersForUser();
    }

    //A method for retrieving a list of players assigned to a user in a game. 
    //The method uses a RestClient to send a request to a server to retrieve the list of players. 
    //The request includes the user's JWT in the headers. 
    //If the request is successful, the method loops through the list of players, 
    //retrieves the image of each player using a separate RestClient request, and updates the player's name 
    //and image on the game canvas. If the request is unsuccessful, the method prints the error message.	
    private void GetPlayersForUser()
    {
        RestClient.GetArray<FootballPlayer_Model>(new RequestHelper
        {
            Uri = GameManager.baseURI + "/api/get-players-for-user",
            Method = "GET",
            Headers = new Dictionary<string, string>
            {
                {"x-access-token",PlayerPrefs.GetString("JWT") }
            }            
        }).Then(response =>
        {
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            int playerCardIndex = 0;
            foreach (FootballPlayer_Model model in response)
            {
                if (playerCardIndex >= playerCards.Length)
                    break; //stop loop because there aren't enough gameobjects to represent the number of assigned players to user

                GameObject playerCard = playerCards[playerCardIndex];

                //change player name
                playerCard.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = model.name;

                Debug.Log(model);
                Debug.Log(model.name);

                //set image of player
                RestClient.Get(new RequestHelper
                {
                    Uri = model.image_url,
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

                playerCardIndex += 1;
            }
        }).Catch(err =>
        {
            var error = err as RequestException;
            print(error.StatusCode);
            print(error.Response);
            print(err.Message);
        });


    }

}
