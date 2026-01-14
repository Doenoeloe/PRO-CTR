using TMPro;
using UnityEngine.UI;

public interface IPickups
{
    string itemName { get; set; }

    string itemDescription { get; set; }

    Image itemSprite { get; set; }

    bool isItemKey { get; set; }

    void PickUpItem();

    void RemoveItemFromInventory();
}
