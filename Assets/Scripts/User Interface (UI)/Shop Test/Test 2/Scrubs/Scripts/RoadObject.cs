using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Item", menuName = "Inventory System/Items/Road")]
public class RoadObject : ItemObject
{
    public int roadPrice;
    public void Awake()
    {
        type = itemType.road;
    }
}
