using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UsableItemObject", menuName = "Scriptable Objects/UsableItemObject")]
public class ItemData : ScriptableObject 
{
    public string itemName;

    public string itemDescription;

    public Sprite itemSprite;

    public bool isItemKey;
    
}
