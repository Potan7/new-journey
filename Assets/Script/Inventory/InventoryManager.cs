using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    // 모든 인벤토리 슬롯 상위 객체
    public Transform container;
    // 아이템 카드 프리팹
    public InvenSlot itemCardContanier;

    Animator animator;

    List<InvenSlot> itemList = new List<InvenSlot>();

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AddItem(Item item)
    {
        InvenSlot card = Instantiate(itemCardContanier, container);
        card.AddItem(item);
        itemList.Add(card);    
    }
    

    //인벤토리 키고끄는 버튼
    public void SetButton(bool OnOff)
    {
        animator.SetBool("Open", OnOff);
    }
}
