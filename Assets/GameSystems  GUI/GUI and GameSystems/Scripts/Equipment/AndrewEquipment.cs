using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EquipmentSlot
{
    [SerializeField] private Items item;
    public Items EquippedItem
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
            itemEquipped.Invoke(this); // ** change to this
        }
    }
    public Transform visualLocation;
    public Vector3 offset;

    public delegate void ItemEquipped(EquipmentSlot item); // ** change to equipment slot
    public event ItemEquipped itemEquipped;

}

//Eqipment equpment = get component<>
// equipment.primary.EquippedItem = ItemToEquip;

public class AndrewEquipment : MonoBehaviour
{
    public EquipmentSlot primary;
    public EquipmentSlot secondary;
    public EquipmentSlot defensive;

    public void Awake()
    {
        primary.itemEquipped += EquipItem;
        secondary.itemEquipped += EquipItem;
        defensive.itemEquipped += EquipItem;
    }

    // Start is called before the first frame update
    void Start()
    {
        EquipItem(primary);
        EquipItem(secondary);
        EquipItem(defensive);
    }


    public void EquipItem(EquipmentSlot item) //  passes what equip slot it's going to
    {
        if(item.visualLocation == null) // if there's nowhere to put it
        {
            return;
        }
        foreach(Transform child in item.visualLocation)// each item in the location?
        {
            GameObject.Destroy(child.gameObject);
        }
        // need a way of unequipping
        if(item.EquippedItem.Mesh == null)
        {
            return;
        }

        GameObject meshInstance = Instantiate(item.EquippedItem.Mesh, item.visualLocation);//spawn item based on visual location
        meshInstance.transform.localPosition = item.offset;
        
        OffsetLocation offset = meshInstance.GetComponent<OffsetLocation>();
        if(offset != null)
        {
            meshInstance.transform.localPosition += offset.positionOffset;

            meshInstance.transform.localRotation = Quaternion.Euler(offset.rotationOffset);

            meshInstance.transform.localScale = offset.scaleOffset;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
