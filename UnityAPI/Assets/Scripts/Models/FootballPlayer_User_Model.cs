using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code defines a FootballPlayer_User_Model class, which is marked with the [Serializable] attribute. This attribute indicates that the class can be serialized, which means it can be converted to and from a format that can be stored or transmitted, such as JSON or XML.
//The FootballPlayer_User_Model class has two properties: playerId and userId. These properties are both marked as public, which means they can be accessed and modified from outside the class. The playerId property is an int type, and the userId property is also an int type. These properties represent the IDs of a football player and a user.

[Serializable]
public class FootballPlayer_User_Model 
{
    public int playerId;
    public int userId;
}
