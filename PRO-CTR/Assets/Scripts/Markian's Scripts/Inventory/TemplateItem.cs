using UnityEngine;
using UnityEngine.UI;

// TemplateItem class representing an item in the inventory
public class TemplateItem : MonoBehaviour
{
    public string displayName = "Template Item";
    public string description = "This is a template item used for demonstration purposes.";
    public Image image;

    // Use Awake so sprite loading happens earlier and safely
    void Awake()
    {
        if (image == null)
            image = GetComponent<Image>();

        // Put your sprite at Assets/Resources/Art/itemSprite.png and load "Art/itemSprite"
        var sprite = Resources.Load<Sprite>("Art/itemSprite");
        if (image != null && sprite != null)
            image.sprite = sprite;
    }
}
