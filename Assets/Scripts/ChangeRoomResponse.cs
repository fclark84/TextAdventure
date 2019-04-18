using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/changeRoom")]
public class ChangeRoomResponse : ActionResponse
{
    //if you bring item to right room, it changes rooms to new one (unlock door)
    public Room roomToChangeTo;

    public override bool DoActionResponse(GameController controller)
    {
        //if you have the right item...
        if (controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            //set current room as the next room through unlocked door
            controller.roomNavigation.currentRoom = roomToChangeTo;
            controller.DisplayRoomText();
            return true;
        }
        //otherwise its wrong, return false
        return false; 
    }
}


