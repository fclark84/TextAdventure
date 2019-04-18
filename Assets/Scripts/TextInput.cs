using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class to take text that the player gives and return a reaction (go through a door, ect)

public class TextInput : MonoBehaviour {
    //Inputfield is where we input text (duh)
    public InputField inputField;
    GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();

        //call acceptstringinput any time someone hits enter(submit) This is a Listener Event
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    void AcceptStringInput(string userInput)
    {
        //make sure everything is lower case to make it simple to match
        userInput = userInput.ToLower();

        //take input and mirror back to player to see what they typed
        controller.LogStringWithReturn(userInput);

        //create single item in array, that is a space (aka look for spaces to seperate words. go (space) north
        char[] delimiterCharacters = { ' ' };
        //our array of seperated input words are what the user inputs delmited
        string[] separatedInputWords = userInput.Split(delimiterCharacters);
        //loop over array of input actions, checks slot zero which is our verb
        for (int i = 0; i < controller.inputActions.Length; i++)
        {
            InputAction inputAction = controller.inputActions[i];
            if (inputAction.keyWord == separatedInputWords[0])
            {
                inputAction.RespondToInput(controller, separatedInputWords);
            }

        }
        //call input complete to end the process
        InputComplete();
    }

    void InputComplete()
    {
        //update the action log
        controller.DisplayLoggedText();
        //when you hit return, it takes away focus from input field. This reactivates it to "click" the input field again
        inputField.ActivateInputField();
        //get ready to type something new by clearing out text field
        inputField.text = null;
    }
}
