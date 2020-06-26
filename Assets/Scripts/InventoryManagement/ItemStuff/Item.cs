using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public float currentAverageValue;
    
    [TextArea(3, 5)]
    [SerializeField] public string itemDescription;
    
    [SerializeField] public string itemType;
    [SerializeField] public string itemName;
    [SerializeField] public string rarity;

    public Vector2 itemSize; //x and y
    public Sprite itemIcon;

    [SerializeField]
    public string trait;
    [SerializeField]
    public Sprite traitIcon;

    [SerializeField] public GameObject thisItemsPrefab; //This is what actually gets put into inventories

    public void GetItem()
    {
       
        InventoryManager invMan = FindObjectOfType<InventoryManager>();

        bool pickedUpItem = invMan.AddItemInOpenSpace(thisItemsPrefab); //add to the bag matrix.

        if (pickedUpItem)
        {
            Destroy(this.gameObject);
        }
        
    }
}
