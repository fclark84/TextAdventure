using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionResponse : ScriptableObject
{
    //key to check against to see if you are in the right room
    public string requiredString;
    public abstract bool DoActionResponse(GameController controller);

}
