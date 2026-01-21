using UnityEngine;
// InventoryInput class to handle inventory UI input(Mainly for debugging purposes)
public class InventoryInput : MonoBehaviour
{
    [SerializeField] InventoryUI invUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (invUI.gameObject.activeSelf)
            {
                invUI.Close();
            }
            else
            {
                invUI.Open();
            }
        }
    }
}
