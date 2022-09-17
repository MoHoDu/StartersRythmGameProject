using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InvenSlot : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public int itemCount = 0;
    public Image itemImg;
    [SerializeField] public TextMeshProUGUI itemCountText;
    private int slotNum;
    private ItemEffect itemEffect;

    private void Awake()
    {
        for (int i = 0; i < transform.parent.GetComponentsInChildren<InvenSlot>().Length; i++)
        {
            if (transform.parent.GetComponentsInChildren<InvenSlot>()[i] == this)
            {
                slotNum = i;
            }
        }

        itemEffect = FindObjectOfType<ItemEffect>();
    }

    private void Update()
    {
        OnKeyClick();
    }

    private void ImgAlpha(float _alpha)
    {
        Color color = itemImg.color;
        color.a = _alpha;
        itemImg.color = color;
    }

    public void ItemAdd(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImg.sprite = item.itemImg;
        itemCountText.text = itemCount.ToString();
        ImgAlpha(1);
    }

    public void SlotCount(int _count)
    {
        itemCount += _count;
        itemCountText.text = itemCount.ToString();
        if (itemCount <= 0) SlotClear();
    }

    public void SlotClear()
    {
        item = null;
        itemCount = 0;
        itemImg.sprite = null;
        ImgAlpha(0);
        itemCountText.text = itemCount.ToString();
    }

    public void OnKeyClick()
    {
        KeyCode n = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + (slotNum + 1 > 9 ? 0 : slotNum + 1).ToString());
        if (Input.GetKeyDown(n))
        {
            if (item != null)
            {
                if (item.itemName == "key01")
                {
                    return;
                }
                Debug.Log(item.itemName + "Consumed");
                itemEffect.ItemUse(item);
                SlotCount(-1);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null)
            {
                Debug.Log(item.itemName + "Consumed");
                itemEffect.ItemUse(item);
                SlotCount(-1);
            }
        }
    }
}
