using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    Infopanel infoPanel;
    Item item;
    public void AssignCell(Item pItem, Infopanel pInfopanel)
    {
        infoPanel = pInfopanel;
        item = pItem;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Infopanel.DisplayDetails(item);
        throw new System.NotImplementedException();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Infopanel.HideDetails(item);
        throw new System.NotImplementedException();
    }
}
