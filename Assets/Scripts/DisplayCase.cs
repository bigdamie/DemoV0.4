using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisplayCase : Interactable
{
    [SerializeField] InvUIManager invUIman;
    [SerializeField] InventoryManager invMan;

    [SerializeField] AllDisplays allDisplays;
    [SerializeField] ParticleSystem particles;
    [SerializeField] Inventory displayedItem;

    [SerializeField] PlayerInventory playerInventory;
    SpriteRenderer sprend;
    bool takeItem;

    private void Start()
    {
        sprend = GetComponentInChildren<SpriteRenderer>();
        invUIman = FindObjectOfType<InvUIManager>();
        invMan = FindObjectOfType<InventoryManager>();

        DisplayItem();
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag(otherTag))
        {
            particles.gameObject.transform.position = transform.position + new Vector3(0, 1, 0);
            particles.Play();
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (other.CompareTag(otherTag))
        {
            particles.Stop();
        }
    }

    public void DisplayItem()
    {
        if (playerInRange)
        {
            if(displayedItem.thisInventory.Count > 0)
            {
                playerInventory.AddItem(displayedItem.thisInventory[0].GetComponent<Item>());

                takeItem = invMan.AddItemInOpenSpace(displayedItem.thisInventory[0].thisItemsPrefab);

                displayedItem.RemoveItem(displayedItem.thisInventory[0]);
            }

            displayedItem.AddItem(invUIman.currItemShell?.item);
            playerInventory.RemoveItem(displayedItem.thisInventory[0].GetComponent<Item>());
        }

        if (displayedItem.thisInventory.Count > 0)
        sprend.sprite = displayedItem.thisInventory[0].itemIcon;

    }
}
