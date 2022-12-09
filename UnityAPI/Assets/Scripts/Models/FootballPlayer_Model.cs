using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//This code defines a FootballPlayer_Model class, which is marked with the [Serializable] attribute. This attribute indicates that the class can be serialized, which means it can be converted to and from a format that can be stored or transmitted, such as JSON or XML.
//The FootballPlayer_Model class has three properties: id, name, and image_url. These properties are all marked as public, which means they can be accessed and modified from outside the class. The id property is an int type, the name property is a string type, and the image_url

[Serializable]
public class FootballPlayer_Model 
{
    public int id;
    public string name;
    public string image_url;
}
