using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// the one with james

namespace Plr
{
    public class PlayerSCreept : MonoBehaviour
    {
        // first we need slots
        public enum EquipmentSlot
        {
            Helmet,
            Chestplate,
            Pants,
            Boots,
            Weapon,
            HectorProtector
        }

        private Dictionary<EquipmentSlot, JamesEquipItem> slots = new Dictionary<EquipmentSlot, JamesEquipItem>();


        // Start is called before the first frame update
        void Start()
        {
            // presetup slots even though nothing's in them
            foreach(EquipmentSlot slot in System.Enum.GetValues(typeof(EquipmentSlot)))
            {
                // * Auto generate the slots in the dictionary
                slots.Add(slot, null);
            }
            //slots are set up, now we need to uhh
        }


        public JamesEquipItem EquipItem(JamesEquipItem _toEquip)
        {// wanna make a new script before adding more than return null here

            if(_toEquip == null)
            {
                Debug.LogError("NOOOOO NOT THE NULL");
                    return null;
            }

            
            //find out if somethings already in the slot
            if(slots.TryGetValue(_toEquip.slot, out JamesEquipItem item))
            {
                // * Create a copy of the original set the slot item to the passed value
                JamesEquipItem original = item;
                slots[_toEquip.slot] = _toEquip;
                // * return what was origninally in the slot to prevent losing items when equipping
                return original;
            }
            
            // Somehow the slot didn't exist, so create it and return null as no item
            // would be in the slot anyway.
            slots.Add(_toEquip.slot, _toEquip);
            return null;

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}