using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    //to show all text on screen
    public Text displayText;
    //array of inputactions
    public InputAction[] inputActions;

    [HideInInspector] public InteractableItem interactableItem;
    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();

    //this is our list to keep track of what actions have been done
    //while in the room
    List<string> actionLog = new List<string>();

    void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
        interactableItem = GetComponent<InteractableItem>();
    }

    void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    //Display what weve done (action log)
    public void DisplayLoggedText()
    {
        //takes our whole list of actions and turns it into one long string
        string logAsText = string.Join("\n", actionLog.ToArray());

        displayText.text = logAsText;
    }

    //Displays the text of the room
    public void DisplayRoomText()
    {
        //first clear everything from the room you were just in
        ClearCollectionsForNewRoom();

        //Now unpack everything for this room
        UnpackRoom();

        //join into single string
        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());

        //room description, then interaction descpritions are displayed
        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;
        LogStringWithReturn(combinedText);
    }

    //unpack the room contents
    void UnpackRoom()
    {
        roomNavigation.UnpackExitsInRoom();
        //get current room and prepare to display objects
        PrepareObjectsToTakeOrExamine(roomNavigation.currentRoom);
   
    }

    //prepare interactable objects
void PrepareObjectsToTakeOrExamine(Room curentRoom)
    {
        for (int i = 0; i < curentRoom.interactableObjectsInRoom.Length; i++)
        {
            //go over our array of objects in room, and show object descriptions
            string descriptionNotInInventory = interactableItem.GetObjectsNotInInventory(curentRoom, i);
                {
                if (descriptionNotInInventory != null)
                {
                    interactionDescriptionsInRoom.Add(descriptionNotInInventory);
                }
                
                InteractableObject interactableInRoom = curentRoom.interactableObjectsInRoom[i];
                //checking over all interactions
                for (int j = 0; j < interactableInRoom.interactions.Length; j++)
                {
                    Interaction interaction = interactableInRoom.interactions[j];
                    //what did they say? Was it examine?
                    if (interaction.inputAction.keyWord == "examine")
                    {
                        //if it is, add to examine dictionary
                        interactableItem.examineDictionary.Add(interactableInRoom.noun, interaction.textResponse);
                    }
                    if (interaction.inputAction.keyWord == "take")
                    {
                        //if it is, add to examine dictionary
                        interactableItem.takeDictionary.Add(interactableInRoom.noun, interaction.textResponse);
                    }
                }
            }
        }
    }

    //can you do that thing? if not, tell player they cant
    public string TestVerbDictionaryWithNoun(Dictionary<string, string> verbDictionary, string verb, string noun)
    {
        if (verbDictionary.ContainsKey(noun))
        {
            return verbDictionary[noun];
        }
        return "You can't " + verb + " " + noun;
    }
    //clear room
    void ClearCollectionsForNewRoom()
    {
        interactableItem.ClearCollections();
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
    }

    //takes string to add, and passes it into action log 
    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    // Update is called once per frame
    private void Update()
    {

    }


}
