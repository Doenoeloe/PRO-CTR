using TMPro;
using UnityEngine;
using UnityEngine.UI;
// Infopanel class to display item details in the inventory UI
public class Infopanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] public Image InfoPanelImage;
    [SerializeField] public TMP_Text InfoPanelName;
    [SerializeField] public TMP_Text InfoPanelDescription;
    [SerializeField] public Button UseButton;
    [SerializeField] public Button DropButton;

    public bool isItemSelected = false;
    TemplateItem selectedItem;
    // Display the details of the given item in the info panel
    public void DisplayDetails(TemplateItem pItem)
    {
        // Disable buttons while previewing item details
        UseButton.interactable = false;
        DropButton.interactable = false;

        if (pItem == null) return;

        InfoPanelName.text = pItem.displayName;
        InfoPanelDescription.text = pItem.description;

        if (InfoPanelImage != null && pItem.image != null)
        {
            InfoPanelImage.sprite = pItem.image.sprite;
        }
        else if (InfoPanelImage != null)
        {
            InfoPanelImage.sprite = null;
        }
    }

    // Hide the details in the info panel
    public void HideDetails()
    {
        if (InfoPanelName != null) InfoPanelName.text = "";
        if (InfoPanelDescription != null) InfoPanelDescription.text = "";
        if (InfoPanelImage != null) InfoPanelImage.sprite = null;
    }
    public void SaveSelection(TemplateItem pItem)
    {
        selectedItem = pItem;
        isItemSelected = true;
    }
    public void LoadSelection()
    {
        // Enable buttons when an item is selected
        UseButton.interactable = true;
        DropButton.interactable = true;

        InfoPanelName.text = selectedItem.displayName;
        InfoPanelDescription.text = selectedItem.description;
        InfoPanelImage.sprite = selectedItem.image.sprite;
    }
}
