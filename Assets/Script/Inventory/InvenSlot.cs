using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
    // ��ũ��Ʈ ���� �κ� ���������� ��ǻ� �������� ī�� �ȿ� �ִ°��� (������ ���)
{
    public Item item;

    [SerializeField]
    Image itemImageRen;
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI itemDecrption;

    public void AddItem(Item newItem)
    {
        item = newItem;
        itemImageRen.sprite = item.sprite;
        itemImageRen.gameObject.SetActive(true);
        itemDecrption.text = item.decription;
        itemName.text = item.visibleName;
    }

}
