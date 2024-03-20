using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
    bool isitem = false;
    public bool isItem { get { return isitem; } }
    public Item item;

    [SerializeField]
    Image itemImageRen;

    public void AddItem(Item newItem)
    {
        if (isItem)
        {
            throw new System.Exception("이미 아이템이 들어있습니다.");
        }

        isitem = true;
        item = newItem;
        itemImageRen.sprite = item.itemImage;
        itemImageRen.gameObject.SetActive(true);
    }

    public void RemoveItem()
    {
        isitem = false;
        item = null;
        itemImageRen.sprite = null;
        itemImageRen.gameObject.SetActive(false);
    }

}
