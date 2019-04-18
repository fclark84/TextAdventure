using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TextAdventure/InputActions/Go")]

public class Go : InputAction {

    public override void RespondToInput(GameController controller, string[] seperatedInputWords)
    {
        //pass in reference to game controller 
        //to say yes this is ocrrect contoller
        //then pass our seperated input words to the controller
        controller.roomNavigation.AttemptToChangeRooms(seperatedInputWords[1]);
    }
}
