using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Item", menuName = "Inventory System/Items/Plant")]
public class PlantObject : ItemObject
{
    public int plantPrice;
    public float happinessValue;
    public void Awake()
    {
        type = itemType.plant;
    }
}
