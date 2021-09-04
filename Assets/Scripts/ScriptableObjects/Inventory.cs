using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int defaultNumberOfKeys;

    public void OnAfterDeserialize()
    {
        // Game End
        numberOfKeys = defaultNumberOfKeys;
    }

    public void OnBeforeSerialize()
    {
        // Game Start
    }

    public void AddItem(Item itemToAdd)
    {
        // Check if Item is key
        if (itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            // Check if item is in inventory, if not add item
            if(!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }

    }

}
