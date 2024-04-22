using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Item", menuName = "Inventory System/Items/Building")]
public class BuildingObject : ItemObject
{
    public int buildingPrice;
    public void Awake()
    {
        type = itemType.building;
    }
}
