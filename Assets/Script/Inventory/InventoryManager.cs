using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // �κ��丮 â UI
    public GameObject invenUI;

    // ��� �κ��丮 ���� ���� ��ü
    public GameObject slotParent;

    [SerializeField]
    InvenSlot[] slots;

    private void Awake()
    {
        slots = slotParent.GetComponentsInChildren<InvenSlot>();
    }

    // ���� ���� �� ĭ�� ������ �ֱ�
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

    //�κ��丮 �޴��� X ��ư�� �Ҵ��
    public void ExitButton()
    {
        invenUI.SetActive(false);
    }
}
