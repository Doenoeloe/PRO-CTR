//using UnityEngine;
//using System.Collections.Generic;
//public class InventoryUI : MonoBehaviour
//{
//    Inventory inventory;
//    [SerializeField] Infopanel infoPanel;
//    //place where all the cells are located
//    [SerializeField] GameObject cellGrid;
//    [SerializeField] GameObject cellPrefab;
//    List<GameObject> cells;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        inventory = GetComponent<Inventory>();
//    }
//    void CreateCellUI(Item pItem)
//    {
//        var cell = Instantiate(cellPrefab, cellGrid.transform, false);
//        cell.GetComponent<InventoryCell>().AssignCell(pItem, infoPanel);
//        cells.Add(cell);
//    }
//    void Refresh()
//    {
//        foreach (var cell in cells)
//        {
//            Destroy(cell);
//        }
//        cells.Clear();
//        foreach (Item item in inventory.GetItems())
//        {
//            CreateCellUI(item);
//        }
//    }
//}
