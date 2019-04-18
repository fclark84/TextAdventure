using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{

    public Room currentRoom;

    //create a dictionary to reference (all the words to escape a room)
    Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();

    GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    //passes exits to game
    public void UnpackExitsInRoom()
    {
        
        for (int i = 0; i < currentRoom.exits.Length; i++)
        {
            //add items to dictionary
            //dictionary now contains all the key words to exit the room
            exitDictionary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);

            //whichever exit we are looping over, send description to our list of descriptions
            controller.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
        }
        
    }

    public void AttemptToChangeRooms(string directionNoun)
    {
        //can you exit to (direction noun) (north south ect)
        if (exitDictionary.ContainsKey(directionNoun))
        {
            //this is how we change rooms, if they chose a correct direction
            currentRoom = exitDictionary[directionNoun];
            controller.LogStringWithReturn("You head off to the " + directionNoun);
            controller.DisplayRoomText();
        }
        else
        {
            //if the key (direction) is not there
            controller.LogStringWithReturn("There is no path to the " + directionNoun);
        }

    }

    public void ClearExits()
    {
        //empty out exit dictionary
        exitDictionary.Clear();
    }
}
