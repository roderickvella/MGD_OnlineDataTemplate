using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code defines a User_Model class, which is marked with the [Serializable] attribute. This attribute indicates that the class can be serialized, which means it can be converted to and from a format that can be stored or transmitted, such as JSON or XML.
//The User_Model class has four properties: id, username, password, and accessToken. These properties are all marked as public, which means they can be accessed and modified from outside the class. The id property is an int type, the username and password properties are both string types, and the accessToken property is also a string type. These properties represent the ID, username, password, and access token of a user.

[Serializable]
public class User_Model 
{
    public int id;
    public string username;    
    public string password;
    public string accessToken;
}
