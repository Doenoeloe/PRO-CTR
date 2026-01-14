using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData itemData;

    SpriteRenderer spriteRenderer = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemData.itemSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
