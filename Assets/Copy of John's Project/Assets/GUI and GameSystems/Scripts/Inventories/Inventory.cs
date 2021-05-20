using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Items> inventory = new List<Items>(); // list of items
    [SerializeField]private bool showIMGUIInventory = true; // tick on untick to make it show or not
    private Items selectedItem = null;// places item in this variable

    #region Canvas Inventory
    [SerializeField] private Button ButtonPrefab;
    [SerializeField] private GameObject InventoryGameObject;
    [SerializeField] private GameObject InventoryContent;
    [SerializeField] private GameObject FilterContent;
    #endregion





    #region Display Inventory
    private Vector2 scrollPosition; // just for the IMGUI to scroll through it.
    private string sortType = "All"; // sorting through Inventory
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryGameObject.SetActive(true);
            DisplayItemsCanvas();
        }
    }

    private void DisplayItemsCanvas()
    {

        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].Type.ToString() == sortType || sortType == "All")
            {
                Button buttonGO = Instantiate<Button>(ButtonPrefab, InventoryContent.transform);
                Text buttonText = buttonGO.GetComponentInChildren<Text>();
                buttonGO.name = inventory[i].Name + "button";
                buttonText.text = inventory[i].Name;
            }
        }
    }

    
    private void OnGUI()
    {
        // loops through item typpe, puts item type on it and clicking it will give item(?)
        if (showIMGUIInventory)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

            List<string> itemTypes = new List<string>(Enum.GetNames(typeof(Items.ItemType)));
            itemTypes.Insert(0, "All");

            for (int i = 0; i < itemTypes.Count; i++)
            {
                if (GUI.Button(new Rect(
                    (Screen.width / itemTypes.Count) * i
                    , 10
                    , Screen.width / itemTypes.Count
                    , 20), itemTypes[i]))
                {
                    sortType = itemTypes[i];
                }
            }
       
            Display();
            if(selectedItem != null)
            {
                DisplaySelectedItem();
            }
        }

    }
    
    
    private void DisplaySelectedItem()
    {
        GUI.Box(new Rect(Screen.width / 4, Screen.height / 3,
            Screen.width / 5, Screen.height / 5),
            selectedItem.Icon);

        GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 5),
            Screen.width / 7, Screen.height / 15),
            selectedItem.Name);

        GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 3), 
            Screen.width / 5, Screen.height / 5), selectedItem.Description +
            "\nValue:" + selectedItem.Value +
            "\nAmount:" + selectedItem.Amount);

            


    }

    private void Display()
    {
        scrollPosition = GUI.BeginScrollView(new Rect(0, 40, Screen.width, Screen.height - 40),
            scrollPosition,
            new Rect(0,0,0, inventory.Count * 30),
            false,
            true);
        int count = 0;
        for(int i = 0; i < inventory.Count; i++)
        {
            if ( inventory[i].Type.ToString() == sortType||sortType == "All")
            {
                if (GUI.Button(new Rect(30, 0 + (count * 30), 200, 30), inventory[i].Name))
                {
                    selectedItem = inventory[i];
                }
                count++;
            }
        }
        GUI.EndScrollView();
    }
       
}
