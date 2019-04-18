using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Exit is a very simple data class. It has no functions

[System.Serializable]
public class Exit
{
    //our key word, what we look for to exit a room
    public string keyString;
    public string exitDescription;
    public Room valueRoom;

}
