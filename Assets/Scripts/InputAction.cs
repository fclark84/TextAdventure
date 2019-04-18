using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputAction : ScriptableObject {

    public string keyWord;
    //takes in an array of strings that are our input words
    public abstract void RespondToInput (GameController controller, string[] seperatedInputWords);
}
