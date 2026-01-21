using UnityEngine;
using System.Collections.Generic;
public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    [SerializeField] Infopanel infoPanel;
    //place where all the cells are located
    [SerializeField] GameObject cellGrid;
    [SerializeField] GameObject cellPrefab;
    List<GameObject> cells;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        cells = new List<GameObject>();
        Close();
    }

    //Create a cell UI for the given item
    void CreateCellUI(TemplateItem pItem)
    {
        if (cellPrefab == null || cellGrid == null) return;

        var cell = Instantiate(cellPrefab, cellGrid.transform, false);
        var cellComp = cell.GetComponent<InventoryCell>();
        if (cellComp != null)
            cellComp.AssignCell(pItem, infoPanel);

        cells.Add(cell);
    }

    //Refresh the inventory UI
    void Refresh()
    {
        // Destroy existing cells
        foreach (var cell in cells)
        {
            if (cell != null)
                Destroy(cell);
        }
        cells.Clear();

        if (inventory == null) return;

        foreach (TemplateItem item in inventory.GetItems())
        {
            CreateCellUI(item);
        }
    }

    //Open the inventory UI
    public void Open()
    {
        gameObject.SetActive(true);
        Refresh();
    }

    //Close the inventory UI
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
