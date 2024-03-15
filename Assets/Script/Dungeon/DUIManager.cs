using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DUIManager : MonoBehaviour
{
    public int rollCnt;

    public TextMeshProUGUI rollText;
    public Button rollButton;

    public DungeonManager dungeonManager;

    public void doRollButton()
    {
        rollButton.interactable = false;

        rollCnt = Random.Range(1, 7);
        ChangeDiceText(rollCnt);

        dungeonManager.diceRoll(rollCnt);
    }

    public void ChangeDiceText(int num)
    {
        rollText.text = num.ToString();
    }

    public void EnableRollButton()
    {
        rollButton.interactable = true;
    }
}
