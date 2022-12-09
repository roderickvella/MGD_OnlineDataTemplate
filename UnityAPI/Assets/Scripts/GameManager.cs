using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class GameManager : MonoBehaviour
{
    //The backend's server address
    public static string baseURI = "http://localhost:3000"; 

    //This method creates a Sprite object from a Texture2D object. The method takes in a Texture2D object as a parameter and returns a Sprite object that is created using the Sprite.Create method. The Sprite.Create method takes in the Texture2D object as well as several other parameters.
    //The method is marked as public and static, which means it can be called on the class itself without needing to create an instance of the class. This allows the method to be used as a utility method that can be called from anywhere in the code.
    //Mainly used to convert images downloaded from the web into sprites and shown on canvas.
    public static Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

}
