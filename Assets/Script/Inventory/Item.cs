using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    // 아이템 이미지
    public Sprite sprite;
    // 아이템 이름
    public string visibleName;
    // 아이템 설명
    public string decription;

    // 아이템 전용 스크립트라든가 뭔가 더 넣을 수 있지
    // public ItemScript itemScript;
}
