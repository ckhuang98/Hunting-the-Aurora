﻿using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;

    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        //set cached inventory to inventory from the game manager
        inventory = Inventory.instance;

        //add the UpdateUI method to the onItemChangedCallback delegate
        inventory.onItemChangedCallback += UpdateUI;

        //get the inventory slots under the itemsParent in Inventory
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    //Update the inventory slots
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].UpdateItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
