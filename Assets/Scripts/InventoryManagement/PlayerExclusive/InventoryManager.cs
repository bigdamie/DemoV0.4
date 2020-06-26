using UnityEngine;
using System.Collections.Generic;

// This is for the player's inventory GUI and stuff
public class InventoryManager : MonoBehaviour
{
    public PlayerInventory theInventory;
    public List<InvItemShell> heldItems = new List<InvItemShell>();

    public int[,] grid;//2 dimensions

    public int maxGridX;
    public int maxGridY;

    Vector2 cellSize = new Vector2(32f,32f);

    public InvItemShell slotPrefab;

    public List<Vector2> inventoryPositions = new List<Vector2>();

    private void Start()
    {
        maxGridY = (int)((theInventory.maxSize + 1) / maxGridX);

        grid = new int[maxGridX, maxGridY]; // matrix of bag size
    }

    public bool AddItemInOpenSpace(GameObject newItemPrefab)
    {
        Item newItem = newItemPrefab.GetComponent<Item>();

        int contX = (int)newItem.itemSize.x;
        int contY = (int)newItem.itemSize.y;

        for(int i = 0; i < maxGridX; i++)
        {
            for(int j=0; j < maxGridY; j++)
            {
                if (inventoryPositions.Count != (contX * contY)) // if false, the item fits
                {
                    //for each x,y position (i,j), test if item fits
                    for (int sizeY = 0; sizeY < contY; sizeY++) // item size in Y
                    {
                        for (int sizeX = 0; sizeX < contX; sizeX++)//item size in X
                        {
                            if ((i + sizeX) < maxGridX && (j + sizeY) < maxGridY && grid[i + sizeX, j + sizeY] != 1)//inside of index
                            {
                                Vector2 pos;
                                pos.x = i + sizeX;
                                pos.y = j + sizeY;
                                inventoryPositions.Add(pos);
                            }
                            else
                            {
                                sizeX = contX;
                                sizeY = contY;
                                inventoryPositions.Clear();
                            }
                        }
                    }
                }
                else
                    break;
            }
        }

        if(inventoryPositions.Count == (contX * contY)) // If the item's in the inventory
        {
            InvItemShell myInvItem = Instantiate(slotPrefab);

            myInvItem.startPosition = new Vector2(inventoryPositions[0].x, inventoryPositions[0].y); //1st position
            myInvItem.item = newItem;
            myInvItem.icon.sprite = newItem.itemIcon;

            myInvItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            myInvItem.GetComponent<RectTransform>().anchorMax = new  Vector2(0f,1f);
            myInvItem.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 1f);
            myInvItem.transform.SetParent(this.GetComponent<RectTransform>(), false);
            myInvItem.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            myInvItem.GetComponent<RectTransform>().anchoredPosition =
                                                        new Vector2(myInvItem.startPosition.x * cellSize.x,
                                                                    -myInvItem.startPosition.y * cellSize.y);

            heldItems.Add(myInvItem);
            
            for(int k=0; k < inventoryPositions.Count;k++)
            {
                int posToAddX = (int)inventoryPositions[k].x;
                int posToAddY = (int)inventoryPositions[k].y;
                grid[posToAddX, posToAddY] = 1;
            }
            inventoryPositions.Clear();

            theInventory.AddItem(newItem);

            return true;
        }
        return false;
    }

}
