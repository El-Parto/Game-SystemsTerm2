using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Items> inventory = new List<Items>(); // list of items
    [SerializeField]private bool showIMGUIInventory = true; // tick on untick to make it show or not
    
    public Items selectedItem = null;// places item in this variable

    #region Canvas Inventory
    [SerializeField] private Button ButtonPrefab;
    [SerializeField] private GameObject InventoryGameObject;
    [SerializeField] private GameObject InventoryContent;
    [SerializeField] private GameObject FilterContent;

    [Header("Selected Item Display")]
    [SerializeField] private RawImage itemImage;
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemDescription;

    #endregion

    public void AddItem(Items _item)
    {
        AddItem(_item, _item.Amount);
    }

    public void AddItem(Items _item, int count)
    {
        Items foundItem = inventory.Find((x) => x.Name == _item.Name);
        if(foundItem == null)
        {
            inventory.Add(_item);
        }
        else
        {
            foundItem.Amount += count;
        }
        DisplayItemsCanvas();
        DisplaySelectedItemOnCanvas(selectedItem);

    }
    public void RemoveItem(Items _item)
    {
        if (inventory.Contains(_item))
            inventory.Remove(_item);

        DisplayItemsCanvas();
        DisplaySelectedItemOnCanvas(selectedItem);
    }


    #region Display Inventory
    private Vector2 scrollPosition; // just for the IMGUI to scroll through it.
    private string sortType = "All"; // sorting through Inventory
    #endregion




    // Start is called before the first frame update
    void Start()
    {
        DisplayfiltersCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryGameObject.activeSelf)
            {
                InventoryGameObject.SetActive(false);
            }
            else
            {
                InventoryGameObject.SetActive(true);
                DisplayItemsCanvas();
            }
            
        }
    }

    private void DisplayfiltersCanvas()
    {
        //get name turns things into strings
        List<string> itemTypes = new List<string>(Enum.GetNames(typeof(Items.ItemType)));
        itemTypes.Insert(0, "All");

        for (int i = 0; i < itemTypes.Count; i++)
        {
            Button buttonGO = Instantiate<Button>(ButtonPrefab, FilterContent.transform);//adding it to the transform
            Text buttonText = buttonGO.GetComponentInChildren<Text>(); // the variable now equals text
            buttonGO.name = itemTypes[i] + "filter"; //name is the name of object in text plus filter
            buttonText.text = itemTypes[i]; // buttontext  is now the name of the item

            int x = i;
            buttonGO.onClick.AddListener(delegate { ChangeFilter(itemTypes[x]); });
        }
    }
    private void ChangeFilter(string itemType)
    {
        sortType = itemType;
        DisplayItemsCanvas();
    }

    private void DestroyAllChildren(Transform parent)
    {
        foreach(Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    private void DisplayItemsCanvas()
    {
        // to prevent the inventory from endlessly adding.
        DestroyAllChildren(InventoryContent.transform);

        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].Type.ToString() == sortType || sortType == "All")
            {
                Button buttonGO = Instantiate<Button>(ButtonPrefab, InventoryContent.transform);
                Text buttonText = buttonGO.GetComponentInChildren<Text>();
                buttonGO.name = inventory[i].Name + "button";
                buttonText.text = inventory[i].Name;

                Items item = inventory[i];
                buttonGO.onClick.AddListener(delegate { DisplaySelectedItemOnCanvas(item); });

            }
        }
    }
    void DisplaySelectedItemOnCanvas(Items item)
    {
        //click button, get item, from item, we want to fill in things with our item.
        selectedItem = item;
        itemImage.texture = selectedItem.Icon;
        itemName.text = selectedItem.Name;
        itemDescription.text = selectedItem.Description + 
            "\nValue:" + selectedItem.Value +
            "\nAmount:" + selectedItem.Amount;
    }
    // \ == carraige escape
    
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
                    selectedItem.onClicked();
                }
                count++;
            }
        }
        GUI.EndScrollView();
    }
       
}
