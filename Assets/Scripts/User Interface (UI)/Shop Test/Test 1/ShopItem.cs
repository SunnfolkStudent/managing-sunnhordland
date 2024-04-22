using System.Collections;
using System.Collections.Generic;using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu (fileName = "New Item")]
public class ShopItem : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite artwork;

    public int owned;
    public int price;
}
