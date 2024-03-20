using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // 인벤토리 창 UI
    public GameObject invenUI;

    // 모든 인벤토리 슬롯 상위 객체
    public GameObject slotParent;

    [SerializeField]
    InvenSlot[] slots;

    private void Awake()
    {
        slots = slotParent.GetComponentsInChildren<InvenSlot>();
    }

    // 가장 빠른 빈 칸에 아이템 넣기
    public void AddItem(Item newitem)
    {
        foreach (InvenSlot slot in slots)
        {
            if (!slot.isItem)
            {
                slot.AddItem(newitem);
                return;
            }
        }
    }

    //인벤토리 메뉴의 X 버튼에 할당됨
    public void ExitButton()
    {
        invenUI.SetActive(false);
    }
}
