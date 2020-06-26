using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InvItemShell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    //script with the items in the bag, drap and drop functions, reescaling based on item size.
    //This script is present in each collected item

    // ADD ITEM ROTATION

    public Vector2 slotSize = new Vector2(32f,32f); //slot cell size 
    public Item item;

    public Vector2 startPosition;
    public Vector2 oldPosition;
    public Image icon;

    InventoryManager invMan;
    void Start()
    {
        invMan = FindObjectOfType<InventoryManager>();

        #region Rescaling
        //Scaling object to inventory/slot size
        this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, item.itemSize.y * slotSize.y);
        this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, item.itemSize.x * slotSize.x);

        //Doing the same for this objects children
        // Which will be the icon + slot outline
        foreach (RectTransform child in transform)
        {
            child.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, item.itemSize.y * child.rect.height);
            child.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, item.itemSize.x * child.rect.width);

            foreach (RectTransform iconChild in child)
            {
                iconChild.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, item.itemSize.y * iconChild.rect.height);
                iconChild.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, item.itemSize.x * iconChild.rect.width);
                iconChild.localPosition = new Vector2(child.localPosition.x + child.rect.width / 2, child.localPosition.y + child.rect.height / 2 * -1f);
            }

        }
        #endregion
    }

 

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("bert");     
    }

    public void OnPointerEnter(PointerEventData eventData) // shows item description
    {
        //Gets display info from item cursor hovers over

        string title = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<InvItemShell>().item.itemName;
        string body = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<InvItemShell>().item.itemDescription;
        string attribute = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<InvItemShell>().item.trait;
        Sprite icon_attribute = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<InvItemShell>().item.traitIcon;
        string rarity = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<InvItemShell>().item.rarity;
        string avgPrice = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<InvItemShell>().item.currentAverageValue.ToString();
        InvUIManager descript = FindObjectOfType<InvUIManager>();

        descript.ChangeDisplayInfo(title, body, attribute, avgPrice, rarity, icon_attribute);
        descript.currItemShell = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InvUIManager descript = FindObjectOfType<InvUIManager>();

        descript.ChangeDisplayInfo("", "", "", "");
        descript.currItemShell = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        oldPosition = transform.GetComponent<RectTransform>().anchoredPosition;

        GetComponent<CanvasGroup>().blocksRaycasts = false; // disable registering hit on item
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        //allow the intersection between old pos and new pos.
        for (int i = 0; i < item.itemSize.y; i++)
        {
            for (int j = 0; j < item.itemSize.x; j++)
            {
                invMan.grid[(int)startPosition.x + j, (int)startPosition.y + i] = 0;
            }
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 finalPos = GetComponent<RectTransform>().anchoredPosition; //position that the item was dropped on canvas

            Vector2 finalSlot;
            finalSlot.x = Mathf.Floor(finalPos.x / slotSize.x); //which x slot it is
            finalSlot.y = Mathf.Floor(-finalPos.y / slotSize.y); //which y slot it is

            // test if item is inside slot area
            if (((int)(finalSlot.x) + (int)(item.itemSize.x) - 1) < invMan.maxGridX && 
                ((int)(finalSlot.y) + (int)(item.itemSize.y) - 1) < invMan.maxGridY && 
                ((int)(finalSlot.x)) >= 0 && (int)finalSlot.y >= 0) 
            {
                List<Vector2> newPosItem = new List<Vector2>(); //new item position in bag
                bool fit = false;
                

                for (int sizeY = 0; sizeY < item.itemSize.y; sizeY++)
                {
                    for (int sizeX = 0; sizeX < item.itemSize.x; sizeX++)
                    {
                        if (invMan.grid[(int)finalSlot.x + sizeX, (int)finalSlot.y + sizeY] != 1)
                        {
                            Vector2 pos;
                            pos.x = (int)finalSlot.x + sizeX;
                            pos.y = (int)finalSlot.y + sizeY;
                            newPosItem.Add(pos);
                            fit = true;
                        }
                        else
                        {
                            fit = false;

                            this.transform.GetComponent<RectTransform>().anchoredPosition = oldPosition; //back to old pos
                            sizeX = (int)item.itemSize.x;
                            sizeY = (int)item.itemSize.y;
                            newPosItem.Clear();

                        }

                    }

                }
                if (fit)
                { //delete old item position in bag
                    for (int i = 0; i < item.itemSize.y; i++) //through item Y
                    {
                        for (int j = 0; j < item.itemSize.x; j++) //through item X
                        {
                            invMan.grid[(int)startPosition.x + j, (int)startPosition.y + i] = 0; //clean old pos

                        }
                    }

                    for (int i = 0; i < newPosItem.Count; i++)
                    {
                        invMan.grid[(int)newPosItem[i].x, (int)newPosItem[i].y] = 1; // add new pos
                    }

                    this.startPosition = newPosItem[0]; // set new start position
                    transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(newPosItem[0].x * slotSize.x, -newPosItem[0].y * slotSize.y);
                }
                else //It'll snap back to its OG pos
                {
                    for (int i = 0; i < item.itemSize.y; i++) //through item Y
                    {
                        for (int j = 0; j < item.itemSize.x; j++) //through item X
                        {
                            invMan.grid[(int)startPosition.x + j, (int)startPosition.y + i] = 1; //back to position 1;

                        }
                    }
                }
            }
            else
            { // out of index, back to the old pos
                this.transform.GetComponent<RectTransform>().anchoredPosition = oldPosition;
            }
        }
        else
        {
            PlayerMovement3D player = FindObjectOfType<PlayerMovement3D>();

            
            Instantiate(item.thisItemsPrefab, new Vector2(player.transform.position.x + Random.Range(-1.5f, 1.5f), 
                    player.transform.position.y + Random.Range(-1.5f, 1.5f)), Quaternion.identity); //dropa o item
                    

            Destroy(this.gameObject);
                           
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true; //register hit on item again
    }

 
   
}
