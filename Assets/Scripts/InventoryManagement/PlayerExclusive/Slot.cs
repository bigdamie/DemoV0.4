using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Inventory theInventory;
    [SerializeField] GameObject slotPrefab;

    private void Start()
    {
        for(int i=0; i<theInventory.maxSize; i++)
        {
           Instantiate(slotPrefab, transform);
        }
    }

}
