using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Infopanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] public Image InfoPanelImage;
    [SerializeField] public TMP_Text InfoPanelName;
    [SerializeField] public TMP_Text InfoPanelDescription;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisplayDetails(Item pItem)
    {
        InfoPanelName.text = pItem.name;
        InfoPanelImage = pItem.image;
        InfoPanelDescription.text = pItem.description;
    }
    public void HideDetails()
    {
        InfoPanelName.text = null;
        InfoPanelImage = null;
        InfoPanelDescription.text = null;
    }
}
