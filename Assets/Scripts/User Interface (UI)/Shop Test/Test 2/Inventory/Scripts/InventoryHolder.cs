using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    public InventoryObject Inventory;

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            Inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }
}
