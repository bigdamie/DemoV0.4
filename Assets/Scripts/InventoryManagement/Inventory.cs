using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Inventory", fileName = "New Inventory")]
//This is the actual Inventory itselfSs
public class Inventory : ScriptableObject
{
    public int maxSize;
    public List<Item> thisInventory = new List<Item>();

    public  virtual void AddItem(Item newItem)
    {
        if (thisInventory.Count <= maxSize)
            thisInventory.Add(newItem);
    }

    public  virtual void RemoveItem(Item newItem)
    {
        thisInventory.Remove(newItem);
    }

}
