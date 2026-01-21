using UnityEngine;
using UnityEngine.EventSystems;
// InventoryCell class to handle individual inventory cell interactions
public class InventoryCell : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
{
    Infopanel infoPanel;
    TemplateItem item;
    // Assign the item and infopanel to this cell
    public void AssignCell(TemplateItem pItem, Infopanel pInfopanel)
    {
        infoPanel = pInfopanel;
        item = pItem;
    }
    // Show item details on pointer enter
    public void OnPointerEnter(PointerEventData eventData)
    {
        infoPanel.DisplayDetails(item);
    }
    // Hide item details on pointer exit
    public void OnPointerExit(PointerEventData eventData)
    {
        if (infoPanel.GetComponent<Infopanel>().isItemSelected)
        {
            infoPanel.LoadSelection();
        }
        else
        {
            infoPanel.HideDetails();
        }

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        infoPanel.SaveSelection(item);
    }
}
