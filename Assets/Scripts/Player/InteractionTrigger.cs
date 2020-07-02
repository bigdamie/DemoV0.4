using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is basically for all the things the player will
//run up and press "A" on. Certain larger things like dialogue
//can have their own scripts
public class InteractionTrigger : MonoBehaviour
{
    ItemObj itemOnFloor = null;
    DisplayCase display = null;

    PauseManager pMan = null;

    private void Start()
    {
        pMan = FindObjectOfType<PauseManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            itemOnFloor = other.GetComponent<ItemObj>();
        }

        else if(other.CompareTag("OpenInventory"))
        { 
            display = other.GetComponent<DisplayCase>(); 
        }
        //else if(other.CompareTag("")
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            itemOnFloor = null;
        }

        else if (other.CompareTag("OpenInventory"))
        { 
            display = null; 
        }
        //else if(other.CompareTag("")
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && itemOnFloor)
            itemOnFloor.GetItem();

        else if (Input.GetButtonDown("Interact") && display)
        {
            pMan.ResumeButton();
            pMan.OpenInventory();
        }
    }
}
