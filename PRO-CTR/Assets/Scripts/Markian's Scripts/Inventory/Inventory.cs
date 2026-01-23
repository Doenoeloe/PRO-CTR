using System.Collections.Generic;
using UnityEngine;
//Inventory class representing the player's inventory logic
public class Inventory : MonoBehaviour
{
    public List<TemplateItem> items = new List<TemplateItem>();
    // Add an item to the inventory
    public void AddItem(TemplateItem item)
    {
        items.Add(item);
    }
    // Get the list of items in the inventory
    public List<TemplateItem> GetItems()
    {
        return items;
    }
    // Remove an item from the inventory
    public void RemoveItem(TemplateItem item)
    {
        items.Remove(item);
    }
    // Example initialization of the inventory with some items
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            var newItem = new GameObject("Item" + i, typeof(TemplateItem)).GetComponent<TemplateItem>();
            newItem.displayName = "Item" + i;
            newItem.description = "This is item number " + i;
            items.Add(newItem);
        }
    }
}
