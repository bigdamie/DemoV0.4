using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="New Item")]
public class Item : ScriptableObject
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

   
}
