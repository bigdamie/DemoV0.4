using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InvUIManager : MonoBehaviour
{
    // Item info to display
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI description;
    public TextMeshProUGUI rarity;
    public TextMeshProUGUI averagePrice;
    public TextMeshProUGUI itemTrait;
    public Image attributeIcon;

    public InvItemShell currItemShell;

    public void ChangeDisplayInfo(string name, string itemDesc, string attribute = null, 
                                    string price = "", string itemRarity="", Sprite newIcon = null)
    {
        itemName.text = name;
        description.text = itemDesc;
        rarity.text = itemRarity;
        averagePrice.text = price;
        itemTrait.text = attribute;
        attributeIcon.sprite = newIcon;


        /* For multiple traits
        for (int i = 0; i < attributes.Length; i++)
            itemTrait[i].text = attributes[i];

        for (int i = 0; i < icons.Length; i++)
            attributeIcons[i].sprite = icons[i];
            */
    }

    private void Start()
    {
        ChangeDisplayInfo("", "", "", "");
    }
}
