using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickupItem : MonoBehaviour
{
   [SerializeField]private Inventory inventory;
   [SerializeField] private Transform dropPoint;
   [SerializeField] private new Camera camera;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 50f))
            {
                DroppedItem droppedItem = hitInfo.collider.gameObject.GetComponent<DroppedItem>(); // if you hit something with this compenent,
                //now got that item.

                 
                if (droppedItem != null)
                {
                    if (droppedItem.item != null)
                    {
                        inventory.AddItem(droppedItem.item);
                    }
                    Destroy(hitInfo.collider.gameObject);
                }
            }
        }
    }


    public void DropItem ()
    { 
        if(inventory.selectedItem == null)
        {
            return;
        }
        GameObject mesh = inventory.selectedItem.Mesh;
        if(mesh != null)
        {
            GameObject spawnedMesh = Instantiate(mesh, null);
            spawnedMesh.transform.position = dropPoint.position;


            DroppedItem droppedItem = mesh.GetComponent<DroppedItem>();

            if(droppedItem == null)
            {
                droppedItem = spawnedMesh.AddComponent<DroppedItem>();
            }
            if (droppedItem != null)
            {

                droppedItem.item = new Items(inventory.selectedItem, 1);
            }
        }

        inventory.selectedItem.Amount--;
        //select item has amount 9assumpton0
        if(inventory.selectedItem.Amount <= 0)
        {
            inventory.RemoveItem(inventory.selectedItem);
            inventory.selectedItem = null;
        }
    }


}
