using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool invenOpen = false;
    public GameObject invenWindow;
    public GameObject slotParent;
    private InvenSlot[] slotList;
    private Vector3 originalPos;
    private Vector3 targetPos;
    private bool arrivePos = true;
    public float windowSpeed = 3f;

    void Awake()
    {
        slotList = slotParent.GetComponentsInChildren<InvenSlot>();
        originalPos = invenWindow.transform.position;
        targetPos = originalPos;
    }

    void Update()
    {
        OpenInven();

        if (invenWindow.GetComponent<RectTransform>().position != targetPos)
        {
            Vector3 tarV = (targetPos - invenWindow.GetComponent<RectTransform>().position);
            invenWindow.transform.Translate(tarV * Time.deltaTime * windowSpeed);
        }
    }

    private void OpenInven()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))
        {
            if (!invenOpen)
            {
                // invenWindow.SetActive(true);
                targetPos = originalPos + new Vector3(0, 200, 0);
                invenOpen = true;
            }
            else
            {
                targetPos = originalPos;
                // invenWindow.SetActive(false);
                invenOpen = false;
            }
        }
    }

    public void GetItem(Item _item, int _count = 1)
    {
        for (int i = 0; i < slotList.Length; i++)
        {
            if (slotList[i].item != null)
            {
                if (slotList[i].item.itemName == _item.name)
                {
                    slotList[i].SlotCount(_count);
                    return;
                }
            }

            if (slotList[i].item == null)
            {
                slotList[i].ItemAdd(_item, _count);
                return;
            }
        }
    }
}
