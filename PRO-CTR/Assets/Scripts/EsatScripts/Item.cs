using UnityEngine;

public class Item : MonoBehaviour, IPickups
{
    [SerializeField] ItemData itemData;

    SpriteRenderer spriteRenderer = null;

    public string itemName
    {
        get { return itemData.itemName; }
        set { itemData.itemName = value; }
    }

    public string itemDescription
    {
        get { return itemData.itemDescription; }
        set { itemData.itemDescription = value; }
    }

    public Sprite itemSprite
    {
        get { return itemData.itemSprite; }
        set { itemData.itemSprite = value; }
    }

    public bool isItemKey
    {
        get { return itemData.isItemKey; }
        set { itemData.isItemKey = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemSprite;
        spriteRenderer.sortingOrder = 10;
        transform.localScale = new Vector3(2, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out PlayerMovement player) == true)
        {
            PickUpItem();
        }
    }

    public void PickUpItem()
    {
        Debug.Log("Picked up: " + itemName);

        Destroy(gameObject);
    }

    public void UseItem()
    {
        switch (itemName) 
        {
            case "health potion":
                break;
        }
    }
}
