using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_obj : MonoBehaviour
{
    public Item item;
    private Inventory inventory;

    void Awake()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inventory = other.GetComponentInChildren<Inventory>();
            // inventory = FindObjectOfType<Inventory>();
            inventory.GetItem(item);
            Debug.Log(item.itemName);
            Destroy(this.gameObject);
        }
    }
}
