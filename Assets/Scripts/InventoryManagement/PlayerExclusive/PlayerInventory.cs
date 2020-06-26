using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Player Inventory")]
public class PlayerInventory : Inventory
{
    InventoryManager invMan;

    private void OnEnable()
    {
        invMan = FindObjectOfType<InventoryManager>();
    }

    public override void AddItem(Item newItem)
    {
        base.AddItem(newItem);
    }

    public void AddItem(Item newItem, bool getDisplayItem)
    {
       
    }

    public override void RemoveItem(Item newItem)
    {
        base.RemoveItem(newItem);
    }
}
