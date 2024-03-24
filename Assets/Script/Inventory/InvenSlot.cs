using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
    // 스크립트 명은 인벤 슬롯이지만 사실상 아이템을 카드 안에 넣는거임 (프리팹 사용)
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
