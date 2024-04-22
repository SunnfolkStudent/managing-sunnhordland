using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public ShopItem item;
    
    public Text nameText;
    public Text itemDescription;

    public Image artwork;

    public Text owned;
    public Text price;

    private Dictionary<ShopItem, int> _shopItemsInStock;
    
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = item.name;
        itemDescription.text = item.description;
        artwork.sprite = item.artwork;
        owned.text = item.owned.ToString();
        price.text = item.price.ToString();
    }

    private int NumberOwnedOfItem(int itemAmount)
    {
        return itemAmount;
    }
}