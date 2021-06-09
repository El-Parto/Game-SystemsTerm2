using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plr;


[System.Serializable]
public class JamesEquipItem : Items
{


    public GameObject weapon;
    public Transform handle;

    public bool isEquipped = false;

    public PlayerSCreept.EquipmentSlot slot = PlayerSCreept.EquipmentSlot.HectorProtector;

    public override void onClicked()
    {
        base.onClicked();

        PlayerSCreept player = GameObject.FindObjectOfType<PlayerSCreept>();
        JamesEquipItem oldItem = player.EquipItem(this);

        //finding inventory
        Inventory inventory = GameObject.FindObjectOfType<Inventory>();

        if(oldItem != null)
        {
            inventory.AddItem(oldItem);

        }
        inventory.RemoveItem(this);

        GameObject newWeapon = GameObject.Instantiate<GameObject>(weapon, handle.transform);
    }


}
