using UnityEngine;

public interface IPickups
{
    string itemName { get; set; }

    string itemDescription { get; set; }

    Sprite itemSprite { get; set; }

    bool isItemKey { get; set; }

    void PickUpItem();

    void UseItem();

    //void RemoveItemFromInventory();
}