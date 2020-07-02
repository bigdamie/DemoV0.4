using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisplayCase : Interactable
{
    [SerializeField] InvUIManager invUIman;
    [SerializeField] InventoryManager invMan;

    [SerializeField] ParticleSystem particles;
    [SerializeField] Inventory displayedItem;
    DisplayCaseManager displayMan;

    [SerializeField] PlayerInventory playerInventory;
    SpriteRenderer sprend;
    bool takeItem;

    private void Start()
    {
        sprend = GetComponentInChildren<SpriteRenderer>();
        displayMan = GetComponentInParent<DisplayCaseManager>();
        invUIman = FindObjectOfType<InvUIManager>();
        invMan = FindObjectOfType<InventoryManager>();
        stay = true;

        DisplayItem();
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag(otherTag))
        {
            displayMan.currDisplay = this;
            particles.gameObject.transform.position = transform.position + new Vector3(0, 1, 0);
            particles.Play();
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (other.CompareTag(otherTag))
        {
            displayMan.currDisplay = null;

            particles.Stop();
        }
    }

    public void DisplayItem()
    {
        if (playerInRange)
        {
            if(displayedItem.thisInventory.Count > 0)
            {
                Item newItem = displayedItem.thisInventory[0];

                takeItem = invMan.AddItemInOpenSpace(newItem.thisItemsPrefab);

              
                displayedItem.RemoveItem(displayedItem.thisInventory[0]);

                myNotification.Raise();              
            }

            displayedItem.AddItem(invUIman.currItemShell?.item);
            playerInventory.RemoveItem(invUIman.currItemShell?.item);
        }

        if (displayedItem.thisInventory.Count > 0)
        sprend.sprite = displayedItem.thisInventory[0]?.itemIcon;


    }
}
