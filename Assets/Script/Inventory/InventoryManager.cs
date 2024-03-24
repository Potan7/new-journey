using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    // ��� �κ��丮 ���� ���� ��ü
    public Transform container;
    // ������ ī�� ������
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
    

    //�κ��丮 Ű����� ��ư
    public void SetButton(bool OnOff)
    {
        animator.SetBool("Open", OnOff);
    }
}
