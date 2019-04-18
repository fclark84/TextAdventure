using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for this class, if something is in our inventory, it no longer shows in the room.
public class InteractableItem : MonoBehaviour {
    //public list that contains all items we can use
    public List<InteractableObject> usableItemList;
    //create a dictionary
    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();


    [HideInInspector] public List<string> nounsInRoom = new List<string>();

    //Create a dictionary
    Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();

    List<string> nounsInInventory = new List<string>();
    GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }
    public string GetObjectsNotInInventory(Room currentRoom, int i)
    {
        InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i];
        if (!nounsInInventory.Contains(interactableInRoom.noun))
        {
            nounsInRoom.Add(interactableInRoom.noun);
            return interactableInRoom.description;
        }
        return null;
    }

    //called whenever we take an item, updates use dictionary 
    //to include everything we can potentioally use
    public void AddActionResponsesToUseDictionary()
    {
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            string noun = nounsInInventory[i];
            //while looping all nouns in inventory,
            InteractableObject interactableObjectInInventory = GetInteractableObjectFromUsableList(noun);
            //if we cant find it, just keep going with the loop
            if (interactableObjectInInventory == null)
                continue;
            //if we can actually find it,
            for (int j = 0; j < interactableObjectInInventory.interactions.Length; j++)
            {
                Interaction interaction = interactableObjectInInventory.interactions[j];

                if (interaction.actionResponse == null) continue;
                //however if it is not null
                if(!useDictionary.ContainsKey(noun))
                {
                    useDictionary.Add(noun, interaction.actionResponse);
                }
            }
        }
    }


    //takes the noun of the object we want to use, and checks if its a usable item
    InteractableObject GetInteractableObjectFromUsableList(string noun)
    {
        for (int i = 0; i < usableItemList.Count; i++)
        {
            if (usableItemList[i].noun == noun)
            {
                return usableItemList[i];
            }
        }
        return null;
    }

    public void DisplayInventory()
    {
        controller.LogStringWithReturn("You look in your backpack, you have: ");
            for (int i = 0; i < nounsInInventory.Count; i++)
            {
                controller.LogStringWithReturn(nounsInInventory[i]);
            }
        
    }
    //clear out our room of items
    public void ClearCollections()
    {
        examineDictionary.Clear();
        takeDictionary.Clear();
        nounsInRoom.Clear();

    }

    public Dictionary<string, string> Take (string[] seperatedInputWords)
    {
        string noun = seperatedInputWords[1];
        if (nounsInRoom.Contains(noun))
        {
            nounsInInventory.Add(noun);
            AddActionResponsesToUseDictionary();
            nounsInRoom.Remove(noun);
            return takeDictionary;
        }
        //our error message if there is nothing to take
        else
        {
            controller.LogStringWithReturn("There is no " + noun + " here to take.");
            return null;
        }
    }

    public void UseItem(string[] seperatedInputWords)
    {
        string nounToUse = seperatedInputWords[1];//1 is the noun

        if (nounsInInventory.Contains(nounToUse))
        {
            if (useDictionary.ContainsKey(nounToUse))
            {
                //passed our two checks, now passing in noun we want to use in dictionary
                //will return an action response, now we call DoActionResponse
                bool actionResult = useDictionary[nounToUse].DoActionResponse(controller);
                //if it fails...
                if (!actionResult)
                {
                    controller.LogStringWithReturn("Hmmm, Nothing Happens");
                }
            }
            else
            {
                //this means its not in the use dictionary
                controller.LogStringWithReturn("You can't use the " + nounToUse);
            }
        }
        else
        {
            controller.LogStringWithReturn("There is no " + nounToUse + " in your inventory to use");
        }
    }
}
