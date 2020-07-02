using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj : MonoBehaviour
{
    public Item thisItem;

    public void GetItem()
    {

        InventoryManager invMan = FindObjectOfType<InventoryManager>();

        bool pickedUpItem = invMan.AddItemInOpenSpace(this.gameObject); //add to the bag matrix.

        if (pickedUpItem)
            Destroy(this.gameObject);
    }
}
